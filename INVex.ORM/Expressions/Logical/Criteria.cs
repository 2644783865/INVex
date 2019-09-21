using INVex.ORM.Expressions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Logical
{
    public class Criteria : ICriteria
    {
        public List<ICondition> Conditions { get; } = new List<ICondition>();

        private CriteriaType criteriaType;

        public Criteria(CriteriaType criteriaType, params ICondition[] conditions)
        {
            this.criteriaType = criteriaType;
            foreach(ICondition condition in conditions)
            {
                this.Conditions.Add(condition);
            }
        }

        public bool IsTrue()
        {
            bool result = true;
            foreach(ICondition condition in this.Conditions)
            {
                switch (this.criteriaType)
                {
                    case CriteriaType.AND:
                        if (!condition.IsTrue()) { return false; }
                        break;
                    case CriteriaType.OR:
                        if (condition.IsTrue()) { result = true; }
                        break;
                }
            }
            return result;
        }

        public string ToSql()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string criteriaOperator = string.Empty;

            switch (this.criteriaType)
            {
                case CriteriaType.AND:
                    criteriaOperator = "AND";
                    break;
                case CriteriaType.OR:
                    criteriaOperator = "OR";
                    break;
            }

            foreach(ICondition condition in this.Conditions)
            {
                stringBuilder.Append(condition.ToSql() + " " + criteriaOperator);
            }

            return stringBuilder.ToString().Remove(stringBuilder.Length-criteriaOperator.Length);
        }
    }
}
