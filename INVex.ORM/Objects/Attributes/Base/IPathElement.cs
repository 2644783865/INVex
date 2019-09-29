using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Attributes.Base
{
    public interface IPathElement
    {
        IObjectInstance OwnerInstance { get; }
        IAttributeModel ProcessElement(IObjectInstance objectInstance);
        IAttributeModel ProcessElement();
        void SetOwnerInstance(IObjectInstance objectInstance);
    }
}
