using INVex.Core.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Fields.Base
{
    public interface IReferenceField
    {
        string ObjectType { get; }
        IObjectModel Model { get; }
    }
}
