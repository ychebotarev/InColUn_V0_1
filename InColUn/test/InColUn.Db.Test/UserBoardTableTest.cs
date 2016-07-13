using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace InColUn.Db.Test
{
    [TestClass]
    public class UserBoardTableTest
    {
        MySqlDBContext dbContext;

        [TestInitialize]
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

        [TestCleanup]
        public void TestCleanup()
        {
            var dbService = this.dbContext.GetTableService<UserBoardTableService>();

            dbService.Execute("delete from users");
            dbService.Execute("delete from userboards");
            dbService.Execute("delete from boards");
        }

        [TestCategory("UserBoard tests"), TestMethod]
        public void UserBoardAcceptanceTest()
        {
            var userBoardTable = this.dbContext.GetTableService<UserBoardTableService>();
            var result = userBoardTable.CreateUserBoard(1,1, UserBoardRelations.Owner);
            result.Should().BeTrue();

            result = userBoardTable.CreateUserBoard(2, 1, UserBoardRelations.Owner);
            result.Should().BeFalse("Should not create new record if board already have an owner.");

            result = userBoardTable.CreateUserBoard(2, 1, UserBoardRelations.Forked);
            result.Should().BeTrue("Should be able to create a link");

            result = userBoardTable.CreateUserBoard(2, 1, UserBoardRelations.Contributer);
            result.Should().BeTrue("Should be able to update link relation");

            result = userBoardTable.CreateUserBoard(2, 1, UserBoardRelations.Owner);
            result.Should().BeFalse("Should not update relation if board already have an owner.");

            result = userBoardTable.CreateUserBoard(2, 4, UserBoardRelations.Viewer);
            result.Should().BeFalse("Should not create new link if board doesn't have an owner.");

            result = userBoardTable.CreateUserBoard(2, 4, UserBoardRelations.Owner);
            result.Should().BeTrue("Should create owner link since board doesn't have an owner.");

            var userId = userBoardTable.GetBoardOwner(1);
            userId.Should().Be(1);

            var userBoard = userBoardTable.FindUserBoard(1, 1);
            userBoard.Should().NotBeNull();
            userBoard.userid.Should().Be(1);
            userBoard.boardid.Should().Be(1);
            userBoard.relation.Should().Be("O");

            userBoard = userBoardTable.FindUserBoard(2, 1);
            userBoard.relation.Should().Be("C");

            userBoard = userBoardTable.FindUserBoard(2, 5);
            userBoard.Should().BeNull();

            var boards = userBoardTable.GetUserBoards(2);
            boards.Should().NotBeNull();

            var boardsList = boards.ToList();
            boardsList.Count.Should().Be(2);
            boardsList.Should().Contain(1);
            boardsList.Should().Contain(4);
        }
    }
}