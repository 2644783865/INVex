using System;

namespace INVex.ORM.Fields
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
    }
}