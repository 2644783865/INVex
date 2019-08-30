using INVex.ORM.Expressions.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Queries
{
    public class ObjectQuery : BaseQuery
    {
        public override string QueryString { get; set; }
        public override bool NeedTransaction { get; set; }
        public override Dictionary<string, object> QueryParameters { get; set; }

        public ObjectQuery(string modelName)
        {

        }

        public override object Execute()
        {
            return base.Execute();
        }
    }
}
