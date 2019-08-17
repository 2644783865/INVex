using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Modify
{
    public class AttributeStep : IAttributeStep
    {
        public string Name { get; private set; }

        public AttributeStep(string name)
        {
            this.Name = name;
        }
    }
}
