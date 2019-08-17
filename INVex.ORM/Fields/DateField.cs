using System;

namespace INVex.ORM.Fields
{
    public class DateField : ObjectField
    {
        public new DateTime Value
        {
            get
            {
                return (DateTime)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        public DateField()
        {
            this.Value = DateTime.MinValue;
        }

        public static bool operator ==(DateField a, DateField b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(DateField a, DateField b)
        {
            return a.Value != b.Value;
        }

        public static bool operator >(DateField a, DateField b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(DateField a, DateField b)
        {
            return a.Value < b.Value;
        }

#warning Возможно проблемы. Нужно проверить.
        public override bool Equals(object obj)
        {
            return obj is DateField field &&
                   this.Value == field.Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
