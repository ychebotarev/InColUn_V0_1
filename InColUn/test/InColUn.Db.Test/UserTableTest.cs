using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace InColUn.Db.Test
{
    [TestClass]
    public class UserTableTest
    {
        //MySqlDBContext dbContext;
        //UserTable userTable;

        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            //this.dbContext = new MySqlDBContext(connectionString);
            //this.userTable = new UserTable(dbContext);

            //userTable.DeleteUserById(1);
            //userTable.DeleteUserById(2);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //userTable.DeleteUserById(1);
            //userTable.DeleteUserById(2);
        }

        [TestMethod]
        public void UserTableAddLocalUser()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            var dbContext = new MySqlDBContext(connectionString);
            var userTable = new UserTable(dbContext);

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);

            var result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            result.Should().BeTrue("First call");

            result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            result.Should().BeFalse("Same insert paraemters");

            result = userTable.CreateLocalUser(1, "test1", "test1", "test1@test1.com");
            result.Should().BeFalse("Same ID");

            result = userTable.CreateLocalUser(2, "test", "test1", "test@test.com");
            result.Should().BeFalse("Same login string");

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);
        }

        [TestMethod]
        public void UserTableFindUserById()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            var dbContext = new MySqlDBContext(connectionString);
            var userTable = new UserTable(dbContext);

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);

            userTable.FindUserById(1);

            var result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            result.Should().BeTrue("First call");

            var user = userTable.FindUserById(1);

            user.Should().NotBeNull();
            user.Id.Should().Be(1);
            user.login_string.Should().Be("test@test.com");
            user.email.Should().Be("test@test.com");
            user.display_name.Should().Be("test");

            user = userTable.FindUserById(2);
            user.Should().BeNull();

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);
        }

        [TestMethod]
        public void UserTableFindUserByName()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            var dbContext = new MySqlDBContext(connectionString);
            var userTable = new UserTable(dbContext);

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);

            var result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            result.Should().BeTrue("First call");

            var user = userTable.FindUserByLoginString("test@test.com");

            user.Should().NotBeNull();
            user.Id.Should().Be(1);
            user.login_string.Should().Be("test@test.com");
            user.email.Should().Be("test@test.com");
            user.display_name.Should().Be("test");

            user = userTable.FindUserByLoginString("test1@test.com"); ;
            user.Should().BeNull();

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);
        }

        [TestMethod]
        public void UserTableAddExternalUser()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            var dbContext = new MySqlDBContext(connectionString);
            var userTable = new UserTable(dbContext);

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);

            var result = userTable.CreateExternalUser(1, "G1", "test", "test@test.com", "G");
            result.Should().BeTrue("First call");

            result = userTable.CreateExternalUser(1, "G1", "test", "test@test.com", "G");
            result.Should().BeFalse("Same insert paraemters");

            result = userTable.CreateExternalUser(1, "G2", "test", "test@test.com", "G");
            result.Should().BeFalse("Same ID");

            result = userTable.CreateExternalUser(2, "G", "test", "test@test.com", "G");
            result.Should().BeFalse("Same login string");

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);
        }
    }
}
