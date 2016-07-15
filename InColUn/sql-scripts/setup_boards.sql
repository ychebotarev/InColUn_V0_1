SET FOREIGN_KEY_CHECKS=0;  DROP TABLE IF EXISTS `incolun`.`userboards`; SET FOREIGN_KEY_CHECKS=1; 
SET FOREIGN_KEY_CHECKS=0;  DROP TABLE IF EXISTS `incolun`.`boards`; SET FOREIGN_KEY_CHECKS=1; 
DROP TABLE IF EXISTS `incolun`.`openedboards`;
DROP TABLE IF EXISTS `incolun`.`sections`;

/*
status:
P - private
S - shared
O - open
D - deleted
*/
CREATE TABLE IF NOT EXISTS `incolun`.`boards` 
(
  `id` BIGINT UNSIGNED NOT NULL,
  `parentid` BIGINT UNSIGNED NOT NULL DEFAULT 0,
  `boardid` BIGINT UNSIGNED NOT NULL,
  `title` VARCHAR(255) NOT NULL,
  `created` DATETIME NOT NULL DEFAULT NOW(),
  `updated` DATETIME NOT NULL DEFAULT NOW(),
  `status` ENUM('P', 'S', 'O', 'D') NOT NULL DEFAULT 'P',
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `boardid_idx` (`boardid` ASC)
)
ENGINE = InnoDB;

select * from incolun.users;
select * from incolun.boards;
select * from incolun.userboards;

delete from incolun.users;
delete from incolun.boards;

/*
relation:
O - owner
V - viewer
C - contributer
F - forked (saved public board)
*/
CREATE TABLE IF NOT EXISTS `incolun`.`userboards` 
(
  `userid` BIGINT UNSIGNED NOT NULL,
  `boardid` BIGINT UNSIGNED NOT NULL,
  `relation` ENUM('O', 'F', 'V', 'C', 'S') NOT NULL DEFAULT 'O',
  `timestamp` DATETIME NOT NULL DEFAULT NOW(),
  PRIMARY KEY (`userid`, `boardid`),
  INDEX `userid_idx` (`userid`),
  INDEX `boardid_idx` (`boardid`),
  CONSTRAINT `userid`
    FOREIGN KEY (`userid`)
		REFERENCES `incolun`.`users` (`Id`)
		ON DELETE CASCADE
		ON UPDATE NO ACTION,
  CONSTRAINT `boardid`
    FOREIGN KEY (`boardid`)
		REFERENCES `incolun`.`boards` (`id`)
		ON DELETE CASCADE
		ON UPDATE NO ACTION
)
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `incolun`.`openboards` 
(
  `userid` BIGINT UNSIGNED NOT NULL,
  `boardid` BIGINT UNSIGNED NOT NULL,
  `timestamp` DATETIME NOT NULL DEFAULT NOW(),
  PRIMARY KEY (`userid`),
  INDEX `userid_idx` (`userid`),
  INDEX `boardid_idx` (`boardid`),
 
  CONSTRAINT `boardid`
    FOREIGN KEY (`boardid`)
		REFERENCES `incolun`.`boards` (`id`)
		ON DELETE CASCADE
		ON UPDATE NO ACTION,
  CONSTRAINT `userid`
    FOREIGN KEY (`userid`)
		REFERENCES `incolun`.`users` (`Id`)
		ON DELETE CASCADE
		ON UPDATE NO ACTION
)
ENGINE = InnoDB;

INSERT INTO incolun.boards (boardid, parentid, title) 
   VALUES 
   (0, NULL, 'Statistic and machine learning'),
   (1, 0, 'board 0 section1'),
   (2, 1, 'board 0 section1'),
   (3, 2, 'board 0 section1'),
   (4, 3, 'board 0 section1'),
   
   (10, NULL, 'Misc' ),
   (11, 10, 'board 1 section2'),
   (12, 10, 'board 1 section3'),
   (13, 10, 'board 1 section4'),
   (14, 11, 'board 1 section5'),
   
   (20, NULL, 'Working with Typescript and VS code'),
   (21, 20, 'board 2 section2'),
   (22, 20, 'board 2 section3'),
   (23, 20, 'board 2 section4'),
   (24, 20, 'board 2 section5'),
   
   (30, NULL, 'TODO' ),
   (31, 30, 'board 3 section2'),
   (32, 31, 'board 3 section3'),
   (33, 31, 'board 3 section4'),
   (34, 31, 'board 3 section5'),
   (35, 31, 'board 3 section6'),
   (36, 31, 'board 3 section7'),
   (37, 31, 'board 3 section8'),
   (38, 30, 'board 3 section9'),

   (40, NULL, 'Paris planning'),
   (41, 40, 'board 4 section2'),
   (42, 41, 'board 4 section3'),
   (43, 41, 'board 4 section4'),
   (44, 42, 'board 4 section5');

INSERT INTO incolun.boards (boardid, parentid, title, created, updated, status) 
   VALUES 
   (50, NULL, 'Really really really really really long long long tiiiiiiitle', NOW(), NOW(), 'O'),
   (51, 50, 'board 5 section2', NOW(), NOW(), 'O'),
   (52, 50, 'board 5 section3', NOW(), NOW(), 'O'),
   (53, 52, 'board 5 section4', NOW(), NOW(), 'O'),
   (54, 52, 'board 5 section5', NOW(), NOW(), 'O');
   
INSERT INTO incolun.userboards(userid, boardid, relation, timestamp)
	VALUES 
    (0, 0, 'O', NOW()),
    (0, 10, 'O', NOW()),
    (0, 20, 'O', NOW()),
    
    (1, 30, 'O', NOW()),
    (1, 40, 'O', NOW()),
    (1, 50, 'O', NOW()),
    
    (0, 30, 'C', NOW()),
    (0, 40, 'V', NOW()),
    (0, 50, 'S', NOW());
    
select * from incolun.boards;  
select * from incolun.userboards;  

SELECT a.title, a.boardid from incolun.boards a inner join 
incolun.userboards b on a.boardid = b.boardid
where b.userid = 0


DELIMITER $$
CREATE PROCEDURE `get_tables` (IN userid BIGINT)
BEGIN
SELECT a.title, a.boardid, a.created, a.updated, b.timestamp, b.relation from incolun.boards a inner join 
incolun.userboards b on a.boardid = b.boardid
where b.userid = userid;
END$$
DELIMITER ;

CALL get_tables(0)