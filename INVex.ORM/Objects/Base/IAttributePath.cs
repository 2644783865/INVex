using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Base
{
    public interface IAttributePath
    {
        Queue<IAttributeStep> AttributeSteps { get; }
    }
}
