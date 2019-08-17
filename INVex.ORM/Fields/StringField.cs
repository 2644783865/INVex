using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Fields
{
    public class StringField : ObjectField
    {
        public new string Value
        {
            get
            {
                return (string)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        public StringField()
        {

        }

        public StringField(string value)
        {
            this.Value = value;
        }

        public static implicit operator string(StringField field)
        {
            return field.Value;
        }
    }
}
