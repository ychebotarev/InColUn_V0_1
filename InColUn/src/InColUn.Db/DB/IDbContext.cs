using System.Data.Common;

namespace InColUn.Db
{
    public interface IDbContext
    {
        DbConnection GetDbConnection();
    }
}
