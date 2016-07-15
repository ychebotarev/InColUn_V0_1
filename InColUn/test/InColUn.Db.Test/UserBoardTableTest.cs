using System;
using System.Linq;
using Xunit;

namespace InColUn.Db.Test
{
    public class UserBoardTableFixture : IDisposable
    {
        public MySqlDBContext dbContext;

        public UserBoardTableFixture()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            this.dbContext = new MySqlDBContext(connectionString, null);

            var userTable = new UserTableService(dbContext);
            var boardsTable = new BoardsTableService(dbContext);
            var userBoardTable = new UserBoardTableService(dbContext);

            this.dbContext.AddTableService(userTable);
            this.dbContext.AddTableService(boardsTable);
            this.dbContext.AddTableService(userBoardTable);
        }

        public void Dispose()
        {

        }
    }

    /*public class UserBoardTableTestContext : IDisposable
    {
        UserBoardTableFixture fixture;

        public UserBoardTableTestContext(UserBoardTableFixture fixture)
        {
            this.fixture = fixture;

            var userTable = this.fixture.dbContext.GetTableService<UserTableService>();

            userTable.CreateLocalUser(1, "test1", "test2", "test1@test.com");
            userTable.CreateLocalUser(2, "test2", "test2", "test2@test.com");

            var boardsTable = this.fixture.dbContext.GetTableService<BoardsTableService>();

            boardsTable.CreateBoard(1, "1");
            boardsTable.CreateSection(2, "2", 1, 1);
            boardsTable.CreateSection(3, "3", 2, 1);
            boardsTable.CreateBoard(4, "4");
        }
        public void Dispose()
        {
            var dbService = this.fixture.dbContext.GetTableService<UserBoardTableService>();

            //dbService.Execute("delete from users");
            dbService.Execute("delete from userboards");
            dbService.Execute("delete from boards");
        }
    }*/

    public class UserBoardTableTest : IClassFixture<UserBoardTableFixture>
    {
        UserBoardTableFixture fixture;

        public UserBoardTableTest(UserBoardTableFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void UserBoardAcceptanceTest()
        {
            string name = "UserTableAddLocalUser";
            ulong id = (ulong)name.GetHashCode();

            var userTable = this.fixture.dbContext.GetTableService<UserTableService>();
            var boardsTable = this.fixture.dbContext.GetTableService<BoardsTableService>();
            var userBoardTable = this.fixture.dbContext.GetTableService<UserBoardTableService>();

            //remove artifacts
            userBoardTable.DeleteUserBoard(id + 1, id + 1);
            userBoardTable.DeleteUserBoard(id + 2, id + 1);
            userBoardTable.DeleteUserBoard(id + 2, id + 4);

            //Setup boards
            userTable.CreateLocalUser(id + 1, "test1", "test2", name+"1@test.com");
            userTable.CreateLocalUser(id + 2, "test2", "test2", name+"2@test.com");

            boardsTable.CreateBoard(id + 1, name+"1");
            boardsTable.CreateSection(id + 2, name+"2", id + 1, id + 1);
            boardsTable.CreateSection(id + 3, name+"3", id + 2, id + 1);
            boardsTable.CreateBoard(id + 4, name+"4");

            var result = userBoardTable.CreateUserBoard(id + 1, id + 1, UserBoardRelations.Owner);
            Assert.True(result, "First link creation should work");

            result = userBoardTable.CreateUserBoard(id + 2, id + 1, UserBoardRelations.Owner);
            Assert.False(result, "Should not create new record if board already have an owner.");


            result = userBoardTable.CreateUserBoard(id + 2, id + 1, UserBoardRelations.Forked);
            Assert.True(result, "Should be able to create a link with different relation");

            result = userBoardTable.CreateUserBoard(id + 2, id + 1, UserBoardRelations.Contributer);
            Assert.True(result, "Should be able to update link relation");

            result = userBoardTable.CreateUserBoard(id + 2, id + 1, UserBoardRelations.Owner);
            Assert.False(result, "Should not update to owner relation if board already have a different owner.");

            result = userBoardTable.CreateUserBoard(id + 2, id + 4, UserBoardRelations.Viewer);
            Assert.False(result, "Should not create new link if board doesn't have an owner.");

            result = userBoardTable.CreateUserBoard(id + 2, id + 4, UserBoardRelations.Owner);
            Assert.True(result, "Should create owner link since board doesn't have an owner.");

            var userId = userBoardTable.GetBoardOwner(id + 1);
            Assert.Equal(id + 1u, userId);

            var userBoard = userBoardTable.FindUserBoard(id + 1, id + 1);
            Assert.NotNull(userBoard);
            Assert.Equal(id + 1u, userBoard.userid);
            Assert.Equal(id + 1u, userBoard.boardid);
            Assert.Equal("O", userBoard.relation);

            userBoard = userBoardTable.FindUserBoard(id + 2, id + 1);
            Assert.Equal("C", userBoard.relation);

            userBoard = userBoardTable.FindUserBoard(2, 5);
            Assert.Null(userBoard);

            var boards = userBoardTable.GetUserBoards(id + 2);
            Assert.NotNull(boards);

            var boardsList = boards.ToList();
            Assert.Equal(2, boardsList.Count);
            Assert.True(boardsList.Contains(id + 1u));
            Assert.True(boardsList.Contains(id + 4u));

            //Cleanup
            userTable.DeleteUserById(id + 1);
            userTable.DeleteUserById(id + 2);

            boardsTable.DeleteBoardById(id + 1);
            boardsTable.DeleteBoardById(id + 2);
            boardsTable.DeleteBoardById(id + 3);
            boardsTable.DeleteBoardById(id + 4);

            userBoardTable.DeleteUserBoard(id + 1, id + 1);
            userBoardTable.DeleteUserBoard(id + 2, id + 1);
            userBoardTable.DeleteUserBoard(id + 2, id + 4);
        }
    }
}