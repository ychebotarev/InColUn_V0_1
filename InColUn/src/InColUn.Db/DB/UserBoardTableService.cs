using System;
using System.Collections.Generic;
using Helpers;
using InColUn.Db.Models;
using Dapper;

namespace InColUn.Db
{
    public class UserBoardTableService : BasicTableService
    {
        public UserBoardTableService(MySqlDBContext dbContext):base(dbContext)
        {
        }

        public void DelectUserBoard(long userId, long boardId)
        {
            //if user was an owner - mark board as deleted
            var userBoard = this.FindUserBoard(userId, boardId);
            if(userBoard != null && userBoard.relation == "O")
            {
                 
            }

            var deleteQuery = string.Format("DELETE FROM userboards WHERE userid = {0} and boardid = {1}", userId, boardId);
            this._dbContext.GetDbConnection().Execute(deleteQuery);
        }

        public UserBoard FindUserBoard(long userId, long boardId)
        {
            var connection = this._dbContext.GetDbConnection();
            var query = string.Format("select * from userboards where userid = {0} and boardid = {1}", userId, boardId);
            var userBoard = connection.QuerySingleOrDefault<UserBoard>(query);

            return userBoard;
        }

        public IEnumerable<long> GetUserBoards(long userId)
        {
            var connection = this._dbContext.GetDbConnection();
            var query = string.Format("select boardid from userboards where userid = {0}", userId);
            var boards = connection.Query<long>(query);
            return boards;
        }

        public IEnumerable<long> GetUserBoards(long userId, string relation)
        {
            var connection = this._dbContext.GetDbConnection();
            var query = string.Format("select boardid from userboards where userid = {0} and relation = '{1}'", userId, relation);
            var boards = connection.Query<long>(query);
            return boards;
        }

        public IEnumerable<long> GetUserOpenedBoards(long userId)
        {
            var connection = this._dbContext.GetDbConnection();
            var query = string.Format("select boardid from openedboards where userid = {0}", userId);
            var boards = connection.Query<long>(query);
            return boards;
        }

        public bool OpenBoard(long userId, long boardid)
        {
            var insertQuery = "INSERT INTO openboards (userid, boardid)" +
                " VALUES (@userid,@boardid)";

            return this.ExecuteInsert(insertQuery, new
            {
                userid = userId,
                boardid = boardid
            });
        }

        public void CloseBoard(long userId, long boardid)
        {
            var deleteQuery = string.Format("DELETE FROM openboards WHERE userid = {0} and boardid = {1}", userId, boardid);
            this._dbContext.GetDbConnection().Execute(deleteQuery);
        }
    }
}
