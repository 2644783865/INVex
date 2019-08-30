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
        private ObjectInstance cachedInstance = null;

        public virtual ObjectInstance Reference
        {
            get
            {
                if (this.cachedInstance == null)
                {
                    //TODO: Доделать
                    this.ReferenceModel = ObjectInstance.GetModel(this.ReferenceObjectModelName);
                    this.cachedInstance = (ObjectInstance)ObjectInstance.GetInstance(this.ReferenceModel, this.Value);
                }

                return this.cachedInstance;
            }
        }

        public string ReferenceObjectModelName { get; }
 
        public IObjectModel ReferenceModel { get; protected set; }

        public ReferenceField()
        {

        }

        public ReferenceField(string objectModelName)
        {
            this.ReferenceObjectModelName = objectModelName;
        }
    }
}
