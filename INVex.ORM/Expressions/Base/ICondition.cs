using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Base
{
    public interface ICondition
    {
        IObjectInstance Owner { get; }
        string ToSql();
        bool IsTrue();
    }
}
