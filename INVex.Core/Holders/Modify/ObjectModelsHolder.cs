using INVex.Core.Common;
using INVex.Core.Holders.Base;
using INVex.Core.Objects.Base;
using INVex.Core.Objects.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Holders.Modify
{
    public class ObjectModelsHolder : SingletonBase<ObjectModelsHolder>, IHolderBase
    {
        public const string HolderName = "ObjectModelsHolder";

        private Dictionary<ObjectModelKey, IObjectModel> cachedModels = new Dictionary<ObjectModelKey, IObjectModel>();

        public string Name { get { return HolderName; } }

        #region Contains
        public bool ContainsInCached(IObjectModel model)
        {
            foreach (ObjectModelKey key in this.cachedModels.Keys)
            {
                if (key.Equals(new ObjectModelKey(model)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsInCached(int modelId)
        {
            foreach (ObjectModelKey key in this.cachedModels.Keys)
            {
                if (key.Equals(new ObjectModelKey(modelId)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsInCached(string modelName)
        {
            foreach (ObjectModelKey key in this.cachedModels.Keys)
            {
                if (key.Equals(new ObjectModelKey(modelName)))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Indexers
        public IObjectModel this[int index]
        {
            get
            {
                return this.FindModelByKey(new ObjectModelKey(index));
            }
        }

        public IObjectModel this[string name]
        {
            get
            {
                return this.FindModelByKey(new ObjectModelKey(name));
            }
        }
        #endregion

        private IObjectModel FindModelByKey(ObjectModelKey key)
        {
            foreach (ObjectModelKey cachedKey in this.cachedModels.Keys)
            {
                if (cachedKey.Equals(key))
                {
                    return this.cachedModels[cachedKey];
                }
            }
            return null;
        }

        public virtual void FillModels(List<IObjectModel> models)
        {
            foreach (IObjectModel model in models)
            {
                this.cachedModels.Add(new ObjectModelKey(model), model);
            }
        }

        public virtual void AddModel(IObjectModel model)
        {
            this.cachedModels.Add(new ObjectModelKey(model), model);
        }

        #region Get cached model

        public IObjectModel GetCachedModel(int modelId)
        {
            return this.FindModelByKey(new ObjectModelKey(modelId));
        }

        public IObjectModel GetCachedModel(string modelName)
        {
            return this.FindModelByKey(new ObjectModelKey(modelName));
        }

        public IObjectModel GetCachedModel(ObjectModelKey objectModelKey)
        {
            return cachedModels[objectModelKey];
        }

        #endregion

        public void Register()
        {
            HoldersCollection.Current.RegisterHolder(this);
        }
    }
}
