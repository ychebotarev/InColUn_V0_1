using Xunit;

namespace InColUn.Db.Test
{
    public class DbContextTest
    {
        [Fact]
        public void DbContextAddServiceTest()
        {
            var dbContext = new MySqlDBContext("none", null);

            dbContext.AddTableService(new UserTableService(dbContext));
            var userTable = dbContext.GetTableService<UserTableService>();
            Assert.NotNull(userTable);
        }
    }
}