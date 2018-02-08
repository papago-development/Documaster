/*********************************************************/
-- v2

CREATE TABLE dbo.Customer (
	Id INT IDENTITY(1,1) NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,[Address] NVARCHAR(100)
)
GO

ALTER TABLE dbo.Customer
	ADD CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Customer_Name
	ON dbo.Customer ([Name])
GO


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


CREATE TABLE dbo.ProjectType (
	Id INT IDENTITY(1,1) NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
)
GO

ALTER TABLE dbo.ProjectType
	ADD CONSTRAINT PK_ProjectType PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_ProjectType_Name
	ON dbo.ProjectType ([Name])
GO


CREATE TABLE dbo.Project (
	Id INT IDENTITY(1,1) NOT NULL
	,CustomerId INT NOT NULL
	,ProjectTypeId INT NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
)
GO

ALTER TABLE dbo.Project
	ADD CONSTRAINT PK_Project 
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE dbo.Project WITH CHECK
	ADD CONSTRAINT FK_Project_CustomerId_Customer_Id
	FOREIGN KEY (CustomerId)
	REFERENCES dbo.Customer (Id)
GO

ALTER TABLE dbo.Project
	CHECK CONSTRAINT FK_Project_CustomerId_Customer_Id
GO

ALTER TABLE dbo.Project WITH CHECK
	ADD CONSTRAINT FK_Project_ProjectTypeId_ProjectType_Id
	FOREIGN KEY (ProjectTypeId)
	REFERENCES dbo.ProjectType (Id)
GO

ALTER TABLE dbo.Project
	CHECK CONSTRAINT FK_Project_ProjectTypeId_ProjectType_Id
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Project_CustomerId_Name
	ON dbo.Project (CustomerId, [Name])
GO


CREATE TABLE dbo.Requirement (
	Id INT IDENTITY(1,1) NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
)
GO

ALTER TABLE dbo.Requirement
	ADD CONSTRAINT PK_Requirement 
	PRIMARY KEY CLUSTERED (Id)
GO


CREATE TABLE dbo.ProjectTypeRequirement (
	Id INT IDENTITY(1,1) NOT NULL
	,ProjectTypeId INT NOT NULL
	,RequirementId INT NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
)

ALTER TABLE dbo.ProjectTypeRequirement
	ADD CONSTRAINT PK_ProjectTypeRequirement
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE dbo.ProjectTypeRequirement WITH CHECK
	ADD CONSTRAINT FK_ProjectTypeRequirement_ProjectTypeId_ProjectType_Id
	FOREIGN KEY (ProjectTypeId)
	REFERENCES dbo.ProjectType (Id)
GO

ALTER TABLE dbo.ProjectTypeRequirement
	CHECK CONSTRAINT FK_ProjectTypeRequirement_ProjectTypeId_ProjectType_Id
GO

ALTER TABLE dbo.ProjectTypeRequirement WITH CHECK
	ADD CONSTRAINT FK_ProjectTypeRequirement_RequirementId_Requirement_Id
	FOREIGN KEY (RequirementId)
	REFERENCES dbo.Requirement (Id)
GO

ALTER TABLE dbo.ProjectTypeRequirement
	CHECK CONSTRAINT FK_ProjectTypeRequirement_RequirementId_Requirement_Id
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_ProjectTypeRequirement_ProjectTypeId_RequirementId
	ON dbo.ProjectTypeRequirement (ProjectTypeId, RequirementId)
GO


CREATE TABLE dbo.InputDocument (
	Id INT IDENTITY(1,1) NOT NULL
	,ProjectId INT NOT NULL
	,RequirementId INT NOT NULL
	,[Name] NVARCHAR(100) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
)
GO

ALTER TABLE dbo.InputDocument
	ADD CONSTRAINT PK_InputDocument
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE dbo.InputDocument WITH CHECK
	ADD CONSTRAINT FK_InputDocument_ProjectId_Project_Id
	FOREIGN KEY (ProjectId)
	REFERENCES dbo.Project (Id)
GO

ALTER TABLE dbo.InputDocument
	CHECK CONSTRAINT FK_InputDocument_ProjectId_Project_Id
GO

ALTER TABLE dbo.InputDocument WITH CHECK
	ADD CONSTRAINT FK_InputDocument_RequirementId_Requirement_Id
	FOREIGN KEY (RequirementId)
	REFERENCES dbo.Requirement (Id)
GO

ALTER TABLE dbo.InputDocument
	CHECK CONSTRAINT FK_InputDocument_ProjectId_Project_Id
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_InputDocument_ProjectId_RequirementId_Name
	ON dbo.InputDocument (ProjectId, RequirementId, [Name])
GO


CREATE TABLE dbo.OutputDocument (
	Id INT IDENTITY(1,1) NOT NULL
	,ProjectId INT NOT NULL
	,RequirementId INT NOT NULL
	,[Name] NVARCHAR(100) NOT NULL
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
	CHECK CONSTRAINT FK_OutputDocument_ProjectId_Project_Id
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_OutputDocument_ProjectId_RequirementId_Name
	ON dbo.OutputDocument (ProjectId, RequirementId, [Name])
GO