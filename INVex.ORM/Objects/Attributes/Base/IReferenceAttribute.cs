using INVex.ORM.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Attributes.Base
{
    public interface IReferenceAttribute
    {
        ReferenceField Field { get; }
    }
}
