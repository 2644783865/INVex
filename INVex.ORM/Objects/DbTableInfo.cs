using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects
{
    public class DbTableInfo : IDbTable
    {
        public string FullName
        {
            get
            {
                return this.Prefix + this.TableName;
            }
        }
        public string TableName { get; private set; }
        public string Prefix { get; private set; }

        public DbTableInfo(string tbName, string prefix)
        {
            this.TableName = tbName;
            this.Prefix = prefix;
        }
    }
}
