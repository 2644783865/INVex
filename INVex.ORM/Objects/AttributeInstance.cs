using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using INVex.Common.Serialize.Base;
using INVex.ORM.Fields.Base;
using INVex.ORM.Fields;
using INVex.ORM.Objects.Base;
using INVex.ORM.Objects.Attributes.Base;

namespace INVex.ORM.Objects
{
    public class AttributeInstance : IAttributeModel, IBinarySerializable
    {
        public string Name { get; private set; }
        public string Description { get; protected set; }
        public IBaseField Field { get; protected set; }
        public string Type { get; protected set; }
        public IAttributeMapping Mapping { get; set; }
        public IObjectInstance Owner { get; protected set; }
        public Dictionary<string, object> CustomFlags { get; set; } = new Dictionary<string, object>();

        public object Value { get { return this.Field.Value; } }


        public AttributeInstance(string name, string description, string attributeType, IObjectInstance owner)
        {
            this.Name = name;
            this.Description = description;
            this.Type = attributeType;
            this.Owner = owner;

            this.PrepareParse();
            this.Parse();
            this.PostParse();

            this.CreateField();
        }

        protected virtual void PrepareParse()
        {
            
        }

        protected virtual void Parse()
        {

        }

        protected virtual void PostParse()
        {

        }

        public virtual void CreateField()
        {
            this.Field = new ObjectField();
        }

        public virtual void Pack(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public virtual void Unpack(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
