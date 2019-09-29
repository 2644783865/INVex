using INVex.ORM.Expressions.Objects;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System.Collections.Generic;

namespace INVex.ORM.Objects.Attributes
{
    public class APath : IAttributePath
    {
        public List<IAttributeStep> Steps { get; private set; }
        public IObjectInstance OwnerInstance { get; private set; }        

        public APath(params AStep[] step)
        {
            this.Steps = new List<IAttributeStep>();

            for(int i = 0; i < step.Length; i++)
            {
                this.Steps.Add(step[i]);
            }
        }

        public IAttributeModel ProcessElement(IObjectInstance objectInstance)
        {
            if(objectInstance == null)
            {
                objectInstance = this.OwnerInstance;
            }

            return PathProcessor.ProcessPath(this, objectInstance);
        }

        public IAttributeModel ProcessElement()
        {
            throw new System.NotImplementedException();
        }

        public void SetOwnerInstance(IObjectInstance objectInstance)
        {
            this.OwnerInstance = objectInstance;
        }
    }
}
