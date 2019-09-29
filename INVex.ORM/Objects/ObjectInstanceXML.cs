using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using INVex.ORM.Objects.Attributes;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;

namespace INVex.ORM.Objects
{
    public class ObjectInstanceXML : ObjectInstance
    {
        #region .ctor

        public ObjectInstanceXML()
        {
        }

        public ObjectInstanceXML(IObjectModel model) : base(model)
        {
        }

        #endregion

        #region Parsing
        protected override void BeforeParse()
        {
            base.BeforeParse();
        }

        public override void Parse()
        {
            try
            {
                XDocument modelDoc = XDocument.Parse(this.Model.Description);

                string pkName = modelDoc.Root.Attribute("PrimaryKey").Value;

                List<XElement> fields = modelDoc.XPathSelectElements("ObjectModel/Fields/*").ToList();
                List<XElement> mapping = modelDoc.XPathSelectElements("ObjectModel/Mapping/*").ToList();

                #region Attributes creation. Section "FIELD"
                foreach (XElement singleField in fields)
                {
                    string attributeName = singleField.Attribute("Name").Value as string;

                    if (string.IsNullOrEmpty(attributeName))
                    {
                        throw new Exception(string.Format("В описании объекта {0} есть неназванный атрибут", this.ModelName));
                    }

                    if (this.Attributes.ContainsKey(attributeName))
                    {
                        throw new Exception(string.Format("Атрибут с именем {0} уже находится в коллекции аттрибутов объекта {1}", attributeName, this.ModelName));
                    }

                    string attributeType = singleField.Name.LocalName;

                    IAttributeModel newAttributeInstance = this.CreateAttribute(attributeName, attributeType, singleField.ToString());
                    if (newAttributeInstance == null)
                    {
                        throw new Exception();
                    }

                    this.Attributes.Add(attributeName, newAttributeInstance);

                    if(newAttributeInstance.Name == pkName)
                    {
                        this.PrimaryKey = this.Attributes[pkName];
                    }
                }
                #endregion

                #region Attribute mappings creation. Section "MAPPING"

                XElement mappingSection = modelDoc.XPathSelectElement("ObjectModel/Mapping");

                this.Table = new DbTableInfo(mappingSection.Attribute("Table").Value, mappingSection.Attribute("Prefix") == null ? string.Empty : mappingSection.Attribute("Prefix").Value);
                
                foreach (XElement singleMapping in mapping)
                {
                    XAttribute src = singleMapping.Attribute("Source");
                    XAttribute attrName = singleMapping.Attribute("Attribute");
                    if (src == null || attrName == null)
                    {
                        IXmlLineInfo mappingInfo = singleMapping;
                        throw new Exception(string.Format("Маппирование объекта {0} неверно. Строка №{1}", this.ModelName, mappingInfo.LineNumber));
                    }

                    string attributeName = attrName.Value as string;
                    string sourceName = src.Value as string;

                    if (string.IsNullOrEmpty(attributeName) || string.IsNullOrEmpty(sourceName))
                    {
                        continue;
                    }

                    this.Attributes[attributeName].Mapping = this.CreateAttributeMapping(attributeName, sourceName);
                }
                #endregion


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected override void AfterParse()
        {
            base.AfterParse();
        }

        #region Attribute creation

        protected override IAttributeModel CreateAttribute(string attributeName, string attributeType, string description)
        {
            switch (attributeType)
            {
                case "Property":
                    return new PropertyAttribute(attributeName, description, "Property", this);

                case "Reference":
                    return new ReferenceAttribute(attributeName, description, "Reference", this);

                case "FixedList":
                    return new FixedListAttribute(attributeName, description, "FixedList", this);

                case "Collection":
                    return new CollectionAttribute(attributeName, description, "Collection", this);

                default:
                    throw new Exception(string.Format("В описании объекта {0} атрибут с именем {1} имеет неизвестный тип {2}", this.ModelName, attributeName, attributeType));
            }
        }

        protected override IAttributeMapping CreateAttributeMapping(string attributeName, string sourceName)
        {
            return base.CreateAttributeMapping(attributeName, sourceName);
        }

        #endregion

        #endregion

        public static new IObjectInstance GetInstance(IObjectModel model, object primaryKey)
        {
            return null;
        }

    }
}
