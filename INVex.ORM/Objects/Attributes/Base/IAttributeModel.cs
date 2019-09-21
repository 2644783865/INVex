using INVex.ORM.Fields.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Attributes.Base
{
    public interface IAttributeModel
    {
        string Name { get; }
        string Description { get; }
        object Value { get; }
        IBaseField Field { get; }
        string Type { get; }
        IAttributeMapping Mapping { get; set; }
        IObjectInstance Owner { get; }
        Dictionary<string, object> CustomFlags { get; }
    }
}
