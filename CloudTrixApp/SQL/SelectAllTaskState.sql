--***********************************************************
--SELECT Stored Procedure for TaskState table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskStateSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskStateSelectAll] 
GO

CREATE PROCEDURE [TaskStateSelectAll]
AS 
BEGIN 
SELECT 
      [TaskState].[TaskStateID]
     ,[TaskState].[TaskStateName]
FROM
     [TaskState]
END 
GO

GRANT EXECUTE ON [TaskStateSelectAll] TO [Public]
GO

 
