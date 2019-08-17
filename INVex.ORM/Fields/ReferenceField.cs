using INVex.ORM.Fields.Base;
using INVex.ORM.Objects;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Fields
{
    /// <summary>
    /// Contains another ObjectInstance
    /// </summary>
    public class ReferenceField : ObjectField, IReferenceField
    {
        private bool referenceLoaded = false;
        private ObjectInstance cachedInstance = null;

        public virtual ObjectInstance Reference
        {
            get
            {
                if (!this.referenceLoaded)
                {
                    //TODO: Доделать
                    this.Model = ObjectInstance.GetModel(new Objects.Common.ObjectModelKey(this.ObjectType));
                    this.cachedInstance = (ObjectInstance)ObjectInstance.GetInstance(this.Model, this.Value);
                    this.referenceLoaded = true;
                }

                return this.cachedInstance;
            }
        }

        /// <summary>
        /// Тип ссылочного объекта
        /// </summary>
        public string ObjectType { get; }
        /// <summary>
        /// Модель ссылочного объекта
        /// </summary>
        public IObjectModel Model { get; protected set; }

        public ReferenceField()
        {

        }

        public ReferenceField(string objectType)
        {
            this.ObjectType = objectType;
        }
    }
}
