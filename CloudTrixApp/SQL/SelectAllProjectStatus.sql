--***********************************************************
--SELECT Stored Procedure for ProjectStatus table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectStatusSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectStatusSelectAll] 
GO

CREATE PROCEDURE [ProjectStatusSelectAll]
AS 
BEGIN 
SELECT 
      [ProjectStatus].[ProjectStatusID]
     ,[ProjectStatus].[ProjectStatusName]
FROM
     [ProjectStatus]
END 
GO

GRANT EXECUTE ON [ProjectStatusSelectAll] TO [Public]
GO

 
