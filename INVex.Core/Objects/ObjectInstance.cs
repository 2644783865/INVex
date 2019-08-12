using INVex.Core.Objects.Base;
using INVex.Core.Objects.Modify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using INVex.Core.Serialize.Base;
using System.Xml;

namespace INVex.Core.Objects
{
    public class ObjectInstance : IObjectInstance, IBinarySerializable
    {
        // TODO Сделать возможность наследования ObjectInstance

        /// <summary>
        /// Модель
        /// *Модель экземпляра объекта
        /// </summary>
        public IObjectModel Model { get; protected set; }

        public IDbTable Table { get; protected set; }

        public bool IsInherited { get; protected set; }

        /// <summary>
        /// Атрибуты объекта
        /// </summary>
        public Dictionary<string, IAttributeModel> Attributes { get; protected set; }

        /// <summary>
        /// Наименование объекта (из модели)
        /// </summary>
        public string Name
        {
            get
            {
                return this.Model.Name;
            }
        }        

        public ObjectInstance()
        {

        }

        public ObjectInstance(IObjectModel model)
        {
            this.Model = model;
            this.Attributes = new Dictionary<string, IAttributeModel>();
            this.Parse();
        }

        /// <summary>
        /// Default - XML
        /// </summary>
        public virtual void Parse()
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

                    if(this.Attributes.ContainsKey(attributeName))
                    {
                        throw new Exception(string.Format("Атрибут с именем {0} уже находится в коллекции аттрибутов объекта {1}", attributeName, this.Name));
                    }

                    string attributeType = singleField.Name.LocalName;

                    IAttributeModel newAttributeInstance = this.CreateAttribute(attributeName, attributeType, singleField.ToString());
                    if(newAttributeInstance == null)
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
                    if(src == null || attrName == null)
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

        protected virtual IObjectModel FindInheritedModel(string name)
        {
            throw new NotImplementedException();
        }

        protected virtual IAttributeMapping CreateAttributeMapping(string attributeName, string sourceName)
        {
            if (!this.Attributes.ContainsKey(attributeName))
            {
                throw new Exception(string.Format("Маппирование объекта {0} неверно. Используется атрибут, который не указан в секции Fields ({1})", this.Name, attributeName));
            }
            return new AttributeMapping(attributeName, sourceName);
        }

        protected virtual IAttributeModel CreateAttribute(string attributeName, string attributeType, string description)
        {
            switch (attributeType)
            {
                case "Property":
                    return new PropertyAttribute(attributeName, description, "Property", this);
                    
                case "Reference":
                    return new ReferenceAttribute(attributeName, description, "Reference", this);

                case "Choice":
                    return new ChoiceAttribute(attributeName, description, "Choice", this);

                case "Collection":
                    return new CollectionAttribute(attributeName, description, "Collection", this);
                    
                default:
                    throw new Exception(string.Format("В описании объекта {0} атрибут с именем {1} имеет неизвестный тип {2}", this.Name, attributeName, attributeType));
            }
        }

        /// <summary>
        /// Устанавливает значение атрибуту.
        /// </summary>
        /// <param name="attributeName">Наименование атрибута</param>
        /// <param name="value">Значение</param>
        public virtual void SetAttributeValue(string attributeName, object value)
        {
            if (this.Attributes.ContainsKey(attributeName))
            {
                this.Attributes[attributeName].Field.SetValue(value);
            }
            throw new Exception(string.Format("В описании объекта {0} не найден атрибут с именем {1}", this.Name, attributeName));
        }


        public virtual IAttributeModel GetAttribute(string attributeName)
        {
            if (this.Attributes.ContainsKey(attributeName))
            {
                return this.Attributes[attributeName];
            }
            throw new Exception(string.Format("В описании объекта {0} не найден атрибут с именем {1}", this.Name, attributeName));
        }

        public virtual void CopyAttributesFrom(IObjectInstance instance)
        {
            foreach(IAttributeModel attributeModel in instance.Attributes.Values)
            {
                this.Attributes.Add(attributeModel.Name, attributeModel);
            }
        }

        public virtual T GetAttributeValue<T>(string attributeName)
        {
            return this.Attributes[attributeName].Field.GetValue<T>();
        }

        /// <summary>
        /// Создает дубликат объекта, копирует все поля, значения.
        /// </summary>
        /// <returns></returns>
        public ObjectInstance DuplicateInstance()
        {
            ObjectInstance newInstance = new ObjectInstance(this.Model);
            newInstance.Attributes = new Dictionary<string, IAttributeModel>(this.Attributes);
            return newInstance;
        }

        public static ObjectInstance CreateInstance(IObjectModel model)
        {
            return new ObjectInstance(model);
        }

        public virtual void Pack(BinaryWriter writer)
        {
            writer.Write(this.GetType().Name);

            throw new NotImplementedException();
        }

        public virtual void Unpack(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
