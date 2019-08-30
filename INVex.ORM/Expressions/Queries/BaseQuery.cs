using INVex.ORM.Expressions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Queries
{
    public class BaseQuery : IQuery
    {
        public virtual string QueryString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public virtual Dictionary<string, object> QueryParameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public virtual bool NeedTransaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual object Execute()
        {
            throw new NotImplementedException();
        }
    }
}
