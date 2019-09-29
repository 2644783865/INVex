using INVex.ORM.Expressions.Queries.Base;

namespace INVex.ORM.DataBase.Base
{
    public interface IDbConnection
    {
        string ConnectionString { get; }
        bool Opened { get; }
        object ExecuteQuery(IQuery query);
        void Open();
    }
}
