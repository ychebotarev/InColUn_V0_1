using System;
using System.Linq;
using System.Collections.Generic;
using Helpers;
using InColUn.Db.Models;
using Dapper;

namespace InColUn.Db
{
    public enum UserBoardRelations
    {
        Owner = 0,
        Forked = 1,
        Viewer = 2,
        Contributer = 3
    }

    public class UserBoardTableService : BasicTableService
    {
        public UserBoardTableService(IDbContext dbContext)
            :base(dbContext)
        {
        }

        public const string OwnerRelation = "O";
        public const string ForkedRelation = "F";
        public const string ViewerRelation = "V";
        public const string ContributerRelation = "C";

        private static string[] RelationString = new string[] { "O", "F", "V", "C" };

        public bool CreateUserBoard(ulong userId, ulong boardid, UserBoardRelations ubRelation)
        {
            var ownerId = this.GetBoardOwner(boardid);
            if (userId == ownerId) return false;

            if (ownerId != 0 && ubRelation == UserBoardRelations.Owner) return false;
            if (ownerId == 0 && ubRelation != UserBoardRelations.Owner) return false;

            var relation = RelationString[(int)ubRelation];

            var insertQuery = "INSERT INTO userboards (userid, boardid, relation)" +
                " VALUES (@userid,@boardid, @relation)" +
                " ON DUPLICATE KEY UPDATE" +
                " relation = @relation";

            return this.ExecuteInsert(insertQuery, new
            {
                userid = userId,
                boardid = boardid,
                relation = relation
            });
        }

        public ulong GetBoardOwner(ulong boardid)
        {
            var selectQuery = string.Format("SELECT * FROM userboards WHERE boardid = {0} and relation = 'O'", boardid);
            var owners = this.dbContext.GetDbConnection().Query<ulong>(selectQuery).ToList();

            if (owners.Count == 0) return 0;

            if(owners.Count != 1)
            {
                throw new ArgumentOutOfRangeException(selectQuery);
            }

            return owners.First();
        }

        public void DeleteUserBoard(ulong userId, ulong boardId)
        {
            //if user was an owner - mark board as deleted
            var userBoard = this.FindUserBoard(userId, boardId);
            if(userBoard != null && userBoard.relation == "O")
            {
                var boardsService = this.dbContext.GetTableService<BoardsTableService>();
                boardsService.SetBoardStatus(boardId, "D");
            }

            var deleteQuery = string.Format("DELETE FROM userboards WHERE userid = {0} and boardid = {1}", userId, boardId);
            this.dbContext.GetDbConnection().Execute(deleteQuery);
        }

        public UserBoard FindUserBoard(ulong userId, ulong boardId)
        {
            var connection = this.dbContext.GetDbConnection();
            var query = string.Format("select * from userboards where userid = {0} and boardid = {1}", userId, boardId);
            var userBoard = connection.QuerySingleOrDefault<UserBoard>(query);

            return userBoard;
        }

        public IEnumerable<ulong> GetUserBoards(ulong userId)
        {
            var connection = this.dbContext.GetDbConnection();
            var query = string.Format("select boardid from userboards where userid = {0}", userId);
            var boards = connection.Query<ulong>(query);
            return boards;
        }

        public IEnumerable<long> GetUserBoards(ulong userId, UserBoardRelations ubRelation)
        {
            var relation = RelationString[(int)ubRelation];
            var connection = this.dbContext.GetDbConnection();
            var query = string.Format("select boardid from userboards where userid = {0} and relation = '{1}'", userId, relation);
            var boards = connection.Query<long>(query);
            return boards;
        }

        public IEnumerable<long> GetUserOpenedBoards(ulong userId)
        {
            var connection = this.dbContext.GetDbConnection();
            var query = string.Format("select boardid from openedboards where userid = {0}", userId);
            var boards = connection.Query<long>(query);
            return boards;
        }

        public bool OpenBoard(ulong userId, ulong boardid)
        {
            var insertQuery = "INSERT INTO openboards (userid, boardid)" +
                " VALUES (@userid,@boardid)";

            return this.ExecuteInsert(insertQuery, new
            {
                userid = userId,
                boardid = boardid
            });
        }

        public void CloseBoard(ulong userId, ulong boardid)
        {
            var deleteQuery = string.Format("DELETE FROM openboards WHERE userid = {0} and boardid = {1}", userId, boardid);
            this.dbContext.GetDbConnection().Execute(deleteQuery);
        }
    }
}