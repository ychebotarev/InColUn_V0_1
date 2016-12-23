using System.Collections.Generic;
using InColUn.Data.Models;

namespace InColUn.Data.Repositories
{
    public enum UserBoardRelations
    {
        Owner = 0,
        Forked = 1,
        Viewer = 2,
        Contributer = 3
    }

    public interface IUserBoardRepository
    {
        bool CreateUserBoard(long userId, long boardid, UserBoardRelations ubRelation);

        long GetBoardOwnerId(long boardid);

        void DeleteUserBoard(long userId, long boardId);

        UserBoard FindUserBoard(long userId, long boardId);

        IEnumerable<long> GetUserBoardsIds(long userId);

        IEnumerable<long> GetUserBoardsIds(long userId, UserBoardRelations ubRelation);

        IEnumerable<long> GetUserOpenBoardsIds(long userId);

        bool OpenBoard(long userId, long boardid);

        void CloseBoard(long userId, long boardid);
    }
}
