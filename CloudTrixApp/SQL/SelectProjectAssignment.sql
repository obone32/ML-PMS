--***********************************************************
--SELECT Stored Procedure for ProjectAssignment table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectAssignmentSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectAssignmentSelect] 
GO

CREATE PROCEDURE [ProjectAssignmentSelect] 
      @ProjectAssignmentID int
AS 
BEGIN 
SELECT 
      [ProjectAssignmentID]
     ,[ProjectID]
     ,[EmployeeID]
     ,[AssignmentDate]
     ,[Remarks]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
FROM
     [ProjectAssignment]
WHERE
    [ProjectAssignmentID] = @ProjectAssignmentID
END 
GO

GRANT EXECUTE ON [ProjectAssignmentSelect] TO [Public]
GO

 
