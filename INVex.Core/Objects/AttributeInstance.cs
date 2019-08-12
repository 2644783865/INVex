using INVex.Core.Fields;
using INVex.Core.Fields.Base;
using INVex.Core.Objects.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using INVex.Core.Serialize.Base;

namespace INVex.Core.Objects
{
    public class AttributeInstance : IAttributeModel, IBinarySerializable
    {
        public string Name { get; private set; }
        public string Description { get; protected set; }
        public IBaseField Field { get; protected set; }
        public string Type { get; protected set; }
        public IAttributeMapping Mapping { get; set; }
        public IObjectInstance Owner { get; protected set; }

        protected XDocument AttributeXElement;

        public AttributeInstance(string name, string description, string attributeType, IObjectInstance owner)
        {
            this.Name = name;
            this.Description = description;
            this.Type = attributeType;
            this.Owner = owner;

            this.PrepareParse();
            this.Parse();
            this.CreateField();
        }

        /// <summary>
        /// Подготавительная стадия для парсинга.
        /// Нужна для случая, если нужно отказаться от XML
        /// </summary>
        protected virtual void PrepareParse()
        {
            this.AttributeXElement = XDocument.Parse(this.Description);
        }

        protected virtual void Parse()
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
