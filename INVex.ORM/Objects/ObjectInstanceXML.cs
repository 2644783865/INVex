using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using INVex.ORM.Expressions.Modify;
using INVex.ORM.Expressions.Queries;
using INVex.ORM.Objects.Base;
using INVex.ORM.Objects.Modify;

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

                if (modelDoc.Root.Attribute("Inherits") != null)
                {
                    this.IsInherited = true;
                    // TODO: Возможно, в теории, можно так и оставить, если загружать сущности по Id asc. Не представляю себе ситуации, когда нужно наследовать
                    // сущность с Id 5 у сущности Id 2
                    string inheritedModelName = modelDoc.Root.Attribute("Inherits").Value;

                    // Если мы наследуем модель, то секция <Mapping> должна быть
                    // а) Исключена
                    // б) Внесена, но только с полями для новых атрибутов и ссылающиеся на другую таблицу
                    // TODO: Доделать
                    IObjectModel inherited = this.FindInheritedModel(inheritedModelName);
                    this.CopyAttributesFrom(new ObjectInstance(inherited));
                }

                List<XElement> fields = modelDoc.XPathSelectElements("ObjectModel/Fields/*").ToList();
                List<XElement> mapping = modelDoc.XPathSelectElements("ObjectModel/Mapping/*").ToList();

                #region Attributes creation. Section "FIELD"
                foreach (XElement singleField in fields)
                {
                    string attributeName = singleField.Attribute("Name").Value as string;

                    if (string.IsNullOrEmpty(attributeName))
                    {
                        throw new Exception(string.Format("В описании объекта {0} есть неназванный атрибут", this.Name));
                    }

                    if (this.Attributes.ContainsKey(attributeName))
                    {
                        throw new Exception(string.Format("Атрибут с именем {0} уже находится в коллекции аттрибутов объекта {1}", attributeName, this.Name));
                    }

                    string attributeType = singleField.Name.LocalName;

                    IAttributeModel newAttributeInstance = this.CreateAttribute(attributeName, attributeType, singleField.ToString());
                    if (newAttributeInstance == null)
                    {
                        throw new Exception();
                    }

                    this.Attributes.Add(attributeName, newAttributeInstance);
                }
                #endregion

                #region Attribute mappings creation. Section "MAPPING"
                // TODO: Подумать над случаем, если мы наследуем какую либо модель.
                foreach (XElement singleMapping in mapping)
                {
                    XAttribute src = singleMapping.Attribute("Source");
                    XAttribute attrName = singleMapping.Attribute("Attribute");
                    if (src == null || attrName == null)
                    {
                        IXmlLineInfo mappingInfo = singleMapping;
                        throw new Exception(string.Format("Маппирование объекта {0} неверно. Строка №{1}", this.Name, mappingInfo.LineNumber));
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
                    throw new Exception(string.Format("В описании объекта {0} атрибут с именем {1} имеет неизвестный тип {2}", this.Name, attributeName, attributeType));
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
            ObjectInstance tempInstance = new ObjectInstance(model);

            new Query()
            {
                ForObject = model.Name,
                Expresssion =
                new QueryExpression
                (
                    model,
                    new ValueCondition
                    (
                        new AttributePath(new AttributeStep(tempInstance.PrimaryKey.Name)), Expressions.Base.OperatorType.Equal, primaryKey
                    )
                )
            }.Run();
            return null;
        }

    }
}
