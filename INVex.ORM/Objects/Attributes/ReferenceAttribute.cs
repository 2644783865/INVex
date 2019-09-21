using INVex.ORM.Fields;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace INVex.ORM.Objects.Attributes
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

        public new IObjectInstance Value
        {
            get
            {
                if (this.Field.WasReaded)
                {
                    return this.Field.Reference;
                }

                return null;
            }
        }

        public string ReferenceModel { get; private set; }
        public ReferenceAttribute(string name, string description, string attributeType, IObjectInstance owner) : base(name, description, attributeType, owner) { }

        protected override void Parse()
        {
            XDocument elementModel = XDocument.Parse(this.Description);
            this.ReferenceModel = elementModel.Root.Attribute("RefModel").Value;
        }

        public override void CreateField()
        {
            base.Field = new ReferenceField(this.ReferenceModel);
        }
    }
}
