using INVex.ORM.Expressions.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Modify
{
    public class ValueCondition : ICondition
    {
        public ValueCondition(IAttributePath attrubutePath, OperatorType operatorType, object value)
        {

        }

        public virtual string ToSql()
        {
            throw new NotImplementedException();
        }
    }
}
