using System;
using System.Linq;
using Xunit;


namespace InColUn.Db.Test
{
    public class BoardsTableTest
    {
        MySqlDBContext dbContext;

        public void TestInitialize()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            this.dbContext = new MySqlDBContext(connectionString, null);

            var userTable = new UserTableService(dbContext);
            var boardsTable = new BoardsTableService(dbContext);

            this.dbContext.AddTableService(userTable);
            this.dbContext.AddTableService(boardsTable);

            userTable.CreateLocalUser(1, "test", "test", "test@test.com");

            boardsTable.DeleteBoardById(1);
            boardsTable.DeleteBoardById(2);
            boardsTable.DeleteBoardById(3);
        }

        public void TestCleanup()
        {
            var userTable = this.dbContext.GetTableService<UserTableService>();
            var boardsTable = this.dbContext.GetTableService<BoardsTableService>();

            userTable.DeleteUserById(1);

            boardsTable.DeleteBoardById(1);
            boardsTable.DeleteBoardById(2);
            boardsTable.DeleteBoardById(3);
        }

        [Fact]
        public void BoardCreateAcceptanceTest()
        {
            this.TestInitialize();

            var boardsTable = this.dbContext.GetTableService<BoardsTableService>();

            var result = boardsTable.CreateBoard(1, "title1");
            Assert.True(result);

            result = boardsTable.CreateBoard(1, "title2");
            Assert.False(result, "Should not create board with same id");

            result = boardsTable.CreateBoard(2, "title1");
            Assert.True(result, "Same title should be ok");

            var ex = Record.Exception(() => boardsTable.DeleteBoardById(1));
            Assert.Null(ex);

            ex = Record.Exception(() => boardsTable.DeleteBoardById(1));
            Assert.Null(ex);

            ex = Record.Exception(() => boardsTable.DeleteBoardById(2));
            Assert.Null(ex);

            this.TestCleanup();
        }

        [Fact]
        public void BoardSecionCreateAcceptanceTest()
        {
            this.TestInitialize();

            var boardsTable = this.dbContext.GetTableService<BoardsTableService>();

            var result = boardsTable.CreateBoard(1, "1");
            Assert.True(result);

            result = boardsTable.CreateSection(2, "1_2", 1, 1);
            Assert.True(result);

            result = boardsTable.CreateSection(3, "1_2_3", 2, 1);
            Assert.True(result);


            var boards = boardsTable.GetBoards(1).ToList();
            Assert.Equal(3, boards.Count);

            var first = boards.FirstOrDefault(x => x.id == 1);
            Assert.NotNull(first);
            Assert.Equal(1, first.id);
            Assert.Equal("1", first.Title);
            Assert.Equal(0, first.parentid);
            Assert.Equal(1, first.boardid);
            Assert.Equal("P", first.status);

            var second = boards.FirstOrDefault(x => x.id == 2);
            Assert.NotNull(second);
            Assert.Equal(2, second.id);
            Assert.Equal("1_2", second.Title);
            Assert.Equal(1, second.parentid);
            Assert.Equal(1, second.boardid);
            Assert.Equal("P", second.status);

            var third = boards.FirstOrDefault(x => x.id == 3);
            Assert.NotNull(third);
            Assert.Equal(3, third.id);
            Assert.Equal("1_2_3", third.Title);
            Assert.Equal(2, third.parentid);
            Assert.Equal(1, third.boardid);
            Assert.Equal("P", third.status);

            
            var board = boardsTable.FindBoardById(3);
            Assert.NotNull(board);
            Assert.Equal(3, board.id);
            Assert.Equal("1_2_3", board.Title);
            Assert.Equal(2, board.parentid);
            Assert.Equal(1, board.boardid);
            Assert.Equal("P", board.status);
        }
    }
}