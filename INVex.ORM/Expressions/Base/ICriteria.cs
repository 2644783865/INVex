using INVex.ORM.Expressions.Modify;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Base
{
    public interface ICriteria
    {
        IObjectInstance Owner { get; }
        void SetOwner(IObjectInstance objectInstance);
        List<ICondition> Conditions { get; }
        bool IsTrue();
        SqlComputedString ToSql();
    }
}
