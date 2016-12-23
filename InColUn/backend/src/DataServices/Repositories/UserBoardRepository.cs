using System;
using System.Linq;
using System.Collections.Generic;
using InColUn.DB;
using InColUn.Data.Models;

namespace InColUn.Data.Repositories
{
    public class UserBoardRepository : IUserBoardRepository
    {
        IDbContext dbContext;
        IBoardsRepository boardsRepository;
        public UserBoardRepository(IDbContext dbContext, IBoardsRepository boardsRepository)
        {
            this.dbContext = dbContext;
            this.boardsRepository = boardsRepository;
        }

        public const string OwnerRelation = "O";
        public const string ForkedRelation = "F";
        public const string ViewerRelation = "V";
        public const string ContributerRelation = "C";

        private static string[] RelationString = new string[] { "O", "F", "V", "C" };

        public bool CreateUserBoard(long userId, long boardid, UserBoardRelations ubRelation)
        {
            var ownerId = this.GetBoardOwnerId(boardid);
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

            return dbContext.Execute(mergeQuery, new
            {
                userid = userId,
                boardid = boardid,
                relation = relation
            });
        }

        public long GetBoardOwnerId(long boardid)
        {
            var selectQuery = string.Format("SELECT * FROM userboards WHERE boardid = {0} and relation = 'O'", boardid);

            var owners = dbContext.Query<long>(selectQuery);

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
                boardsRepository.SetBoardStatus(boardId, "D");
            }

            var deleteQuery = string.Format("DELETE FROM userboards WHERE userid = {0} and boardid = {1}", userId, boardId);
            dbContext.Execute(deleteQuery);
        }

        public UserBoard FindUserBoard(long userId, long boardId)
        {
            var query = string.Format("select * from userboards where userid = {0} and boardid = {1}", userId, boardId);
            return dbContext.QuerySingleOrDefault<UserBoard>(query);
        }

        public IEnumerable<long> GetUserBoardsIds(long userId)
        {
            var query = string.Format("select boardid from userboards where userid = {0}", userId);
            return dbContext.Query<long>(query);
        }

        public IEnumerable<long> GetUserBoardsIds(long userId, UserBoardRelations ubRelation)
        {
            var relation = RelationString[(int)ubRelation];
            var query = string.Format("select boardid from userboards where userid = {0} and relation = '{1}'", userId, relation);
            return dbContext.Query<long>(query);
        }

        public IEnumerable<long> GetUserOpenBoardsIds(long userId)
        {
            var query = string.Format("select boardid from openedboards where userid = {0}", userId);
            return dbContext.Query<long>(query);
        }

        public bool OpenBoard(long userId, long boardid)
        {
            var insertQuery = "INSERT INTO openboards (userid, boardid)" +
                " VALUES (@userid,@boardid)";

            return dbContext.Execute(insertQuery, new
            {
                userid = userId,
                boardid = boardid
            });
        }

        public void CloseBoard(long userId, long boardid)
        {
            var deleteQuery = string.Format("DELETE FROM openboards WHERE userid = {0} and boardid = {1}", userId, boardid);
            dbContext.Execute(deleteQuery);
        }

        public IEnumerable<Board> GetUserBoards(long userId, UserBoardRelations ubRelation)
        {
            var query = string.Format("select * from boards where id in (SELECT boardid from userboards where userid = {0} and relation = '{1}')",
                userId,
                RelationString[(int)ubRelation]);
            return dbContext.Query<Board>(query);
        }
    }
}