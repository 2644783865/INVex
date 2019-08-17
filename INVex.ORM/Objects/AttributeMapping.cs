using INVex.ORM.Objects.Base;
using INVex.ORM.Objects.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects
{
    public class AttributeMapping : IAttributeMapping
    {
        public string AttributeName { get; }

        public string ColumnName { get; }

        public MappingType MappingType { get; }

        public AttributeMapping(string attributeName, string columnName, MappingType mappingType = MappingType.Column)
        {
            this.AttributeName = attributeName;
            this.ColumnName = columnName;
            this.MappingType = mappingType;
        }
    }
}
