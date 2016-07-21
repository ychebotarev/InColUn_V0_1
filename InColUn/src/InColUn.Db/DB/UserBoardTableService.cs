using System;
using System.Linq;
using System.Collections.Generic;
using InColUn.Db.Models;

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

        public bool CreateUserBoard(long userId, long boardid, UserBoardRelations ubRelation)
        {
            var ownerId = this.GetBoardOwner(boardid);
            if (userId == ownerId) return false;

            if (ownerId != 0 && ubRelation == UserBoardRelations.Owner) return false;
            if (ownerId == 0 && ubRelation != UserBoardRelations.Owner) return false;

            var relation = RelationString[(int)ubRelation];

            string mergeQuery =
@"MERGE userboards 
USING (VALUES (@userid,@boardid, @relation) ) AS source(userid, boardid, relation) 
ON
   userboards.userid = source.userid AND userboards.boardid = source.boardid
WHEN MATCHED THEN
   UPDATE SET relation = source.relation, timestamp = GETDATE()
WHEN NOT MATCHED THEN
   INSERT(userid, boardid, relation) VALUES(source.userid, source.boardid, source.relation);";

            return this.ExecuteQuery(mergeQuery, new
            {
                userid = userId,
                boardid = boardid,
                relation = relation
            });
        }

        public long GetBoardOwner(long boardid)
        {
            var selectQuery = string.Format("SELECT * FROM userboards WHERE boardid = {0} and relation = 'O'", boardid);

            var owners = this.Query<long>(selectQuery);

            if (owners == null) return 0;

            var ownersList = owners.ToList();

            if (ownersList.Count == 0) return 0;

            if(ownersList.Count != 1)
            {
                throw new ArgumentOutOfRangeException(selectQuery);
            }

            return ownersList.First();
        }

        public void DeleteUserBoard(long userId, long boardId)
        {
            //if user was an owner - mark board as deleted
            var userBoard = this.FindUserBoard(userId, boardId);
            if(userBoard != null && userBoard.relation == "O")
            {
                var boardsService = this.GetContext().GetTableService<BoardsTableService>();
                boardsService.SetBoardStatus(boardId, "D");
            }

            var deleteQuery = string.Format("DELETE FROM userboards WHERE userid = {0} and boardid = {1}", userId, boardId);
            this.Execute(deleteQuery);
        }

        public UserBoard FindUserBoard(long userId, long boardId)
        {
            var query = string.Format("select * from userboards where userid = {0} and boardid = {1}", userId, boardId);
            return this.QuerySingleOrDefault<UserBoard>(query);
        }

        public IEnumerable<long> GetUserBoards(long userId)
        {
            var query = string.Format("select boardid from userboards where userid = {0}", userId);
            return this.Query<long>(query);
        }

        public IEnumerable<long> GetUserBoards(long userId, UserBoardRelations ubRelation)
        {
            var relation = RelationString[(int)ubRelation];
            var query = string.Format("select boardid from userboards where userid = {0} and relation = '{1}'", userId, relation);
            return this.Query<long>(query);
        }

        public IEnumerable<long> GetUserOpenedBoards(long userId)
        {
            var query = string.Format("select boardid from openedboards where userid = {0}", userId);
            return this.Query<long>(query);
        }

        public bool OpenBoard(long userId, long boardid)
        {
            var insertQuery = "INSERT INTO openboards (userid, boardid)" +
                " VALUES (@userid,@boardid)";

            return this.ExecuteQuery(insertQuery, new
            {
                userid = userId,
                boardid = boardid
            });
        }

        public void CloseBoard(long userId, long boardid)
        {
            var deleteQuery = string.Format("DELETE FROM openboards WHERE userid = {0} and boardid = {1}", userId, boardid);
            this.Execute(deleteQuery);
        }
    }
}