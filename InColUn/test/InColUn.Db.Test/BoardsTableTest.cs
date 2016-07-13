using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace InColUn.Db.Test
{
    [TestClass]
    public class BoardsTableTest
    {
        MySqlDBContext dbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            this.dbContext = new MySqlDBContext(connectionString);

            var userTable = new UserTableService(dbContext);
            var boardsTable = new BoardsTableService(dbContext);

            this.dbContext.AddTableService(userTable);
            this.dbContext.AddTableService(boardsTable);

            userTable.CreateLocalUser(1, "test", "test", "test@test.com");

            boardsTable.DeleteBoardById(1);
            boardsTable.DeleteBoardById(2);
            boardsTable.DeleteBoardById(3);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var userTable = this.dbContext.GetTableService<UserTableService>();
            var boardsTable = this.dbContext.GetTableService<BoardsTableService>();

            userTable.DeleteUserById(1);

            boardsTable.DeleteBoardById(1);
            boardsTable.DeleteBoardById(2);
            boardsTable.DeleteBoardById(3);
        }

        [TestMethod]
        public void BoardCreateAcceptanceTest()
        {
            var boardsTable = this.dbContext.GetTableService<BoardsTableService>();

            var result = boardsTable.CreateBoard(1, "title1");
            result.Should().BeTrue();

            result = boardsTable.CreateBoard(1, "title2");
            result.Should().BeFalse("Same id");

            result = boardsTable.CreateBoard(2, "title1");
            result.Should().BeTrue("Same title should be ok");

            Action action1 = () => boardsTable.DeleteBoardById(1);
            action1.ShouldNotThrow("Delete existing board.");

            
            action1.ShouldNotThrow("Delete board that doesn't exist.");

            Action action2 = () => boardsTable.DeleteBoardById(2);
            action1.ShouldNotThrow("Delete existing board.");
        }

        [TestMethod]
        public void BoardSecionCreateAcceptanceTest()
        {
            var boardsTable = this.dbContext.GetTableService<BoardsTableService>();

            var result = boardsTable.CreateBoard(1, "1");
            result.Should().BeTrue();

            result = boardsTable.CreateSection(2, "1_2", 1, 1);
            result.Should().BeTrue();

            result = boardsTable.CreateSection(3, "1_2_3", 2, 1);
            result.Should().BeTrue();

            var boards = boardsTable.GetBoards(1).ToList();
            boards.Count.Should().Be(3);

            var first = boards.FirstOrDefault(x => x.id == 1);
            first.Should().NotBeNull();
            first.id.Should().Be(1);
            first.Title.Should().Be("1");
            first.parentid.Should().Be(0);
            first.boardid.Should().Be(1);
            first.status.Should().Be("P");

            var second = boards.FirstOrDefault(x => x.id == 2);
            second.Should().NotBeNull();
            second.id.Should().Be(2);
            second.Title.Should().Be("1_2");
            second.parentid.Should().Be(1);
            second.boardid.Should().Be(1);
            second.status.Should().Be("P");

            var third = boards.FirstOrDefault(x => x.id == 3);
            third.Should().NotBeNull();
            third.id.Should().Be(3);
            third.Title.Should().Be("1_2_3");
            third.parentid.Should().Be(2);
            third.boardid.Should().Be(1);
            third.status.Should().Be("P");

            var board = boardsTable.FindBoardById(3);
            board.Should().NotBeNull();
            board.id.Should().Be(3);
            board.Title.Should().Be("1_2_3");
            board.parentid.Should().Be(2);
            board.boardid.Should().Be(1);
            board.status.Should().Be("P");
        }
    }
}