using INVex.ORM.Fields;
using INVex.ORM.Objects;
using INVex.ORM.Objects.Base;
using INVex.ORM.Objects.Modify.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace INVex.ORM.Objects.Modify
{
    public class ReferenceAttribute : AttributeInstance, IReferenceAttribute
    {
        public new ReferenceField Field
        {
            get
            {
                return (ReferenceField)base.Field;
            }
        }

        public string ReferenceType { get; private set; }
        public ReferenceAttribute(string name, string description, string attributeType, IObjectInstance owner) : base(name, description, attributeType, owner) { }

        protected override void Parse()
        {
            XDocument elementModel = XDocument.Parse(this.Description);
            this.ReferenceType = elementModel.Root.Attribute("RefType").Value;
        }

        public override void CreateField()
        {
            base.Field = new ReferenceField(this.ReferenceType);
        }
    }
}
