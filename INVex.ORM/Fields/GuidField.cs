using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Fields
{
    public class GuidField : ObjectField
    {
        public new Guid Value
        {
            get
            {
                return (Guid)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        public GuidField()
        {
        }
    }
}
