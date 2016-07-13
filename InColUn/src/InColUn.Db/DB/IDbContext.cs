using System.Data.Common;

namespace InColUn.Db
{
    public interface IDbContext
    {
        DbConnection GetDbConnection();
        void AddTableService<T>(T service) where T : ITableService;
        T GetTableService<T>() where T : class, ITableService;
    }
}
