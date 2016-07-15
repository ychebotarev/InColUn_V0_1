using System;
using System.Collections.Generic;
using Helpers;
using InColUn.Db.Models;
using Dapper;

namespace InColUn.Db
{
    public class BoardsTableService : BasicTableService
    {
        public BoardsTableService(IDbContext dbContext):base(dbContext)
        {
        }

        public Board FindBoardById(ulong Id)
        {
            var connection = this.dbContext.GetDbConnection();
            var query = string.Format("select * from boards where id = {0}", Id);
            var board = connection.QuerySingleOrDefault<Board>(query);

            return board;
        }

        public IEnumerable<Board> GetBoards(ulong boardid)
        {
            var connection = this.dbContext.GetDbConnection();
            var query = string.Format("select * from boards where boardid = {0}", boardid);
            var boards = connection.Query<Board>(query);
            return boards;
        }

        public bool CreateBoard(ulong boardId, string title)
        {
            var insertQuery = "INSERT INTO boards (id, title, boardid)" +
                " VALUES (@id,@title,@id)";
            return this.ExecuteInsert(insertQuery, new
            {
                id = boardId,
                title = title
            });
        }

        public void DeleteBoardById(ulong Id)
        {
            var deleteQuery = string.Format("DELETE FROM boards WHERE id = {0}", Id);
            this.dbContext.GetDbConnection().Execute(deleteQuery);
        }

        public bool CreateSection(ulong id, string title, ulong? parentId, ulong? boardId )
        {
            var insertQuery = "INSERT INTO boards (id, title, parentid, boardid)" +
                " VALUES (@id,@title, @parentid, @boardid)";
            return this.ExecuteInsert(insertQuery, new
            {
                id=id,
                parentId=parentId,
                boardid = boardId,
                title = title
            });
        }

        public bool SetBoardStatus(ulong id, string status)
        {
            var updateQuery = "UPDATE boards SET status = @status WHERE id = @id";

            return this.ExecuteUpdate(updateQuery, new
            {
                id = id,
                status = status
            });
        }
    }
}
