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
            this.ForceSet(DateTime.MinValue);
        }
    }
}
