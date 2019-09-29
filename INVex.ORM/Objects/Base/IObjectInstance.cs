using INVex.ORM.Objects.Attributes.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Base
{
    public interface IObjectInstance
    {
        bool IsNew { get; set; }
        IObjectModel Model { get; }
        IDbTable Table { get; }
        Dictionary<string, IAttributeModel> Attributes { get; }
        //Dictionary<IAttributePath, IAttributeModel> RequiredAttributes { get; }
        List<IPathElement> RequiredAttributes { get; }
        IAttributeModel PrimaryKey { get; }
        IAttributeModel GetAttributeByMappingColumn(string columnName);
        IAttributeModel GetAttribute(IAttributePath path);
        IAttributeModel GetAttribute(string attributeName);
        void AddRequiredAttribute(IAttributePath path);
        void AddRequiredAttribute(IAttributeModel model);
        void SetAttributeValue(string attributeName, object attributeValue);
    }
}
