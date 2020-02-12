--***********************************************************
--SELECT Stored Procedure for Task table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskSelect] 
GO

CREATE PROCEDURE [TaskSelect] 
      @TaskID int
AS 
BEGIN 
SELECT 
      [TaskID]
     ,[TaskName]
     ,[Description]
     ,[CreationDate]
FROM
     [Task]
WHERE
    [TaskID] = @TaskID
END 
GO

GRANT EXECUTE ON [TaskSelect] TO [Public]
GO

 
