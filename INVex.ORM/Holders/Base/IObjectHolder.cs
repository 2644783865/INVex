using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Holders.Base
{
    public interface IObjectHolder
    {
        string Name { get; }
        void SaveObject(IObjectInstance instance);
        void LoadModels();
        IObjectInstance CreateInstance(string modelName);
        IObjectInstance GetInstance(string modelName, object primaryKey);
        IObjectInstance GetInstance(IObjectModel model, object primaryKey);
        IObjectModel GetModel(string modelName);
    }
}
