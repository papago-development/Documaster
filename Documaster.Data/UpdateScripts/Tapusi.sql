ALTER TABLE dbo.Project
	ADD AllowNotification BIT NOT NULL CONSTRAINT DF_Project_AllowNotification DEFAULT (0)
GO

CREATE TABLE dbo.CustomizeTab (
    Id INT IDENTITY(1, 1) NOT NULL
    ,[Name] NVARCHAR(200) NOT NULL
    ,[Type] NVARCHAR(25) NOT NULL
    ,Number INT NOT NULL
    ,CreationDate DATETIME NOT NULL
    ,LastUpdate DATETIME NOT NULL
)
GO

ALTER TABLE dbo.CustomizeTab
    ADD CONSTRAINT PK_CustomizeTab
    PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_CustomizeTab_Name
     ON dbo.CustomizeTab ([Name])
GO

INSERT dbo.CustomizeTab VALUES
	('Documente', 'DisplayDocuments', 1, GETDATE(), GETDATE()),
	('Cert. urbanism', 'DisplayDocuments', 2, GETDATE(), GETDATE()),
	('Avize', 'OutputDocuments', 3, GETDATE(), GETDATE()),
	('Foto Teren', 'DisplayDocuments', 4, GETDATE(), GETDATE()),
	('Foto 3D', 'DisplayDocuments', 5, GETDATE(), GETDATE()),
	('Desene', 'DisplayDocuments', 6, GETDATE(), GETDATE()),
	('Note', 'Notes', 7, GETDATE(), GETDATE())
GO

ALTER TABLE OutputDocument 
	ADD CustomizeTabId INT 
GO

UPDATE dbo.OutputDocument
SET DocumentType = 'DisplayDocuments'
WHERE RequirementId IS NULL
GO

UPDATE dbo.OutputDocument
SET CustomizeTabId = 1
WHERE DocumentType = 'Document'
GO

UPDATE dbo.OutputDocument
SET CustomizeTabId = 2
WHERE DocumentType = 'SpecialDocumentType'
GO

UPDATE dbo.OutputDocument
SET CustomizeTabId = 3
WHERE DocumentType = 'OutputDocument'
GO

UPDATE dbo.OutputDocument
SET CustomizeTabId = 4
WHERE DocumentType = 'GroundDrawing'
GO

UPDATE dbo.OutputDocument
SET CustomizeTabId = 5
WHERE DocumentType = 'Photo'
GO

UPDATE dbo.OutputDocument
SET CustomizeTabId = 6
WHERE DocumentType = 'Drawing'
GO

ALTER TABLE OutputDocument 
	ALTER COLUMN CustomizeTabId INT NOT NULL
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


CREATE TABLE dbo.ProjectTemplate (
	Id INT IDENTITY(1,1) NOT NULL
   ,ProjectId INT NOT NULL
   ,[Name] NVARCHAR(200) NOT NULL
   ,CreationDate DATETIME NOT NULL
   ,LastUpdate DATETIME NOT NULL
   ,[Text] VARCHAR(MAX) NOT NULL
)
GO

ALTER TABLE dbo.ProjectTemplate
	ADD CONSTRAINT PK_ProjectTemplate
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE dbo.ProjectTemplate WITH CHECK
	ADD CONSTRAINT FK_ProjectTemplate_ProjectId_Project_Id
	FOREIGN KEY (ProjectId)
	REFERENCES dbo.Project (Id)
	ON DELETE CASCADE
GO

ALTER TABLE dbo.ProjectTemplate
	CHECK CONSTRAINT FK_ProjectTemplate_ProjectId_Project_Id
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_ProjectTemplate_ProjectId_Name
	ON dbo.ProjectTemplate (ProjectId, [Name])
GO
