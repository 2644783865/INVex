using INVex.Core.Fields;
using INVex.Core.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace INVex.Core.Objects.Modify
{
    public class ReferenceAttribute : AttributeInstance
    {
        public string ReferenceType { get; private set; }
        public ReferenceAttribute(string name, string description, string attributeType, IObjectInstance owner) : base(name, description, attributeType, owner) { }

        protected override void Parse()
        {
            XDocument elementModel = XDocument.Parse(this.Description);
            this.ReferenceType = elementModel.Root.Attribute("RefType").Value;
        }

        public override void CreateField()
        {
            this.Field = new ReferenceField(this.ReferenceType);
        }
    }
}
