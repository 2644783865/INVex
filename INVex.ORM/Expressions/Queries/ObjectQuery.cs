using INVex.ORM.DataBase.Common;
using INVex.ORM.Expressions.Base;
using INVex.ORM.Holders;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System.Collections.Generic;

namespace INVex.ORM.Expressions.Queries
{
    public class ObjectQuery : BaseQuery
    {
        public override string QueryString { get; protected set; }
        public override bool NeedTransaction { get; set; }
        public override Dictionary<string, object> QueryParameters { get; set; }

        public ICriteria Criteria { get; set; } = null;


        public List<IAttributePath> RequiredAttributes { get; set; } = new List<IAttributePath>();
        

        private IObjectInstance modelInstance;

        public ObjectQuery(string modelName)
        {
            this.modelInstance = ObjectModelsHolder.Current.Holder.CreateInstance(modelName);
        }

        public ObjectQuery(IObjectInstance instance)
        {
            this.modelInstance = instance;
        }

        protected virtual void ParseQuery()
        {
            if(this.RequiredAttributes.Count > 0)
            {
                foreach(IAttributePath path in this.RequiredAttributes)
                {
                    this.modelInstance.AddRequiredAttribute(path);
                }
            }

            string selectStmnt = string.Empty;
            string attrsString = string.Empty;
            string joins = string.Empty;
            string whereStmnt;

            int tableJoins = 0;

            foreach(KeyValuePair<IAttributePath, IAttributeModel> pair in this.modelInstance.RequiredAttributes)
            {
                foreach (IAttributeStep step in pair.Key.Steps)
                {
                    if(step.Attribute.Mapping == null)
                    {
                        #warning Нужно подумать
                        continue;
                    }

                    if(step.Attribute is IReferenceAttribute)
                    {
                        IReferenceAttribute refAttr = (IReferenceAttribute)step.Attribute;
                        tableJoins++;
                        string prefix = "t" + tableJoins;
                        joins += string.Format("LEFT JOIN {0} AS {1} ON {2} = {3}",
                            refAttr.Field.Reference.Table.FullName, 
                            prefix, 
                            prefix + "." + step.Attribute.Mapping.ColumnName,
                            refAttr.Field.Reference.PrimaryKey.Mapping.ColumnName
                            );
                        attrsString += string.Format("[{0}].[{1}],", prefix, step.Attribute.Owner.PrimaryKey.Mapping.ColumnName);
                    }
                    else
                    {
                        attrsString += string.Format("[{0}],", step.Attribute.Mapping.ColumnName);
                    }
                }
            }

            if(this.Criteria != null)
            {
                this.Criteria.ToSql();
            }

            selectStmnt = string.Format("SELECT {0} FROM {1} {2}", attrsString.TrimEnd(','), this.modelInstance.Table.FullName, joins);
            this.QueryString = selectStmnt;
        }

        public new List<IObjectInstance> Execute()
        {
            this.ParseQuery();

            List<IObjectInstance> objectInstances = new List<IObjectInstance>();
            ExecutionResult executionResult = (ExecutionResult)base.Execute();

            foreach (RowResult row in executionResult.Rows)
            {
                IObjectInstance objectInstance = ObjectModelsHolder.Current.Holder.CreateInstance(this.modelInstance.Model.Name);

                foreach (string columnName in row.Keys)
                {
                    objectInstance.Attributes[objectInstance.GetAttributeByMappingColumn(columnName).Name].Field.ForceSet(row[columnName]);
                }

                objectInstances.Add(objectInstance);
            }


            return objectInstances;
        }

        public List<T> Execute<T>() where T : IObjectInstance
        {
            this.ParseQuery();

            List<T> objectInstances = new List<T>();
            ExecutionResult executionResult = (ExecutionResult)base.Execute();

            foreach(RowResult row in executionResult.Rows)
            {
                T objectInstance = (T)ObjectModelsHolder.Current.Holder.CreateInstance(this.modelInstance.Model.Name);

                foreach(string columnName in row.Keys)
                {
                    objectInstance.Attributes[objectInstance.GetAttributeByMappingColumn(columnName).Name].Field.ForceSet(row[columnName]);
                }

                objectInstances.Add(objectInstance);
            }


            return objectInstances;
        }
    }
}
