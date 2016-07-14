using System.Linq;
using Xunit;

namespace InColUn.Db.Test
{
    public class UserBoardTableTest
    {
        MySqlDBContext dbContext;

        public void TestInitialize()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            this.dbContext = new MySqlDBContext(connectionString, null);

            var userTable = new UserTableService(dbContext);
            var boardsTable = new BoardsTableService(dbContext);
            var userBoardTable = new UserBoardTableService(dbContext);

            dbContext.AddTableService(userTable);
            dbContext.AddTableService(boardsTable);
            dbContext.AddTableService(userBoardTable);

            userTable.CreateLocalUser(1, "test1", "test2", "test1@test.com");
            userTable.CreateLocalUser(2, "test2", "test2", "test2@test.com");

            boardsTable.CreateBoard(1, "1");
            boardsTable.CreateSection(2, "2", 1, 1);
            boardsTable.CreateSection(3, "3", 2, 1);
            boardsTable.CreateBoard(4, "4");
        }

        public void TestCleanup()
        {
            var dbService = this.dbContext.GetTableService<UserBoardTableService>();

            dbService.Execute("delete from users");
            dbService.Execute("delete from userboards");
            dbService.Execute("delete from boards");
        }

        [Fact]
        public void UserBoardAcceptanceTest()
        {
            this.TestInitialize();

            var userBoardTable = this.dbContext.GetTableService<UserBoardTableService>();
            var result = userBoardTable.CreateUserBoard(1,1, UserBoardRelations.Owner);
            Assert.True(result, "First link creation should work");

            result = userBoardTable.CreateUserBoard(2, 1, UserBoardRelations.Owner);
            Assert.False(result, "Should not create new record if board already have an owner.");


            result = userBoardTable.CreateUserBoard(2, 1, UserBoardRelations.Forked);
            Assert.True(result, "Should be able to create a link with different relation");

            result = userBoardTable.CreateUserBoard(2, 1, UserBoardRelations.Contributer);
            Assert.True(result, "Should be able to update link relation");

            result = userBoardTable.CreateUserBoard(2, 1, UserBoardRelations.Owner);
            Assert.False(result, "Should not update to owner relation if board already have a different owner.");

            result = userBoardTable.CreateUserBoard(2, 4, UserBoardRelations.Viewer);
            Assert.False(result, "Should not create new link if board doesn't have an owner.");

            result = userBoardTable.CreateUserBoard(2, 4, UserBoardRelations.Owner);
            Assert.True(result, "Should create owner link since board doesn't have an owner.");

            var userId = userBoardTable.GetBoardOwner(1);
            Assert.Equal(1, userId);
            

            var userBoard = userBoardTable.FindUserBoard(1, 1);
            Assert.NotNull(userBoard);
            Assert.Equal(1, userBoard.userid);
            Assert.Equal(1, userBoard.boardid);
            Assert.Equal("O", userBoard.relation);


            userBoard = userBoardTable.FindUserBoard(2, 1);
            Assert.Equal("C", userBoard.relation);

            userBoard = userBoardTable.FindUserBoard(2, 5);
            Assert.Null(userBoard);
            
            var boards = userBoardTable.GetUserBoards(2);
            Assert.NotNull(boards);

            var boardsList = boards.ToList();
            Assert.Equal(2, boardsList.Count);
            Assert.True(boardsList.Contains(1));
            Assert.True(boardsList.Contains(2));

            this.TestCleanup();
        }
    }
}