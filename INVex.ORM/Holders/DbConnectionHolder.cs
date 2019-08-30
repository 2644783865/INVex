using INVex.Common.Common;
using INVex.ORM.DataBase.Base;
using INVex.ORM.Expressions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Holders
{
    public class DbConnectionHolder : SingletonBase<DbConnectionHolder>
    {
        private IDbConnection connection;

        public void CreateConnection(IDbConnection connection)
        {
            this.connection = connection;
        }

        public IDbConnection GetConnection()
        {
            return this.connection;
        }

        public object ExecuteQuery(IQuery query)
        {
            if (!this.connection.Opened)
            {
                this.connection.Open();
            }

            return this.connection.ExecuteQuery(query);
        }
    }
}
