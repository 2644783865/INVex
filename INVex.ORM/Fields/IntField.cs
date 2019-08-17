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

        public static bool operator ==(IntField a, IntField b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(IntField a, IntField b)
        {
            return a.Value != b.Value;
        }

#warning Возможно проблемы. Нужно проверить.
        public override bool Equals(object obj)
        {
            return obj is IntField field &&
                   this.Value == field.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }
    }
}
