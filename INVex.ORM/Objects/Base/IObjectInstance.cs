using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Base
{
    public interface IObjectInstance
    {
        IObjectModel Model { get; }
        IDbTable Table { get; }
        Dictionary<string, IAttributeModel> Attributes { get; }
        Dictionary<IAttributePath, IAttributeModel> RequiredAttributes { get; }
        IAttributeModel PrimaryKey { get; }
        IAttributeModel GetAttributeByPath(IAttributePath path);
    }
}
