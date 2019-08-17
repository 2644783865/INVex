using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Base
{
    public interface IDbTable
    {
        string TableName { get; }
        string Prefix { get; }
        string DbName { get; }
    }
}
