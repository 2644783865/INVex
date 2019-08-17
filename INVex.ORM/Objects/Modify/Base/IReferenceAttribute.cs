using INVex.ORM.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Modify.Base
{
    public interface IReferenceAttribute
    {
        ReferenceField Field { get; }
    }
}
