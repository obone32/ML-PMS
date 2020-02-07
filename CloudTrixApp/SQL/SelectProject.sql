--***********************************************************
--SELECT Stored Procedure for Project table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectSelect] 
GO

CREATE PROCEDURE [ProjectSelect] 
      @ProjectID int
AS 
BEGIN 
SELECT 
      [ProjectID]
     ,[ProjectName]
     ,[BillingName]
     ,[Description]
     ,[Location]
     ,[StartDate]
     ,[EndDate]
     ,[ProjectStatusID]
     ,[ClientID]
     ,[ArchitectID]
     ,[CompanyID]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
FROM
     [Project]
WHERE
    [ProjectID] = @ProjectID
END 
GO

GRANT EXECUTE ON [ProjectSelect] TO [Public]
GO

 
