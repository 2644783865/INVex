using System;
using System.Collections.Generic;
using System.Text;
using INVex.ORM.Fields.Base;

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
    }
}
