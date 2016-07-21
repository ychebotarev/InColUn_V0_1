using System;
using Xunit;
using Xunit.Abstractions;

namespace InColUn.Db.Test
{
    public class UserTableFixture : IDisposable
    {
        public MSSqlDbContext dbContext;
        public UserTableService userTable;

        public UserTableFixture()
        {
            var connectionString = @"Server=localhost\SQLEXPRESS;Database=InColUn;User ID=UserTest;Password=1qaz2wsx;Connection Timeout=30;";
            this.dbContext = new MSSqlDbContext(connectionString, null, null);
            this.userTable = new UserTableService(dbContext);
        }

        public void Dispose()
        {
        }
    }

    public class UserTableTest : IClassFixture<UserTableFixture>
    {
        private readonly ITestOutputHelper output;
        UserTableFixture fixture;

        public UserTableTest(ITestOutputHelper output, UserTableFixture userTableFixture)
        {
            this.fixture = userTableFixture;
            this.output = output;
        }

        [Fact]
        public void AddLocalUser()
        {
            string name = "UserTableAddLocalUser";
            long id = (long)name.GetHashCode();
        
            var userTable = this.fixture.userTable;
            //cleanup possible artifacts
            userTable.DeleteUserById(id);
            userTable.DeleteUserById(id + 1);

            var result = userTable.CreateLocalUser(id, name, name, name + "@test.com");
            Assert.True(result, "First call");

            result = userTable.CreateLocalUser(id, name, name , name + "@test.com");
            Assert.False(result, "Same insert paraemters");

            result = userTable.CreateLocalUser(id, name + "1", name + "1", name + "1@test1.com");
            Assert.False(result, "Same ID");

            result = userTable.CreateLocalUser(id + 1, name + "1" , name + "1", name + "@test.com");
            Assert.False(result, "Same login string");

            result = userTable.CreateLocalUser(id + 1, "'" + name + "2'", "'" + name + "2'", "'" + name + "@test.com or '1");
            Assert.True(result);

            userTable.DeleteUserById(id);
            userTable.DeleteUserById(id+1);
        }

        [Fact]
        public void FindUserById()
        {
            string name = "UserTableFindUserById";
            long id = name.GetHashCode();

            var userTable = this.fixture.userTable;

            Action action = () => userTable.FindUserById(id+1);
            var ex = Record.Exception(() => this.fixture.userTable.FindUserById(id+1));
            Assert.Null(ex);

            var result = userTable.CreateLocalUser(id, name, name , name+"@test.com");
            Assert.True(result, "UserTableFindUserById First call");

            var user = userTable.FindUserById(id);
            Assert.NotNull(user);
            Assert.Equal(id, user.Id);
            Assert.Equal(name+"@test.com", user.login_string);
            Assert.Equal(name + "@test.com", user.email);
            Assert.Equal(name, user.display_name);

            user = userTable.FindUserById(id+1);
            Assert.Null(user);

            userTable.DeleteUserById(id);
        }

        [Fact]
        public void FindUserByName()
        {
            string name = "UserTableFindUserByName";
            long id = name.GetHashCode();
            var userTable = this.fixture.userTable;

            var result = userTable.CreateLocalUser(id, name, name, name+"@test.com");
            Assert.True(result, "First call");

            var user = this.fixture.userTable.FindUserByLoginString(name+"@test.com");
            Assert.NotNull(user);
            Assert.Equal(id, user.Id);
            Assert.Equal(name+"@test.com", user.login_string);
            Assert.Equal(name + "@test.com", user.email);
            Assert.Equal(name, user.display_name);

            user = this.fixture.userTable.FindUserByLoginString(name + "1@test.com"); ;
            Assert.Null(user);

            user = this.fixture.userTable.FindUserByLoginString("'" + name + "1@test.com or '1");
            Assert.Null(user);

            userTable.DeleteUserById(id);
        }

        [Fact]
        public void AddExternalUser()
        {
            string name = "UserTableAddExternalUser";
            long id = name.GetHashCode();
            var userTable = this.fixture.userTable;

            var result = this.fixture.userTable.CreateExternalUser(id, name, "test", "test@test.com", "G");
            Assert.True(result, "First call");

            result = this.fixture.userTable.CreateExternalUser(id, name, "test", "test@test.com", "G");
            Assert.False(result, "Same id and name");

            result = this.fixture.userTable.CreateExternalUser(id, name+"1", "test", "test@test.com", "G");
            Assert.False(result, "Same ID");

            result = this.fixture.userTable.CreateExternalUser(id+1, name, "test", "test@test.com", "G");
            Assert.False(result, "Same login string");

            userTable.DeleteUserById(id);
        }
    }
}