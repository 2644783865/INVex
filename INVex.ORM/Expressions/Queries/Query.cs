using INVex.ORM.Expressions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Queries
{
    public class Query
    {
        public string ForObject { get; set; }
        public IExpression Expresssion { get; set; }

        public Query()
        {

        }

        public void Run()
        {
            // Parse to sql
            // get connection to db
            // execute
        }

    }
}
