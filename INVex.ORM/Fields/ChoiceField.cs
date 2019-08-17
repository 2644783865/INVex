using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Fields
{
    public class ChoiceField : ObjectField
    {
        public Dictionary<int, string> Items { get; private set; }

        public int CurrentItemIndex { get; set; } = 0;

        public new string Value
        {
            get
            {
                return this.Items[this.CurrentItemIndex];
            }
        }

        public ChoiceField(Dictionary<int, string> items)
        {
            this.Items = items;
        }

        public override void SetValue(object value)
        {
            this.CurrentItemIndex = (int)value;
            this.WasReaded = true;
        }
    }
}
