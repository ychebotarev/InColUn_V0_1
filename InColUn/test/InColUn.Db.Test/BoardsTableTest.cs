using System;
using System.Linq;
using Xunit;


namespace InColUn.Db.Test
{
    public class BoardsTableFixture : IDisposable
    {
        public MSSqlDbContext dbContext;
        public UserTableService userTable;

        public BoardsTableFixture()
        {
            var connectionString = @"Server=localhost\SQLEXPRESS;Database=InColUn;User ID=UserTest;Password=1qaz2wsx;Connection Timeout=30;";
            this.dbContext = new MSSqlDbContext(connectionString, null, null);

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

    public class BoardsTableTest : IClassFixture<BoardsTableFixture>
    {
        BoardsTableFixture fixture;

        public BoardsTableTest(BoardsTableFixture fixture)
        {
            this.fixture = fixture;
        }


        [Fact]
        public void BoardCreateAcceptanceTest()
        {
            var boardsTable = this.fixture.dbContext.GetTableService<BoardsTableService>();
            string name = "BoardCreateAcceptanceTest";
            long id = name.GetHashCode();

            //cleanup artefacts
            boardsTable.DeleteBoardById(id + 1);
            boardsTable.DeleteBoardById(id);

            var result = boardsTable.CreateBoard(id, name);
            Assert.True(result);

            result = boardsTable.CreateBoard(id, name+"1");
            Assert.False(result, "Should not create board with same id");

            result = boardsTable.CreateBoard(id+1, name);
            Assert.True(result, "Same title should be ok");

            var ex = Record.Exception(() => boardsTable.DeleteBoardById(id));
            Assert.Null(ex);

            ex = Record.Exception(() => boardsTable.DeleteBoardById(id));
            Assert.Null(ex);

            ex = Record.Exception(() => boardsTable.DeleteBoardById(id+1));
            Assert.Null(ex);
        }

        [Fact]
        public void BoardSecionCreateAcceptanceTest()
        {
            string name = "BoardSecionCreateAcceptanceTest";
            long id = name.GetHashCode();
            var boardsTable = this.fixture.dbContext.GetTableService<BoardsTableService>();

            //cleanup artefacts
            boardsTable.DeleteBoardById(id + 2);
            boardsTable.DeleteBoardById(id + 1);
            boardsTable.DeleteBoardById(id);

            var result = boardsTable.CreateBoard(id, name);
            Assert.True(result);

            result = boardsTable.CreateSection(id+1, name + "_2", id, id);
            Assert.True(result);

            result = boardsTable.CreateSection(id+2, name + "_2_3", id+1, id);
            Assert.True(result);

            var boards = boardsTable.GetBoards(id).ToList();
            Assert.Equal(3, boards.Count);

            var first = boards.FirstOrDefault(x => x.id == id);
            Assert.NotNull(first);
            Assert.Equal(id, first.id);
            Assert.Equal(name, first.Title);
            Assert.Equal(0u, first.parentid);
            Assert.Equal(id, first.boardid);
            Assert.Equal("P", first.status);

            var second = boards.FirstOrDefault(x => x.id == id+1);
            Assert.NotNull(second);
            Assert.Equal(id+1, second.id);
            Assert.Equal(name+"_2", second.Title);
            Assert.Equal(id, second.parentid);
            Assert.Equal(id, second.boardid);
            Assert.Equal("P", second.status);

            var third = boards.FirstOrDefault(x => x.id == id+2);
            Assert.NotNull(third);
            Assert.Equal(id+2, third.id);
            Assert.Equal(name + "_2_3", third.Title);
            Assert.Equal(id+1, third.parentid);
            Assert.Equal(id, third.boardid);
            Assert.Equal("P", third.status);

            var board = boardsTable.FindBoardById(id+2);
            Assert.NotNull(board);
            Assert.Equal(id + 2, board.id);
            Assert.Equal(name+"_2_3", board.Title);
            Assert.Equal(id + 1, board.parentid);
            Assert.Equal(id, board.boardid);
            Assert.Equal("P", board.status);

            boardsTable.DeleteBoardById(id + 2);
            boardsTable.DeleteBoardById(id + 1);
            boardsTable.DeleteBoardById(id);
        }
    }
}