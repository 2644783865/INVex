using INVex.Core.Fields;
using INVex.Core.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Objects.Modify
{
    public class PropertyAttribute : AttributeInstance
    {
        public string ValueType { get; private set; }

        public PropertyAttribute(string name, string description, string attributeType, IObjectInstance owner) : base(name, description, attributeType, owner) { }

        protected override void Parse()
        {
            this.ValueType = this.AttributeXElement.Root.Attribute("ValueType").Value;
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
                default:
                    base.CreateField();
                    break;
            }
        }
    }
}
