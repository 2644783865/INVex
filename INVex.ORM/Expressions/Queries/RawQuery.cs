using INVex.ORM.Holders;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Queries
{
    public class RawQuery : BaseQuery
    {
        public override string QueryString { get; protected set; }
        public override bool NeedTransaction { get; set; }
        public override Dictionary<string, object> QueryParameters { get; set; }


        public RawQuery(string query, bool transaction = false, Dictionary<string, object> queryParams = null)
        {
            this.QueryString = query;
            this.NeedTransaction = transaction;
            this.QueryParameters = queryParams;
        }
    }
}
