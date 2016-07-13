using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace InColUn.Db.Test
{
    [TestClass]
    public class DbContextTest
    {
        [TestMethod]
        public void DbContextAddServiceTest()
        {
            var dbContext = new MySqlDBContext("none", null);

            dbContext.AddTableService(new UserTableService(dbContext));
            var userTable = dbContext.GetTableService<UserTableService>();
            userTable.Should().NotBeNull();
        }
    }
}