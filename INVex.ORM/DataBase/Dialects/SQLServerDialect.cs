using INVex.ORM.DataBase.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.DataBase.Dialects
{
    public class SQLServerDialect : IDialect
    {
        public string Select => "SELECT";

        public string Insert => "INSERT";

        public string Update => "UPDATE";

        public string Delete => "DELETE";
    }
}
