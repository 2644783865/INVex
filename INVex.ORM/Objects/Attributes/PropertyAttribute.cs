using INVex.ORM.Fields;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace INVex.ORM.Objects.Attributes
{
    public class PropertyAttribute : AttributeInstance
    {
        public string ValueType { get; private set; }

        public PropertyAttribute(string name, string description, string attributeType, IObjectInstance owner) : base(name, description, attributeType, owner) { }

        protected override void Parse()
        {
            XDocument attributeXElement = XDocument.Parse(this.Description);

            if(attributeXElement.Root.Attribute("ValueType") == null)
            {
                throw new Exception("Attribute should have some type (see 'ValueType' attribute)");
            }

            this.ValueType = attributeXElement.Root.Attribute("ValueType").Value;
        }

        public override void CreateField()
        {
            switch (this.ValueType.ToLower())
            {
                case "int":
                case "num":
                    this.Field = new IntField();
                    break;
                case "string":
                case "text":
                    this.Field = new StringField();
                    break;
                case "decimal":
                case "float":
                case "double":
                    this.Field = new DecimalField();
                    break;
                case "date":
                case "datetime":
                    this.Field = new DateField();
                    break;
                case "guid":
                    this.Field = new GuidField();
                    break;
                default:
                    base.CreateField();
                    break;
            }
        }
    }
}
