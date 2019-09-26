using INVex.ORM.Expressions.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Modify
{
    public class Criteria : ICriteria, ICondition
    {
        public List<ICondition> Conditions { get; private set; } = new List<ICondition>();        
        public CriteriaType CriteriaType { get; private set; }
        public IObjectInstance Owner { get; private set; }

        #region ctor
        public Criteria()
        {

        }

        public Criteria(params ICondition[] conditions)
        {
            this.CriteriaType = CriteriaType.AND;
            foreach (ICondition condition in conditions)
            {
                this.Conditions.Add(condition);
            }
        }

        public Criteria(CriteriaType criteriaType, params ICondition[] conditions)
        {
            this.CriteriaType = criteriaType;
            foreach (ICondition condition in conditions)
            {
                this.Conditions.Add(condition);
            }
        }
        #endregion

        public void SetOwner(IObjectInstance objectInstance)
        {
            this.Owner = objectInstance;
        }

        public bool IsTrue()
        {
            bool result = true;
            foreach (ICondition condition in this.Conditions)
            {
                switch (this.CriteriaType)
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

        public SqlComputedString ToSql()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string criteriaOperator = string.Empty;
            Dictionary<string, object> sqlParams = new Dictionary<string, object>();

            switch (this.CriteriaType)
            {
                case CriteriaType.AND:
                    criteriaOperator = "AND";
                    break;
                case CriteriaType.OR:
                    criteriaOperator = "OR";
                    break;
            }

            for(int i = 0; i<this.Conditions.Count; i++)
            {
                this.Conditions[i].SetOwner(this.Owner);

                SqlComputedString sqlComputed = this.Conditions[i].ToSql();

                if(i+1 < this.Conditions.Count)
                {
                    stringBuilder.Append(sqlComputed.Query + " " + criteriaOperator);
                }
                else
                {
                    stringBuilder.Append(sqlComputed.Query + " ");
                }

                foreach (KeyValuePair<string, object> pair in sqlComputed.SqlParameters)
                {
                    sqlParams.Add(pair.Key, pair.Value);
                }
            }

            return new SqlComputedString("("+stringBuilder.ToString()+")", sqlParams);
        }
    }
}
