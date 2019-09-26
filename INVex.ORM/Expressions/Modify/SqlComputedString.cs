using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Modify
{
    public class SqlComputedString
    {
        public Dictionary<string, object> SqlParameters { get; set; } = new Dictionary<string, object>();
        public string Query { get; set; }

        public SqlComputedString()
        {

        }

        public SqlComputedString(string resultQuery, Dictionary<string, object> sqlParams)
        {
            this.Query = resultQuery;
            this.SqlParameters = sqlParams;
        }
    }
}
