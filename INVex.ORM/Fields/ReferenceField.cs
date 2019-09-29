using INVex.ORM.Fields.Base;
using INVex.ORM.Holders;
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
        private IObjectInstance cachedInstance = null;

        public new IObjectInstance Value
        {
            get
            {
                return this.Reference;
            }
        }

        public virtual IObjectInstance Reference
        {
            get
            {
                if(base.Value == null)
                {
                    return ObjectModelsHolder.Current.Holder.CreateInstance(this.ReferenceObjectModelName);
                }

                if (this.cachedInstance == null)
                {
                    this.ReferenceModel = ObjectModelsHolder.Current.Holder.GetModel(this.ReferenceObjectModelName);
                    // base.Value = Object PrimaryKey
                    this.cachedInstance = ObjectModelsHolder.Current.Holder.GetInstance(this.ReferenceModel, base.Value);
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
