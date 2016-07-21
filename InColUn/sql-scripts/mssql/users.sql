USE incolun
GO 

IF EXISTS (SELECT name FROM sys.tables WHERE name = 'users') 
   DROP TABLE users; 
GO 

if not exists (select * from sysobjects where name='users' and xtype='U')
    create table users (
		id BIGINT NOT NULL
		,login_string VARCHAR(255) NOT NULL
		,password_hash VARCHAR(50)
		,salt BIGINT
		,display_name VARCHAR(255) NOT NULL
		,email VARCHAR(255)
		,auth_provider char NOT NULL /*'L', 'F', 'G'*/
		,created DATETIME NOT NULL DEFAULT GETDATE()
		,status char NOT NULL DEFAULT 'N'/* ('N' - 'New', 'A' - 'Active', 'D' - 'Deactivated') */
    )
go

ALTER TABLE users ADD CONSTRAINT PK_users_id PRIMARY KEY CLUSTERED  (id)
go

CREATE UNIQUE NONCLUSTERED INDEX IX_users_login_string ON users (login_string)
GO

INSERT INTO users (id, login_string, password_hash, salt, display_name, email, auth_provider) VALUES (
'18446744072004360354','UserTableAddLocalUser@test.com', '7A4868DC83C09D68A6A8C47285ABCB3E1771C12F163D969058','1123369847', 'UserTableAddLocalUser', 'UserTableAddLocalUser@test.com','L')

