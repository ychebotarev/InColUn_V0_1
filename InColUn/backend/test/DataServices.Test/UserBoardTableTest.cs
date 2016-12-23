using System;
using System.Linq;
using Xunit;
using InColUn.Data.Repositories;
using InColUn.Data.Models;

namespace InColUn.DB.Test
{
    public class UserBoardTableFixture : IDisposable
    {
        public MSSqlDbContext dbContext;
        public IUserRepository userRepository;
        public IBoardsRepository boardsRepository;
        public IUserBoardRepository userBoardRepository;

        public UserBoardTableFixture()
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

    public class UserBoardTableTest : IClassFixture<UserBoardTableFixture>
    {
        UserBoardTableFixture fixture;

        public UserBoardTableTest(UserBoardTableFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void AcceptanceTest()
        {
            string name = "UserBoardTableTest_AcceptanceTest";
            long id = name.GetHashCode();

            var userRepository = this.fixture.userRepository;
            var boardsRepository = this.fixture.boardsRepository;
            var userBoardRepository = this.fixture.userBoardRepository;

            //remove artifacts
            userBoardRepository.DeleteUserBoard(id + 1, id + 1);
            userBoardRepository.DeleteUserBoard(id + 2, id + 1);
            userBoardRepository.DeleteUserBoard(id + 2, id + 4);

            //Setup boards
            userRepository.CreateLocalUser(id + 1, "test1", "test2", id + 1, name+"1@test.com");
            userRepository.CreateLocalUser(id + 2, "test2", "test2", id + 2, name + "2@test.com");

            boardsRepository.CreateBoard(id + 1, name+"1");
            boardsRepository.CreateSection(id + 2, name+"2", id + 1, id + 1);
            boardsRepository.CreateSection(id + 3, name+"3", id + 2, id + 1);
            boardsRepository.CreateBoard(id + 4, name+"4");

            var result = userBoardRepository.CreateUserBoard(id + 1, id + 1, UserBoardRelations.Owner);
            Assert.True(result, "First link creation should work");

            result = userBoardRepository.CreateUserBoard(id + 2, id + 1, UserBoardRelations.Owner);
            Assert.False(result, "Should not create new record if board already have an owner.");


            result = userBoardRepository.CreateUserBoard(id + 2, id + 1, UserBoardRelations.Forked);
            Assert.True(result, "Should be able to create a link with different relation");

            result = userBoardRepository.CreateUserBoard(id + 2, id + 1, UserBoardRelations.Contributer);
            Assert.True(result, "Should be able to update link relation");

            result = userBoardRepository.CreateUserBoard(id + 2, id + 1, UserBoardRelations.Owner);
            Assert.False(result, "Should not update to owner relation if board already have a different owner.");

            result = userBoardRepository.CreateUserBoard(id + 2, id + 4, UserBoardRelations.Viewer);
            Assert.False(result, "Should not create new link if board doesn't have an owner.");

            result = userBoardRepository.CreateUserBoard(id + 2, id + 4, UserBoardRelations.Owner);
            Assert.True(result, "Should create owner link since board doesn't have an owner.");

            var userId = userBoardRepository.GetBoardOwnerId(id + 1);
            Assert.Equal(id + 1, userId);

            var userBoard = userBoardRepository.FindUserBoard(id + 1, id + 1);
            Assert.NotNull(userBoard);
            Assert.Equal(id + 1, userBoard.userid);
            Assert.Equal(id + 1, userBoard.boardid);
            Assert.Equal("O", userBoard.relation);

            userBoard = userBoardRepository.FindUserBoard(id + 2, id + 1);
            Assert.Equal("C", userBoard.relation);

            userBoard = userBoardRepository.FindUserBoard(2, 5);
            Assert.Null(userBoard);

            var boards = userBoardRepository.GetUserBoardsIds(id + 2);
            Assert.NotNull(boards);

            var boardsList = boards.ToList();
            Assert.Equal(2, boardsList.Count);
            Assert.True(boardsList.Contains(id + 1));
            Assert.True(boardsList.Contains(id + 4));

            //Cleanup
            userRepository.DeleteUserById(id + 1);
            userRepository.DeleteUserById(id + 2);

            boardsRepository.DeleteBoardById(id + 1);
            boardsRepository.DeleteBoardById(id + 2);
            boardsRepository.DeleteBoardById(id + 3);
            boardsRepository.DeleteBoardById(id + 4);

            userBoardRepository.DeleteUserBoard(id + 1, id + 1);
            userBoardRepository.DeleteUserBoard(id + 2, id + 1);
            userBoardRepository.DeleteUserBoard(id + 2, id + 4);
        }
    }
}