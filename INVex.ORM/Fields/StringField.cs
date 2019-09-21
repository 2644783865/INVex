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

        public static implicit operator string(StringField field)
        {
            return field.Value;
        }

        public static bool operator ==(StringField a, StringField b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(StringField a, StringField b)
        {
            return a.Value != b.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is StringField field &&
                   base.Equals(obj) &&
                   this.Value == field.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), this.Value);
        }

        public override bool Equals(IBaseField other)
        {
            return (StringField)other == this;
        }

    }
}
