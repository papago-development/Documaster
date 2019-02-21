/*********************************************************/
-- v2


CREATE TABLE dbo.Project (
	Id INT IDENTITY(1,1) NOT NULL
	,Expire DATETIME  
	,ProjectData VARBINARY(max)
	,[Name] NVARCHAR(50) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
)
GO

ALTER TABLE dbo.Project
	ADD CONSTRAINT PK_Project 
	PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Project_Name
	ON dbo.Project ( [Name])
GO

--v3
CREATE TABLE dbo.Customer (
	Id INT  NOT NULL

	,[Name] NVARCHAR(50) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,Telephone NVARCHAR(10)
	,Email NVARCHAR(50)
	,[Address] NVARCHAR(100)
)
GO


ALTER TABLE dbo.Customer WITH CHECK
	ADD CONSTRAINT FK_Customer_Id_Project_Id
	FOREIGN KEY (Id)
	REFERENCES dbo.Project (Id)
GO

ALTER TABLE dbo.Customer
	CHECK CONSTRAINT FK_Customer_Id_Project_Id
GO




ALTER TABLE dbo.Customer
	ADD CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (Id)
GO

--CREATE UNIQUE NONCLUSTERED INDEX UX_Customer_Name
--	ON dbo.Customer ([Name])
--GO


CREATE TABLE dbo.Template (
	Id INT IDENTITY(1,1) NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,[Text] NVARCHAR(MAX)
)
GO

ALTER TABLE dbo.Template
	ADD CONSTRAINT PK_Template PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Template_Name
	ON dbo.Template ([Name])
GO

CREATE TABLE dbo.Category (
	Id INT IDENTITY(1,1) NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
)
GO

ALTER TABLE dbo.Category
	ADD CONSTRAINT PK_Category
	PRIMARY KEY CLUSTERED (Id)
GO





CREATE TABLE dbo.Requirement (
	Id INT IDENTITY(1,1) NOT NULL
	,CategoryId INT NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL

)
GO

ALTER TABLE dbo.Requirement
	ADD CONSTRAINT PK_Requirement 
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE dbo.Requirement WITH CHECK
	ADD CONSTRAINT FK_Requirement_CategoryId_Category_Id
	FOREIGN KEY (CategoryId)
	REFERENCES dbo.Category (Id)
GO

ALTER TABLE dbo.Requirement
	CHECK CONSTRAINT FK_Requirement_CategoryId_Category_Id
GO

CREATE TABLE dbo.ProjectRequirement (
	Id INT IDENTITY(1,1) NOT NULL
	,ProjectId INT NOT NULL
	,RequirementId INT NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
)

ALTER TABLE dbo.ProjectRequirement
	ADD CONSTRAINT PK_ProjectRequirement
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE dbo.ProjectRequirement WITH CHECK
	ADD CONSTRAINT FK_ProjectRequirement_ProjectId_Project_Id
	FOREIGN KEY (ProjectId)
	REFERENCES dbo.Project (Id)
GO

ALTER TABLE dbo.ProjectRequirement
	CHECK CONSTRAINT FK_ProjectRequirement_ProjectId_Project_Id
GO

ALTER TABLE dbo.ProjectRequirement WITH CHECK
	ADD CONSTRAINT FK_ProjectRequirement_RequirementId_Requirement_Id
	FOREIGN KEY (RequirementId)
	REFERENCES dbo.Requirement (Id)
GO

ALTER TABLE dbo.ProjectRequirement
	CHECK CONSTRAINT FK_ProjectRequirement_RequirementId_Requirement_Id
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_ProjectRequirement_ProjectId_RequirementId
	ON dbo.ProjectRequirement (ProjectId, RequirementId)
GO


CREATE TABLE dbo.OutputDocument (
	Id INT IDENTITY(1,1) NOT NULL
	,ProjectId INT NOT NULL
	,RequirementId INT  NULL
	,ContentType VARCHAR(200) NULL
	,DocumentType VARCHAR(200) NOT NULL
	,[Name] NVARCHAR(100) NOT NULL
	,DocumentData VARBINARY(max) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,IsReady BIT NOT NULL CONSTRAINT DF_IsReady DEFAULT(0)
)
GO

ALTER TABLE dbo.OutputDocument
	ADD CONSTRAINT PK_OutputDocument
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE dbo.OutputDocument WITH CHECK
	ADD CONSTRAINT FK_OutputDocument_ProjectId_Project_Id
	FOREIGN KEY (ProjectId)
	REFERENCES dbo.Project (Id)
GO

ALTER TABLE dbo.OutputDocument
	CHECK CONSTRAINT FK_OutputDocument_ProjectId_Project_Id
GO

ALTER TABLE dbo.OutputDocument WITH CHECK
	ADD CONSTRAINT FK_OutputDocument_RequirementId_Requirement_Id
	FOREIGN KEY (RequirementId)
	REFERENCES dbo.Requirement (Id)
GO

ALTER TABLE dbo.OutputDocument
	CHECK CONSTRAINT FK_OutputDocument_RequirementId_Requirement_Id
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_OutputDocument_ProjectId_RequirementId_Name
	ON dbo.OutputDocument (ProjectId, RequirementId, [Name])
GO

-- Add new row for manually changing of note --
ALTER TABLE dbo.OutputDocument Add 
	IsReady BIT NOT NULL CONSTRAINT DF_IsReady DEFAULT(0)
GO

