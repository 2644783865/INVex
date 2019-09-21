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

        public AStep(string attributeName)
        {
            this.Name = attributeName;
        }
    }
}
