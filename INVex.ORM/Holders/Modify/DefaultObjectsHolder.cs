using INVex.ORM.Common;
using INVex.ORM.DataBase.Common;
using INVex.ORM.Exceptions;
using INVex.ORM.Expressions.Queries;
using INVex.ORM.Holders.Base;
using INVex.ORM.Objects;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Holders.Modify
{
    public class DefaultObjectsHolder : IObjectHolder
    {
        private Dictionary<string, IObjectModel> cachedModels = new Dictionary<string, IObjectModel>();

        public string Name { get; private set; }

        public DefaultObjectsHolder(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Creates instance with valid type
        /// </summary>
        /// <param name="modelName">Model name of required instance</param>
        /// <returns>Instance with type set in <see cref="IObjectModel.InstanceTypeQualifiedName"/> (or default type <see cref="Constants.DefaultInstanceType"/>)</returns>
        public IObjectInstance CreateInstance(string modelName)
        {
            if (!cachedModels.ContainsKey(modelName))
            {
                throw new ModelNotFoundException(modelName);
            }

            IObjectModel model = cachedModels[modelName];

            if (string.IsNullOrEmpty(model.InstanceTypeQualifiedName))
            {
                model.InstanceTypeQualifiedName = Constants.DefaultInstanceType;
            }

            IObjectInstance instance = null;
            try
            {
                instance = (IObjectInstance)Activator.CreateInstance(Type.GetType(model.InstanceTypeQualifiedName), model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instance;
        }

        public IObjectInstance GetInstance(string modelName, object primaryKey)
        {
            throw new NotImplementedException();
        }

        public IObjectInstance GetInstance(IObjectModel model, object primaryKey)
        {
            throw new NotImplementedException();
        }

        public IObjectModel GetModel(string modelName)
        {
            if (!cachedModels.ContainsKey(modelName))
            {
                throw new ModelNotFoundException(modelName);
            }

            return this.cachedModels[modelName];
        }

        public void LoadModels()
        {
            RawQuery query = new RawQuery(string.Format("SELECT * FROM {0}", Constants.DbModelsTable));
            ExecutionResult result = (ExecutionResult)query.Execute();

            foreach (RowResult row in result.Rows)
            {
                IObjectModel modelInstance = new ObjectModel();
                modelInstance.InstanceTypeQualifiedName = (string)row["InstanceTypeQualifiedName"];
                modelInstance.Description = (string)row["Description"];
                modelInstance.Name = (string)row["Name"];
                modelInstance.Id = (int)row["Id"];

                if (this.cachedModels.ContainsKey(modelInstance.Name))
                {
                    this.cachedModels[modelInstance.Name] = modelInstance;
                }
                else
                {
                    this.cachedModels.Add(modelInstance.Name, modelInstance);
                }
            }
        }

        public void SaveObject(IObjectInstance instance)
        {
            //List<IAttributeModel> changedAttributes = new List<IAttributeModel>();
            bool changed = false;
            StringBuilder setString = new StringBuilder();
            Dictionary<string, object> sqlParams = new Dictionary<string, object>();

            foreach (IAttributeModel attrModel in instance.Attributes.Values)
            {
                bool attrIsIdentity = false;
                if (attrModel.CustomFlags.ContainsKey("IsIdentity"))
                {
                    attrIsIdentity = (string)attrModel.CustomFlags["IsIdentity"] == "1";
                }

                if (instance.IsNew)
                {
                    if (!attrIsIdentity)
                    {
                        if(attrModel.Value == null)
                        {
                            sqlParams.Add("@" + attrModel.Name, DBNull.Value);
                        }
                        else
                        {
                            sqlParams.Add("@" + attrModel.Name, attrModel.Value);
                        }
                        changed = true;

                        setString.AppendFormat(" @{1},", attrModel.Mapping.ColumnName, attrModel.Name);
                    }
                }

                if (!instance.IsNew && attrModel.Field.WasUpdated && !attrIsIdentity)
                {
                    sqlParams.Add("@" + attrModel.Name, attrModel.Value);
                    changed = true;

                    setString.AppendFormat(" [{0}] = @{1},", attrModel.Mapping.ColumnName, attrModel.Name);
                }
            }

            //INSERT INTO T_TABLE VALUES()
            if(instance.IsNew == true)
            {
                RawQuery qr = new RawQuery(
                    string.Format("INSERT INTO {0} VALUES({1})", instance.Table.FullName, setString.ToString().TrimEnd(',')),
                    true,
                    sqlParams
                    );
                qr.Execute();
            }
            //UPDATE T_TABLE SET id = @id where primary = @primary;
            else
            {
                //TODO
                sqlParams.Add("@" + instance.PrimaryKey.Name, instance.PrimaryKey.Value);
                RawQuery qr = new RawQuery(
                    string.Format("UPDATE {0} SET {1} WHERE {2}=@{3}", instance.Table.FullName, setString.ToString().TrimEnd(','), instance.PrimaryKey.Mapping.ColumnName, instance.PrimaryKey.Name),
                    true, 
                    sqlParams
                    );
                qr.Execute();
            }

        }
    }
}
