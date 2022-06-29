CREATE DATABASE BaoOnline
ON
USE BaoOnline
GO

CREATE TABLE Roles(

RoleID INT IDENTITY(1,1) PRIMARY KEY,
RoleName  NVARCHAR(255),
RoleDescription NVARCHAR(255)
)
GO

CREATE TABLE Accounts(

AccountID INT IDENTITY(1,1) PRIMARY KEY,
FullName NVARCHAR(50),	
Email NVARCHAR(150),	
Phone NVARCHAR(50),	
Password NVARCHAR(50),	
Salt VARCHAR(10), -- mã hoá password	
Active BIT	NOT NULL,
CreatedDate DATETIME,	
RoleID	INT FOREIGN KEY REFERENCES Roles(RoleID),
LastLogin DATETIME	
)
GO

CREATE TABLE Categories(

CatID INT IDENTITY(1,1) PRIMARY KEY,
CatName NVARCHAR(255),
Title NVARCHAR(255),
Alias NVARCHAR(255), -- hỗ trợ trong vấn đề sale
MetaDesc NVARCHAR(255),
MetaKey NVARCHAR(255),
Img NVARCHAR(255),
Published BIT, -- trạng thái public hay ko public
Ordering INT,
Parent INT, -- dùng để phân cấp danh mục
Levels INT,
Icon NVARCHAR(250),
Cover NVARCHAR(255),
DESCRIPTION NVARCHAR(MAX)
)
GO

CREATE TABLE Posts(

PostID INT IDENTITY(1, 1) PRIMARY KEY,
Title NVARCHAR(255),
ShortContents NVARCHAR(255),
Contents NVARCHAR(255),
Img NVARCHAR(255),
Alias  NVARCHAR(255),
CreateDate DATETIME,
Author NVARCHAR(255), 
AccountID INT,
Tag NVARCHAR(max),
CatID INT,
isHost BIT,
isNewfeed BIT
)
GO


-- 3/ Tạo khoá ngoại

ALTER TABLE Accounts
ADD CONSTRAINT FK_Accounts_Roles
FOREIGN KEY (RoleID) REFERENCES Roles(RoleID); 
GO

ALTER TABLE Posts
ADD CONSTRAINT FK_Accounts_Posts
FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID); 
GO

ALTER TABLE Posts
ADD CONSTRAINT FK_Categories_Posts
FOREIGN KEY (CatID) REFERENCES Categories(CatID);
GO