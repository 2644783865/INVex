using INVex.ORM.Expressions.Queries.Base;
using INVex.ORM.Holders;
using System;
using System.Collections.Generic;

namespace INVex.ORM.Expressions.Queries
{
    public class BaseQuery : IQuery
    {
        public virtual string QueryString { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public virtual Dictionary<string, object> QueryParameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public virtual bool NeedTransaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual object Execute()
        {
            return DbConnectionHolder.Current.ExecuteQuery(this);
        }
    }
}
