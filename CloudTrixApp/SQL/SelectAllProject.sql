--***********************************************************
--SELECT Stored Procedure for Project table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectSelectAll] 
GO

CREATE PROCEDURE [ProjectSelectAll]
AS 
BEGIN 
SELECT 
      [Project].[ProjectID]
     ,[Project].[ProjectName]
     ,[Project].[BillingName]
     ,[Project].[Description]
     ,[Project].[Location]
     ,[Project].[StartDate]
     ,[Project].[EndDate]
     ,[Project].[ProjectStatusID]
     ,[Project].[ClientID]
     ,[Project].[ArchitectID]
     ,[Project].[CompanyID]
     ,[Project].[AddUserID]
     ,[Project].[AddDate]
     ,[Project].[ArchiveUserID]
     ,[Project].[ArchiveDate]
FROM
     [Project]
END 
GO

GRANT EXECUTE ON [ProjectSelectAll] TO [Public]
GO

 
