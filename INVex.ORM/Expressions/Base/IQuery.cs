using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Base
{
    public interface IQuery
    {
        string QueryString { get;  }
        bool NeedTransaction { get; set; }
        Dictionary<string, object> QueryParameters { get; set; }
        object Execute();
    }
}
