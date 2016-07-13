using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace InColUn.Db.Test
{
    [TestClass]
    public class UserTableTest
    {
        MySqlDBContext dbContext;
        UserTableService userTable;

        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            this.dbContext = new MySqlDBContext(connectionString, null);
            this.userTable = new UserTableService(dbContext);

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);
        }

        [TestMethod]
        public void UserTableAddLocalUser()
        {
            var result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            result.Should().BeTrue("First call");

            result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            result.Should().BeFalse("Same insert paraemters");

            result = userTable.CreateLocalUser(1, "test1", "test1", "test1@test1.com");
            result.Should().BeFalse("Same ID");

            result = userTable.CreateLocalUser(2, "test", "test1", "test@test.com");
            result.Should().BeFalse("Same login string");

            result = userTable.CreateLocalUser(2, "'test2", "'test", "'test2@test.com or '1");
            result.Should().BeTrue();
        }

        [TestMethod]
        public void UserTableFindUserById()
        {
            Action action = () => userTable.FindUserById(1);
            action.ShouldNotThrow();
            
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
        }

        [TestMethod]
        public void UserTableFindUserByName()
        {
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

            user = userTable.FindUserByLoginString("'test1@test.com or '1");
            user.Should().BeNull();
        }

        [TestMethod]
        public void UserTableAddExternalUser()
        {
            var result = userTable.CreateExternalUser(1, "G1", "test", "test@test.com", "G");
            result.Should().BeTrue("First call");

            result = userTable.CreateExternalUser(1, "G1", "test", "test@test.com", "G");
            result.Should().BeFalse("Same insert paraemters");

            result = userTable.CreateExternalUser(1, "G2", "test", "test@test.com", "G");
            result.Should().BeFalse("Same ID");

            result = userTable.CreateExternalUser(2, "G1", "test", "test@test.com", "G");
            result.Should().BeFalse("Same login string");
        }
    }
}