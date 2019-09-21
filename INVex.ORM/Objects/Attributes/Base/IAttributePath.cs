using INVex.ORM.Objects.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Attributes.Base
{
    public interface IAttributePath
    {
        List<IAttributeStep> Steps { get; }
        IObjectInstance OwnerInstance { get; }
    }
}
