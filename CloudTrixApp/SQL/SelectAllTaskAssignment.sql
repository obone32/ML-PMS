--***********************************************************
--SELECT Stored Procedure for TaskAssignment table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAssignmentSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAssignmentSelectAll] 
GO

CREATE PROCEDURE [TaskAssignmentSelectAll]
AS 
BEGIN 
SELECT 
      [TaskAssignment].[TaskAssignmentID]
     ,[TaskAssignment].[AssignmentDate]
     ,[TaskAssignment].[TaskID]
     ,[TaskAssignment].[EmployeeID]
     ,[TaskAssignment].[TaskStateID]
FROM
     [TaskAssignment]
END 
GO

GRANT EXECUTE ON [TaskAssignmentSelectAll] TO [Public]
GO

 
