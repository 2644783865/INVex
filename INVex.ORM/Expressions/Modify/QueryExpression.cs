using INVex.ORM.Expressions.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Modify
{
    public class QueryExpression : IExpression
    {
        private SortedList<int, ICondition> sortedConditions = new SortedList<int, ICondition>();

        public QueryExpression(IObjectModel owner, params ICondition[] conditions)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                sortedConditions.Add(i, conditions[i]);
            }
        }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
