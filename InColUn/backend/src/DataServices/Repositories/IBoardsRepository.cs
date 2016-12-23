using System;
using System.Collections.Generic;
using InColUn.Data.Models;

namespace InColUn.Data.Repositories
{
    public interface IBoardsRepository
    {
        Board FindBoardById(long Id);

        IEnumerable<Board> GetBoards(long boardid);

        bool CreateBoard(long boardId, string title);

        void DeleteBoardById(long Id);

        bool CreateSection(long id, string title, long? parentId, long? boardId);

        bool SetBoardStatus(long id, string status);
    }
}
