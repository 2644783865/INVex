using System;
using System.Collections.Generic;
using System.IO;
using INVex.Common.Serialize.Base;
using INVex.ORM.Expressions.Objects;
using INVex.ORM.Holders;
using INVex.ORM.Objects.Attributes;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using INVex.ORM.Objects.Common;

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
        public Dictionary<IAttributePath, IAttributeModel> RequiredAttributes { get; protected set; } = new Dictionary<IAttributePath, IAttributeModel>();
        #endregion

        public bool IsInherited { get; protected set; }

        /// <summary>
        /// Object name (from model)
        /// </summary>
        public string ModelName
        {
            get
            {
                return this.Model.Name;
            }
        }

        public bool IsNew { get; set; }

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

        #region parse

        protected virtual void BeforeParse()
        {

        }
        public virtual void Parse()
        {
            throw new NotImplementedException();
        }

        protected virtual void AfterParse()
        {
            this.DefineRequiredAttributes();
        }


        #endregion

        protected virtual IObjectModel FindInheritedModel(string name)
        {
            throw new NotImplementedException();
        }

        protected virtual IAttributeModel CreateAttribute(string attributeName, string attributeType, string description)
        {
            throw new NotImplementedException();
        }

        public virtual IAttributeModel GetAttribute(IAttributePath path)
        {
            return PathProcessor.ProcessPath(path, this);
        }

        public virtual IAttributeModel GetAttribute(string attributeName)
        {
            if (this.Attributes.ContainsKey(attributeName))
            {
                return this.Attributes[attributeName];
            }
            throw new Exception(string.Format("В описании объекта {0} не найден атрибут с именем {1}", this.ModelName, attributeName));
        }

        protected virtual IAttributeMapping CreateAttributeMapping(string attributeName, string sourceName)
        {
            if (!this.Attributes.ContainsKey(attributeName))
            {
                throw new Exception(string.Format("Маппирование объекта {0} неверно. Используется атрибут, который не указан в секции Fields ({1})", this.ModelName, attributeName));
            }
            return new AttributeMapping(attributeName, sourceName);
        }

        protected virtual void DefineRequiredAttributes()
        {
            this.AddRequiredAttribute(this.PrimaryKey);
        }

        public void AddRequiredAttribute(IAttributeModel attributeModel)
        {
            if(attributeModel.Owner.Model.Id != this.Model.Id)
            {
                throw new Exception("Try to add unknown attribute to required");
            }
            this.AddRequiredAttribute(new APath(new AStep(attributeModel.Name)));
        }

        public virtual void AddRequiredAttribute(IAttributePath path)
        {
            IAttributeModel tempModel = this.GetAttribute(path);
            this.RequiredAttributes.Add(path, tempModel);
        }

        /// <summary>
        /// Устанавливает значение атрибуту.
        /// </summary>
        /// <param name="attributeName">Наименование атрибута</param>
        /// <param name="value">Значение</param>
        public virtual void SetAttributeValue(IAttributePath path, object value)
        {
            this.GetAttribute(path).Field.SetValue(value);
        }

        public virtual void SetAttributeValue(string attributeName, object value)
        {
            this.Attributes[attributeName].Field.SetValue(value);
        }

        public virtual IAttributeModel GetAttributeByMappingColumn(string columnName)
        {
            foreach(IAttributeModel model in this.Attributes.Values)
            {
                if(model.Mapping.ColumnName == columnName)
                {
                    return model;
                }
            }
            throw new Exception("Attribute not found");
        }

        public virtual void CopyAttributesFrom(IObjectInstance instance)
        {
            foreach (IAttributeModel attributeModel in instance.Attributes.Values)
            {
                this.Attributes.Add(attributeModel.Name, attributeModel);
            }
        }

        public virtual void Save()
        {
            ObjectModelsHolder.Current.Holder.SaveObject(this);
        }

        public static IObjectInstance CreateInstance(string modelName)
        {
            IObjectInstance newInstance = ObjectModelsHolder.Current.Holder.CreateInstance(modelName);
            newInstance.IsNew = true;
            return newInstance;
        }

        public static IObjectInstance CreateInstance(IObjectModel model)
        {
             return ObjectInstance.CreateInstance(model.Name);
        }

        public static IObjectInstance GetInstance(IObjectModel model, object primaryKey)
        {
            throw new NotImplementedException();
        }

        public static IObjectModel GetModel(string modelName)
        {
            return ObjectModelsHolder.Current.Holder.GetModel(modelName);
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
