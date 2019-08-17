using INVex.ORM.Objects.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Base
{
    public interface IAttributeMapping
    {
        string AttributeName { get; }
        string ColumnName { get; }
        MappingType MappingType { get; }
    }
}
