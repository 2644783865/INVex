using INVex.Common.Common;
using INVex.ORM.Common;
using INVex.ORM.DataBase.SQLServer;
using INVex.ORM.Expressions.Queries;
using INVex.ORM.Objects;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Holders
{
    public class ObjectModelsHolder : SingletonBase<ObjectModelsHolder>
    {
        private Dictionary<string, IObjectModel> cachedModels = new Dictionary<string, IObjectModel>();

        public ObjectModelsHolder()
        {
            
        }

        public void LoadModels(Type modelType)
        {
            #warning Replace (ObjectQuery)
            RawQuery query = new RawQuery("SELECT * FROM T_ObjectModels");
            SQLExecutionResult result = (SQLExecutionResult)query.Execute();

            //Console.WriteLine(typeOfInstance.GetType().Name);

            foreach(SQLRow row in result.Rows)
            {
                IObjectModel modelInstance = (IObjectModel)Activator.CreateInstance(modelType);
                modelInstance.InstanceTypeQualifiedName = (string)row["InstanceTypeQualifiedName"];
                modelInstance.Description = (string)row["Description"];
                modelInstance.Name = (string)row["Name"];
                modelInstance.Id = (int)row["Id"];

                this.cachedModels.Add(modelInstance.Name, modelInstance);
            }
        }

        public IObjectInstance CreateInstance(string modelName)
        {
            if (!cachedModels.ContainsKey(modelName))
            {
                throw new Exception(string.Format("Model with name '{0}' not found", modelName));
            }

            IObjectModel model = cachedModels[modelName];

            if (string.IsNullOrEmpty(model.InstanceTypeQualifiedName))
            {
                model.InstanceTypeQualifiedName = Constants.DefaultInstanceType;
            }

            IObjectInstance instance = (IObjectInstance)Activator.CreateInstance(Type.GetType(model.InstanceTypeQualifiedName), model);

            return instance;
        }

        public IObjectModel GetCachedModel(string name)
        {
            return this.cachedModels[name];
        }
    }
}
