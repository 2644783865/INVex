using INVex.ORM.DataBase.Base;
using INVex.ORM.Expressions.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace INVex.ORM.DataBase.SQLServer
{
    public class SQLServerConnection : IDbConnection
    {
        public string ConnectionString { get; protected set; }
        public bool Opened { get { return this.sqlConnection == null ? false : this.sqlConnection.State == System.Data.ConnectionState.Open; } }

        private SqlConnection sqlConnection;

        public SQLServerConnection(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void Open()
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.ConnectionString);
                this.sqlConnection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ExecuteQuery(IQuery query)
        {
            SQLExecutionResult result = new SQLExecutionResult();
            SqlTransaction transaction = null;

            bool hasErrors = false;

            try
            {
                if (query.NeedTransaction)
                {
                    transaction = sqlConnection.BeginTransaction();
                }

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandText = query.QueryString;
                result.Query = query.QueryString;

                if(query.QueryParameters != null)
                {
                    foreach (KeyValuePair<string, object> pair in query.QueryParameters)
                    {
                        command.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                }
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            SQLRow row = new SQLRow();
                            for(int i = 0; i < dataReader.FieldCount; i++)
                            {
                                row.Add(dataReader.GetName(i), dataReader.GetValue(i));
                            }
                            result.Rows.Add(row);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                hasErrors = true;
                throw ex;
            }
            finally
            {
                if (query.NeedTransaction)
                {
                    if (hasErrors)
                    {
                        transaction.Rollback();
                    }
                    else
                    {
                        transaction.Commit();
                    }
                }
            }

            return result;
        }
    }
}
