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

-- Added column CustomizeTabId to OutputDocuments table --
ALTER TABLE OutputDocument 
     ADD CustomizeTabId INT 
GO

UPDATE OutputDocument
       SET CustomizeTabId=1
GO

ALTER TABLE OutputDocument ALTER COLUMN CustomizeTabId INT NOT NULL
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
