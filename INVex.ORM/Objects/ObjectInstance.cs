using System;
using System.Collections.Generic;
using System.IO;
using INVex.Common.Serialize.Base;
using INVex.ORM.Holders;
using INVex.ORM.Objects.Base;
using INVex.ORM.Objects.Common;
using INVex.ORM.Objects.Modify.Base;

namespace INVex.ORM.Objects
{
    public class ObjectInstance : IObjectInstance, IBinarySerializable
    {
        // TODO Сделать возможность наследования ObjectInstance

        #region IObjectInstance
        /// <summary>
        /// Модель
        /// *Модель экземпляра объекта
        /// </summary>
        public IObjectModel Model { get; protected set; }

        public IDbTable Table { get; protected set; }

        /// <summary>
        /// Атрибуты объекта
        /// </summary>
        public Dictionary<string, IAttributeModel> Attributes { get; protected set; }

        public IAttributeModel PrimaryKey { get; protected set; }

        /// <summary>
        /// Attributes to read from database first
        /// </summary>
        public Dictionary<IAttributePath, IAttributeModel> RequiredAttributes { get; protected set; }
        #endregion

        public bool IsInherited { get; protected set; }

        /// <summary>
        /// Object name (from model)
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
            this.BeforeParse();
            this.Parse();
            this.AfterParse();
        }

        protected virtual void BeforeParse()
        {

        }

        protected virtual void AfterParse()
        {

        }

        public virtual void Parse()
        {
            throw new NotImplementedException();
        }

        protected virtual IObjectModel FindInheritedModel(string name)
        {
            throw new NotImplementedException();
        }

        protected virtual IAttributeModel ReadAttribute(IAttributePath path)
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
            throw new NotImplementedException();
        }

        public virtual IAttributeModel GetAttributeByPath(IAttributePath path)
        {
            foreach (IAttributeStep step in path.AttributeSteps)
            {
                IAttributeStep currentStep = path.AttributeSteps.Dequeue();
                if (!this.Attributes.ContainsKey(currentStep.Name))
                {
                    throw new Exception(string.Format("В объекте {0} не найден атрибут с именем {1}", currentStep.Name, this.Name));
                }

                if (path.AttributeSteps.Count > 0)
                {
                    if (this.Attributes[currentStep.Name] is IReferenceAttribute)
                    {
                        return ((IReferenceAttribute)this.Attributes[currentStep.Name]).Field.Reference.GetAttributeByPath(path);
                    }
                    else
                    {
                        throw new Exception("Ожидалося атрибут с типом Reference");
                    }
                }
                else
                {
                    return this.GetAttribute(currentStep.Name);
                }
            }

            return null;
        }

        public virtual IAttributeModel GetAttribute(string attributeName)
        {
            if (this.Attributes.ContainsKey(attributeName))
            {
                return this.Attributes[attributeName];
            }
            throw new Exception(string.Format("В описании объекта {0} не найден атрибут с именем {1}", this.Name, attributeName));
        }

        public virtual T GetAttributeValue<T>(string attributeName)
        {
            return this.Attributes[attributeName].Field.GetValue<T>();
        }

        /// <summary>
        /// Устанавливает значение атрибуту.
        /// </summary>
        /// <param name="attributeName">Наименование атрибута</param>
        /// <param name="value">Значение</param>
        public virtual void SetAttributeValue(IAttributePath path, object value)
        {
            this.GetAttributeByPath(path).Field.SetValue(value);
        }


        public virtual void CopyAttributesFrom(IObjectInstance instance)
        {
            foreach (IAttributeModel attributeModel in instance.Attributes.Values)
            {
                this.Attributes.Add(attributeModel.Name, attributeModel);
            }
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

        // TODO: Доделать
        public static IObjectInstance GetInstance(IObjectModel model, object primaryKey)
        {
            throw new NotImplementedException();
        }

        public static IObjectModel GetModel(string modelName)
        {
#warning not implemented
            return ObjectModelsHolder.Current.GetCachedModel("");
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
