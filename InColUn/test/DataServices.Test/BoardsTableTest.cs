using System;
using System.Linq;
using Xunit;
using InColUn.Data.Repositories;
using InColUn.Data.Models;

namespace InColUn.DB.Test
{
    public class BoardsTableFixture : IDisposable
    {
        public MSSqlDbContext dbContext;
        public IUserRepository userRepository;
        public IBoardsRepository boardsRepository;
        public IUserBoardRepository userBoardRepository;

        public BoardsTableFixture()
        {
            var connectionString = @"Server=localhost\SQLEXPRESS;Database=InColUn;User ID=UserTest;Password=1qaz2wsx;Connection Timeout=30;";
            this.dbContext = new MSSqlDbContext(connectionString);

            this.userRepository = new UserRepository(dbContext);
            this.boardsRepository = new BoardsRepository(dbContext);
            this.userBoardRepository = new UserBoardRepository(dbContext, this.boardsRepository);
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
            string name = "BoardCreateAcceptanceTest";
            long id = name.GetHashCode();

            //cleanup artefacts
            fixture.boardsRepository.DeleteBoardById(id + 1);
            fixture.boardsRepository.DeleteBoardById(id);

            var result = fixture.boardsRepository.CreateBoard(id, name);
            Assert.True(result);

            result = fixture.boardsRepository.CreateBoard(id, name+"1");
            Assert.False(result, "Should not create board with same id");

            result = fixture.boardsRepository.CreateBoard(id+1, name);
            Assert.True(result, "Same title should be ok");

            var ex = Record.Exception(() => fixture.boardsRepository.DeleteBoardById(id));
            Assert.Null(ex);

            ex = Record.Exception(() => fixture.boardsRepository.DeleteBoardById(id));
            Assert.Null(ex);

            ex = Record.Exception(() => fixture.boardsRepository.DeleteBoardById(id+1));
            Assert.Null(ex);
        }

        [Fact]
        public void BoardSecionCreateAcceptanceTest()
        {
            string name = "BoardSecionCreateAcceptanceTest";
            long id = name.GetHashCode();

            //cleanup artefacts
            fixture.boardsRepository.DeleteBoardById(id + 2);
            fixture.boardsRepository.DeleteBoardById(id + 1);
            fixture.boardsRepository.DeleteBoardById(id);

            var result = fixture.boardsRepository.CreateBoard(id, name);
            Assert.True(result);

            result = fixture.boardsRepository.CreateSection(id+1, name + "_2", id, id);
            Assert.True(result);

            result = fixture.boardsRepository.CreateSection(id+2, name + "_2_3", id+1, id);
            Assert.True(result);

            var boards = fixture.boardsRepository.GetBoards(id).ToList();
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

            var board = fixture.boardsRepository.FindBoardById(id+2);
            Assert.NotNull(board);
            Assert.Equal(id + 2, board.id);
            Assert.Equal(name+"_2_3", board.Title);
            Assert.Equal(id + 1, board.parentid);
            Assert.Equal(id, board.boardid);
            Assert.Equal("P", board.status);

            fixture.boardsRepository.DeleteBoardById(id + 2);
            fixture.boardsRepository.DeleteBoardById(id + 1);
            fixture.boardsRepository.DeleteBoardById(id);
        }
    }
}