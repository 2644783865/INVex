using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.DataBase.Base
{
    public interface IDialect
    {
        string Select { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }
    }
}
