using System.Data;
 
namespace InColUn.DB
{
    public interface IDbContext
    {
        IDbConnection DbConnection { get; }
    }
}
