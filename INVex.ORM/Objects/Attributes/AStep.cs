using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Attributes
{
    public class AStep : IAttributeStep
    {
        public string Name { get; private set; }
        public IAttributeModel Attribute { get; set; }

        public IObjectInstance OwnerInstance { get; private set; }

        public AStep(string attributeName)
        {
            this.Name = attributeName;
        }

        //public AStep()

        public IAttributeModel ProcessElement(IObjectInstance objectInstance)
        {
            if(objectInstance == null)
            {
                objectInstance = this.OwnerInstance;
            }

            return objectInstance.GetAttribute(this.Name);
        }

        public IAttributeModel ProcessElement()
        {
            return this.OwnerInstance.GetAttribute(this.Name);
        }

        public void SetOwnerInstance(IObjectInstance objectInstance)
        {
            this.OwnerInstance = objectInstance;
        }
    }
}
