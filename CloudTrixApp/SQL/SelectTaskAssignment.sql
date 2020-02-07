--***********************************************************
--SELECT Stored Procedure for TaskAssignment table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAssignmentSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAssignmentSelect] 
GO

CREATE PROCEDURE [TaskAssignmentSelect] 
      @TaskAssignmentID int
AS 
BEGIN 
SELECT 
      [TaskAssignmentID]
     ,[AssignmentDate]
     ,[TaskID]
     ,[EmployeeID]
     ,[TaskStateID]
FROM
     [TaskAssignment]
WHERE
    [TaskAssignmentID] = @TaskAssignmentID
END 
GO

GRANT EXECUTE ON [TaskAssignmentSelect] TO [Public]
GO

 
