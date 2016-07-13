using System;
using System.Collections.Generic;
using Helpers;
using InColUn.Db.Models;
using Dapper;

namespace InColUn.Db
{
    public class BoardsTableService : BasicTableService
    {
        public BoardsTableService(MySqlDBContext dbContext):base(dbContext)
        {
        }

        public Board FindBoardById(long Id)
        {
            var connection = this._dbContext.GetDbConnection();
            var query = string.Format("select * from boards where id = {0}", Id);
            var board = connection.QuerySingleOrDefault<Board>(query);

            return board;
        }

        public IEnumerable<Board> GetBoards(long boardid)
        {
            var connection = this._dbContext.GetDbConnection();
            var query = string.Format("select * from boards where boardid = {0}", boardid);
            var boards = connection.Query<Board>(query);
            return boards;
        }

        public bool CreateBoard(long boardId, string title)
        {
            var insertQuery = "INSERT INTO boards (id, title, boardid)" +
                " VALUES (@id,@title,@id)";
            return this.ExecuteInsert(insertQuery, new
            {
                id = boardId,
                title = title
            });
        }

        public void DeleteBoardById(long Id)
        {
            var deleteQuery = string.Format("DELETE FROM boards WHERE id = {0}", Id);
            this._dbContext.GetDbConnection().Execute(deleteQuery);
        }


        public bool CreateSection(long id, string title, long? parentId, long? boardId )
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
    }
}
