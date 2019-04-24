CREATE TABLE dbo.ProjectStatus (
	Id INT IDENTITY(1,1) NOT NULL
	,[Name] NVARCHAR(200) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
    ,Color NVARCHAR(50) NOT NULL
)
GO

ALTER TABLE dbo.ProjectStatus
	ADD CONSTRAINT PK_ProjectStatus 
	PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_ProjectStatus_Name
	ON dbo.ProjectStatus ([Name])
GO


CREATE TABLE dbo.Project (
	Id INT IDENTITY(1,1) NOT NULL
	,Expire DATETIME  
	,ProjectData VARBINARY(MAX)
	,[Name] NVARCHAR(200) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,ProjectStatusId INT NOT NULL
	,Notes NVARCHAR(MAX)
	,[Number] NVARCHAR(50) NOT NULL
)
GO

ALTER TABLE dbo.Project
	ADD CONSTRAINT PK_Project 
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE dbo.Project WITH CHECK
	ADD CONSTRAINT FK_Project_ProjectStatusId_ProjectStatus_Id
	FOREIGN KEY (ProjectStatusId)
	REFERENCES dbo.ProjectStatus (Id)
GO

ALTER TABLE dbo.Project
	CHECK CONSTRAINT FK_Project_ProjectStatusId_ProjectStatus_Id
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Project_Name_Number
	ON dbo.Project ([Name], Number)
GO


CREATE TABLE dbo.Customer (
	Id INT NOT NULL
	,[Name] NVARCHAR(200) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,Telephone NVARCHAR(10)
	,Email NVARCHAR(50)
	,[Address] NVARCHAR(100)
	,AdditionalInfo1 NVARCHAR(100) NULL
	,AdditionalInfo2 NVARCHAR(100) NULL
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
	,[Name] NVARCHAR(200) NOT NULL
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
	,[Name] NVARCHAR(200) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,Number INT NOT NULL
)
GO

ALTER TABLE dbo.Category
	ADD CONSTRAINT PK_Category
	PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Category_Name
	ON dbo.Category ([Name])
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Category_Number
	ON dbo.Category (Number)
GO


CREATE TABLE dbo.Requirement (
	Id INT IDENTITY(1,1) NOT NULL
	,CategoryId INT NOT NULL
	,[Name] NVARCHAR(200) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,Number INT NOT NULL
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

CREATE UNIQUE NONCLUSTERED INDEX UX_Requirement_Name
	ON dbo.Requirement ([Name])
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Requirement_CategoryId_Number
    ON dbo.Requirement (CategoryId, Number)
GO


CREATE TABLE dbo.ProjectRequirement (
	Id INT IDENTITY(1,1) NOT NULL
	,ProjectId INT NOT NULL
	,RequirementId INT NOT NULL
	,IsReady BIT NOT NULL CONSTRAINT DF_ProjectRequirement_IsReady DEFAULT(0)
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
	ON DELETE CASCADE
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
    ,CustomizeTabId INT NOT NULL
	/*,DocumentType VARCHAR(200) NOT NULL*/
	,ContentType VARCHAR(200) NULL
	,[Name] NVARCHAR(200) NOT NULL
	,DocumentData VARBINARY(max) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
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
	ON DELETE CASCADE
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

 ALTER TABLE dbo.OutputDocument WITH CHECK
     ADD CONSTRAINT FK_OutputDocument_CustomizeTabId_CustomizeTab_Id
     FOREIGN KEY (CustomizeTabId)
     REFERENCES dbo.CustomizeTab (Id)
     ON DELETE CASCADE
GO

ALTER TABLE dbo.OutputDocument 
     CHECK CONSTRAINT FK_OutputDocument_CustomizeTabId_CustomizeTab_Id
GO

CREATE TABLE dbo.UserProfile (
	Id INT IDENTITY(1,1) NOT NULL
	,UserName NVARCHAR(50) NOT NULL
	,[Password] NVARCHAR(200) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
)
GO

ALTER TABLE dbo.UserProfile
	ADD CONSTRAINT PK_UserProfile
	PRIMARY KEY CLUSTERED (Id)
GO


CREATE TABLE dbo.CustomizeTab (
    Id INT IDENTITY(1, 1) NOT NULL
    ,[Name] NVARCHAR(200) NOT NULL
    ,Type NVARCHAR(25) NOT NULL
    ,Number INT NOT NULL
    ,CreationDate DATETIME NOT NULL
    ,LastUpdate DATETIME NOT NULL
)
GO

ALTER TABLE dbo.CustomizeTab
    ADD CONSTRAINT  PK_CustomizeTab
    PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_CustomizeTab_Name
     ON dbo.CustomizeTab ([Name])
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_CustomizeTab_Number
     ON dbo.CustomizeTab (Number)
GO