using System;
using Xunit;

namespace InColUn.Db.Test
{
    public class UserTableTest
    {
        MySqlDBContext dbContext;
        UserTableService userTable;

        public void TestInitialize()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            this.dbContext = new MySqlDBContext(connectionString, null);
            this.userTable = new UserTableService(dbContext);

            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);
        }

        public void TestCleanup()
        {
            userTable.DeleteUserById(1);
            userTable.DeleteUserById(2);
        }

        [Fact]
        public void UserTableAddLocalUser()
        {
            var result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            Assert.True(result,"First call");

            result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            Assert.False(result,"Same insert paraemters");

            result = userTable.CreateLocalUser(1, "test1", "test1", "test1@test1.com");
            Assert.False(result,"Same ID");

            result = userTable.CreateLocalUser(2, "test", "test1", "test@test.com");
            Assert.False(result,"Same login string");

            result = userTable.CreateLocalUser(2, "'test2", "'test", "'test2@test.com or '1");
            Assert.True(result);
        }

        [Fact]
        public void UserTableFindUserById()
        {
            Action action = () => userTable.FindUserById(1);
            var ex = Record.Exception(() => userTable.FindUserById(1));
            Assert.Null(ex);
            
            var result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            Assert.True(result,"First call");

            var user = userTable.FindUserById(1);
            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
            Assert.Equal("test@test.com", user.login_string);
            Assert.Equal("test@test.com", user.email);
            Assert.Equal("test", user.display_name);

            user = userTable.FindUserById(2);
            Assert.Null(user);
        }

        [Fact]
        public void UserTableFindUserByName()
        {
            var result = userTable.CreateLocalUser(1, "test", "test", "test@test.com");
            Assert.True(result,"First call");

            var user = userTable.FindUserByLoginString("test@test.com");
            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
            Assert.Equal("test@test.com", user.login_string);
            Assert.Equal("test@test.com", user.email);
            Assert.Equal("test", user.display_name);

            user = userTable.FindUserByLoginString("test1@test.com"); ;
            Assert.Null(user);

            user = userTable.FindUserByLoginString("'test1@test.com or '1");
            Assert.Null(user);
        }

        [Fact]
        public void UserTableAddExternalUser()
        {
            var result = userTable.CreateExternalUser(1, "G1", "test", "test@test.com", "G");
            Assert.True(result,"First call");

            result = userTable.CreateExternalUser(1, "G1", "test", "test@test.com", "G");
            Assert.False(result,"Same insert paraemters");

            result = userTable.CreateExternalUser(1, "G2", "test", "test@test.com", "G");
            Assert.False(result,"Same ID");

            result = userTable.CreateExternalUser(2, "G1", "test", "test@test.com", "G");
            Assert.False(result,"Same login string");
        }
    }
}