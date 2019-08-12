using INVex.Core.Fields.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Objects.Base
{
    public interface IAttributeModel
    {
        string Name { get; }
        string Description { get; }
        IBaseField Field { get; }
        string Type { get; }
        IAttributeMapping Mapping { get; set; }
        IObjectInstance Owner { get; }
    }
}
