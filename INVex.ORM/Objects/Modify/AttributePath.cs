using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Modify
{
    public class AttributePath : IAttributePath
    {
        public Queue<IAttributeStep> AttributeSteps { get; protected set; }

        public AttributePath()
        {
            this.AttributeSteps = new Queue<IAttributeStep>();
        }

        public AttributePath(params IAttributeStep[] attributeSteps)
        {
            this.AttributeSteps = new Queue<IAttributeStep>();
            foreach (IAttributeStep step in attributeSteps)
            {
                this.Add(step);
            }
        }

        public void Add(IAttributeStep attributeStep)
        {
            this.AttributeSteps.Enqueue(attributeStep);
        }
    }
}
