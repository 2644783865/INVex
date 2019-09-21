using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.DataBase.Common
{
    public class ExecutionResult
    {
        public List<RowResult> Rows { get; } = new List<RowResult>();
    }

    public class RowResult : Dictionary<string, object>
    {

    }
}
