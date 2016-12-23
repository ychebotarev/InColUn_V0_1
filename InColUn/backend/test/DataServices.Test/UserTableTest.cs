using System;
using Xunit;
using Xunit.Abstractions;
using InColUn.DB;
using InColUn.Data.Repositories;

namespace InColUn.DataServices.Test
{
    public class UserTableFixture : IDisposable
    {
        public MSSqlDbContext dbContext;
        public UserRepository userRepository;

        public UserTableFixture()
        {
            var connectionString = @"Server=localhost\SQLEXPRESS;Database=InColUn;User ID=UserTest;Password=1qaz2wsx;Connection Timeout=30;";
            this.dbContext = new MSSqlDbContext(connectionString);
            this.userRepository = new UserRepository(dbContext);
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
        
            //cleanup possible artifacts
            fixture.userRepository.DeleteUserById(id);
            fixture.userRepository.DeleteUserById(id + 1);

            var result = fixture.userRepository.CreateLocalUser(id, name, name, id, name + "@test.com");
            Assert.True(result, "First call");

            result = fixture.userRepository.CreateLocalUser(id, name, name , id, name + "@test.com");
            Assert.False(result, "Same insert paraemters");

            result = fixture.userRepository.CreateLocalUser(id, name + "1", name + "1", id + 1, name + "1@test1.com");
            Assert.False(result, "Same ID");

            result = fixture.userRepository.CreateLocalUser(id + 1, name + "1" , name + "1", id + 1, name + "@test.com");
            Assert.False(result, "Same login string");

            result = fixture.userRepository.CreateLocalUser(id + 1, "'" + name + "2'", "'" + name + "2'", id + 2, "'" + name + "@test.com or '1");
            Assert.True(result);

            fixture.userRepository.DeleteUserById(id);
            fixture.userRepository.DeleteUserById(id+1);
        }

        [Fact]
        public void FindUserById()
        {
            string name = "UserTableFindUserById";
            long id = name.GetHashCode();

            Action action = () => fixture.userRepository.FindUserById(id+1);
            var ex = Record.Exception(() => this.fixture.userRepository.FindUserById(id+1));
            Assert.Null(ex);

            var result = fixture.userRepository.CreateLocalUser(id, name, name , id, name+"@test.com");
            Assert.True(result, "UserTableFindUserById First call");

            var user = fixture.userRepository.FindUserById(id);
            Assert.NotNull(user);
            Assert.Equal(id, user.Id);
            Assert.Equal(name+"@test.com", user.login_string);
            Assert.Equal(name + "@test.com", user.email);
            Assert.Equal(name, user.display_name);

            user = fixture.userRepository.FindUserById(id+1);
            Assert.Null(user);

            fixture.userRepository.DeleteUserById(id);
        }

        [Fact]
        public void FindUserByName()
        {
            string name = "UserTableFindUserByName";
            long id = name.GetHashCode();

            var result = fixture.userRepository.CreateLocalUser(id, name, name, id, name+"@test.com");
            Assert.True(result, "First call");

            var user = fixture.userRepository.FindUserByLoginString(name + "@test.com");
            Assert.NotNull(user);
            Assert.Equal(id, user.Id);
            Assert.Equal(name+"@test.com", user.login_string);
            Assert.Equal(name + "@test.com", user.email);
            Assert.Equal(name, user.display_name);

            user = fixture.userRepository.FindUserByLoginString(name + "1@test.com"); ;
            Assert.Null(user);

            user = fixture.userRepository.FindUserByLoginString("'" + name + "1@test.com or '1");
            Assert.Null(user);

            fixture.userRepository.DeleteUserById(id);
        }

        [Fact]
        public void AddExternalUser()
        {
            string name = "UserTableAddExternalUser";
            long id = name.GetHashCode();

            var result = this.fixture.userRepository.CreateExternalUser(id, name, "test", "test@test.com", "G");
            Assert.True(result, "First call");

            result = this.fixture.userRepository.CreateExternalUser(id, name, "test", "test@test.com", "G");
            Assert.False(result, "Same id and name");

            result = this.fixture.userRepository.CreateExternalUser(id, name+"1", "test", "test@test.com", "G");
            Assert.False(result, "Same ID");

            result = this.fixture.userRepository.CreateExternalUser(id+1, name, "test", "test@test.com", "G");
            Assert.False(result, "Same login string");

            fixture.userRepository.DeleteUserById(id);
        }
    }
}