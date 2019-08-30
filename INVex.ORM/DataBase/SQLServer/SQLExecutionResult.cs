using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.DataBase.SQLServer
{
    public class SQLExecutionResult
    {
        public string Query { get; set; }
        public List<SQLRow> Rows { get; } = new List<SQLRow>();
    }

    public class SQLRow : Dictionary<string, object>
    {
        
    }
}
