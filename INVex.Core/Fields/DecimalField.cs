using System;
namespace INVex.Core.Fields
{
    public class DecimalField : ObjectField
    {
        public new decimal Value
        {
            get
            {
                return (decimal)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        public DecimalField()
        {
            this.Value = 0;
        }

        public static bool operator ==(DecimalField a, DecimalField b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(DecimalField a, DecimalField b)
        {
            return a.Value != b.Value;
        }

#warning Возможно проблемы. Нужно проверить.
        public override bool Equals(object obj)
        {
            return obj is DecimalField field &&
                   this.Value == field.Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();

        }
    }
}