using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Fields
{
    public class IntField : ObjectField
    {
        public new int Value
        {
            get
            {
                return (int)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        public IntField()
        {
            this.Value = default;
        }

        public IntField(int value)
        {
            this.Value = value;
        }
    }
}
