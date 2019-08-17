using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Fields.Base
{
    public interface IReferenceField
    {
        string ObjectType { get; }
        IObjectModel Model { get; }
    }
}
