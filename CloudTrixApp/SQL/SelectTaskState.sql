--***********************************************************
--SELECT Stored Procedure for TaskState table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskStateSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskStateSelect] 
GO

CREATE PROCEDURE [TaskStateSelect] 
      @TaskStateID int
AS 
BEGIN 
SELECT 
      [TaskStateID]
     ,[TaskStateName]
FROM
     [TaskState]
WHERE
    [TaskStateID] = @TaskStateID
END 
GO

GRANT EXECUTE ON [TaskStateSelect] TO [Public]
GO

 
