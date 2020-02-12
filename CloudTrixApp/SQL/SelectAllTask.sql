--***********************************************************
--SELECT Stored Procedure for Task table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskSelectAll] 
GO

CREATE PROCEDURE [TaskSelectAll]
AS 
BEGIN 
SELECT 
      [Task].[TaskID]
     ,[Task].[TaskName]
     ,[Task].[Description]
     ,[Task].[CreationDate]
FROM
     [Task]
END 
GO

GRANT EXECUTE ON [TaskSelectAll] TO [Public]
GO

 
