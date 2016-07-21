USE incolun
GO 

IF EXISTS (SELECT name FROM sys.tables WHERE name = 'boards') 
   DROP TABLE boards; 
GO 

if not exists (select * from sysobjects where name='boards' and xtype='U')
    create table boards (
		id BIGINT NOT NULL
		,parentid BIGINT NOT NULL DEFAULT 0
		,boardid BIGINT NOT NULL
		,title VARCHAR(255) NOT NULL
		,created DATETIME NOT NULL DEFAULT GETDATE()
		,updated DATETIME NOT NULL DEFAULT GETDATE()
		,status char NOT NULL DEFAULT 'P' /* ('P' - 'Private', 'S' - 'Shared', 'O' - 'Opene', 'D' - 'Deleted') */
    )
go

ALTER TABLE boards ADD CONSTRAINT PK_boards_id PRIMARY KEY CLUSTERED  (id)
go

CREATE NONCLUSTERED INDEX IX_boards_boardid ON boards (boardid)
GO

/*
//////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
*/

IF EXISTS (SELECT name FROM sys.tables WHERE name = 'userboards') 
   DROP TABLE userboards; 
GO 

/*
relation:
O - owner
V - viewer
C - contributer
F - forked (saved public board)
*/
if not exists (select * from sysobjects where name='userboards' and xtype='U')
    create table userboards (
		userid BIGINT NOT NULL
		,boardid BIGINT NOT NULL
		,relation char NOT NULL DEFAULT 'O' 
		,timestamp DATETIME NOT NULL DEFAULT GETDATE()
    )
go


CREATE CLUSTERED INDEX IX_userboards_userid ON userboards (userid)
GO

CREATE NONCLUSTERED INDEX IX_userboards_boardid ON userboards (boardid)
GO

/*
//////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
*/
IF EXISTS (SELECT name FROM sys.tables WHERE name = 'openboards') 
   DROP TABLE openboards; 
GO 

if not exists (select * from sysobjects where name='openboards' and xtype='U')
    create table openboards (
		userid BIGINT NOT NULL
		,boardid BIGINT NOT NULL
		,timestamp DATETIME NOT NULL DEFAULT GETDATE()
    )
go

CREATE CLUSTERED INDEX IX_openboards_userid ON openboards (userid)
GO

CREATE NONCLUSTERED INDEX IX_openboards_boardid ON openboards (boardid)
GO

/*
DELIMITER $$
CREATE PROCEDURE `get_tables` (IN userid BIGINT)
BEGIN
SELECT a.title, a.boardid, a.created, a.updated, b.timestamp, b.relation from incolun.boards a inner join 
incolun.userboards b on a.boardid = b.boardid
where b.userid = userid;
END$$
DELIMITER ;

CALL get_tables(0)*/


delete from boards;
select * from boards;

INSERT INTO userboards (userid, boardid, relation) VALUES(1,1, 'O');
INSERT INTO userboards (userid, boardid, relation) VALUES(2,2, 'O');
INSERT INTO userboards (userid, boardid, relation) VALUES(1,2, 'C');
INSERT INTO userboards (userid, boardid, relation) VALUES(2,1, 'C');

select * from userboards where userid = 920683931 and boardid = 920683930


MERGE userboards USING (VALUES (1,1, 'G') ) AS source(userid, boardid, relation) 
ON userboards.userid = source.userid AND userboards.boardid = source.boardid AND source.userid = 1 AND source.boardid = 1
WHEN MATCHED THEN
   UPDATE SET relation = source.relation, timestamp = GETDATE()
WHEN NOT MATCHED THEN
   INSERT(userid, boardid, relation) VALUES(source.userid, source.boardid, source.relation);