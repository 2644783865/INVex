using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Base
{
    public interface ICondition
    {
        string ToSql();
    }
}
