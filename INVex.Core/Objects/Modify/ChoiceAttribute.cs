using INVex.Core.Fields;
using INVex.Core.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace INVex.Core.Objects.Modify
{
    public class ChoiceAttribute : AttributeInstance
    {
        public Dictionary<int, string> AvailableItems { get; private set; } = new Dictionary<int, string>();
        public ChoiceAttribute(string name, string description, string attributeType, IObjectInstance owner) : base(name, description, attributeType, owner) {}

        protected override void Parse()
        {
            foreach(XElement item in this.AttributeXElement.Root.Elements())
            {
                this.AvailableItems.Add(int.Parse(item.Attribute("Index").Value), item.Attribute("Value").Value);
            }
        }

        public override void CreateField()
        {
            this.Field = new ChoiceField(this.AvailableItems);
        }
    }
}
