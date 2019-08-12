using INVex.Core.Fields.Base;
using INVex.Core.Objects;
using INVex.Core.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Fields
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
                    this.cachedInstance = new ObjectInstance();
                    this.referenceLoaded = true;
                    return this.cachedInstance;
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
        public IObjectModel Model { get; }

        public ReferenceField()
        {

        }

        public ReferenceField(string objectType)
        {
            this.ObjectType = objectType;
        }
    }
}
