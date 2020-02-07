--***********************************************************
--SELECT Stored Procedure for Project ProjectStatus table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Project_ProjectStatusSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Project_ProjectStatusSelect] 
GO

CREATE PROCEDURE [Project_ProjectStatusSelect]
AS 
BEGIN 
SELECT
     [ProjectStatusID]
    ,[ProjectStatusName]
FROM 
     [ProjectStatus]
END 
GO

GRANT EXECUTE ON [Project_ProjectStatusSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for Project Client table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Project_ClientSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Project_ClientSelect] 
GO

CREATE PROCEDURE [Project_ClientSelect]
AS 
BEGIN 
SELECT
     [ClientID]
    ,[ClientName]
FROM 
     [Client]
END 
GO

GRANT EXECUTE ON [Project_ClientSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for Project Architect table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Project_ArchitectSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Project_ArchitectSelect] 
GO

CREATE PROCEDURE [Project_ArchitectSelect]
AS 
BEGIN 
SELECT
     [ArchitectID]
    ,[ArchitectName]
FROM 
     [Architect]
END 
GO

GRANT EXECUTE ON [Project_ArchitectSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for Project Company table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Project_CompanySelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Project_CompanySelect] 
GO

CREATE PROCEDURE [Project_CompanySelect]
AS 
BEGIN 
SELECT
     [CompanyID]
    ,[CompanyName]
FROM 
     [Company]
END 
GO

GRANT EXECUTE ON [Project_CompanySelect] TO [Public]
GO


 
