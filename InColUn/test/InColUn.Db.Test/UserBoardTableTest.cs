using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using InColUn.Db;


namespace InColUn.Db.Test
{
    [TestClass]
    class UserBoardTableTest
    {
        UserTableService userTable;
        BoardsTableService boardsTable;
        UserBoardTableService userBoardTable;

        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
            var dbContext = new MySqlDBContext(connectionString);
            this.userTable = new UserTableService(dbContext);
            this.boardsTable = new BoardsTableService(dbContext);
            this.userBoardTable = new UserBoardTableService(dbContext);

            userTable.CreateLocalUser(1, "test1", "test2", "test1@test.com");
            userTable.CreateLocalUser(2, "test2", "test2", "test2@test.com");

            this.boardsTable.DeleteBoardById(1);
            this.boardsTable.DeleteBoardById(2);
            this.boardsTable.DeleteBoardById(3);
            this.boardsTable.DeleteBoardById(4);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.userBoardTable.DelectUserBoard(1, 1);
            this.userBoardTable.DelectUserBoard(2, 1);
            this.userBoardTable.DelectUserBoard(2, 4);

            this.userTable.DeleteUserById(1);
            this.userTable.DeleteUserById(2);

            this.boardsTable.DeleteBoardById(1);
            this.boardsTable.DeleteBoardById(2);
            this.boardsTable.DeleteBoardById(3);
            this.boardsTable.DeleteBoardById(4);
        }

        [TestMethod]
        public void UserBoardAcceptanceTest()
        {

        }
    }
}
