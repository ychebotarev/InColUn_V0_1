﻿using System.Collections.Generic;
using InColUn.Db.Models;

namespace InColUn.Db
{
    public class BoardsTableService : BasicTableService
    {
        public BoardsTableService(IDbContext dbContext):base(dbContext)
        {
        }

        public Board FindBoardById(long Id)
        {
            var query = string.Format("select * from boards where id = {0}", Id);
            return this.QuerySingleOrDefault<Board>(query);
        }

        public IEnumerable<Board> GetBoards(long boardid)
        {
            var query = string.Format("select * from boards where boardid = {0}", boardid);
            return this.Query<Board>(query);
        }

        public bool CreateBoard(long boardId, string title)
        {
            var insertQuery = "INSERT INTO boards (id, title, boardid)" +
                " VALUES (@id,@title,@id)";
            return this.ExecuteQuery(insertQuery, new
            {
                id = boardId,
                title = title
            });
        }

        public void DeleteBoardById(long Id)
        {
            var deleteQuery = string.Format("DELETE FROM boards WHERE id = {0}", Id);
            this.Execute(deleteQuery);
        }

        public bool CreateSection(long id, string title, long? parentId, long? boardId )
        {
            var insertQuery = "INSERT INTO boards (id, title, parentid, boardid)" +
                " VALUES (@id,@title, @parentid, @boardid)";
            return this.ExecuteQuery(insertQuery, new
            {
                id=id,
                parentId=parentId,
                boardid = boardId,
                title = title
            });
        }

        public bool SetBoardStatus(long id, string status)
        {
            var updateQuery = "UPDATE boards SET status = @status WHERE id = @id";

            return this.ExecuteQuery(updateQuery, new
            {
                id = id,
                status = status
            });
        }
    }
}
