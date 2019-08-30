using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Base
{
    public interface IQuery
    {
        bool NeedTransaction { get; set; }
        string QueryString { get; set; }
        Dictionary<string, object> QueryParameters { get; set; }
        object Execute();
    }
}
