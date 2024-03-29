﻿using INVex.ORM.Fields;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace INVex.ORM.Objects.Attributes
{
    public class CollectionAttribute : AttributeInstance
    {
        private bool isUniversalCollection = false;
        private string collectionItemsType = "ObjectField";
        public CollectionAttribute(string name, string description, string attributeType, IObjectInstance owner) : base(name, description, attributeType, owner) { }

        protected override void Parse()
        {
            XDocument attributeXElement = XDocument.Parse(this.Description);

            if (attributeXElement.Root.Attribute("IsUniversal") != null)
            {
                this.isUniversalCollection = bool.Parse(attributeXElement.Root.Attribute("IsUniversal").Value);
            }

            if (attributeXElement.Root.Attribute("ItemsType") != null)
            {
                this.collectionItemsType = attributeXElement.Root.Attribute("ItemsType").Value;
            }
        }

        public override void CreateField()
        {
            if (this.isUniversalCollection)
            {
                this.Field = new CollectionField(true);
            }
            else if (!string.IsNullOrEmpty(this.collectionItemsType))
            {
                this.Field = new CollectionField(this.collectionItemsType);
            }
            else
            {
                this.Field = new CollectionField();
            }
        }

    }
}
