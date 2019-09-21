using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Base
{
    public interface ICriteria
    {
        List<ICondition> Conditions { get; }
        bool IsTrue();
        string ToSql();
    }
}
