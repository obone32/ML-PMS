--***********************************************************
--SELECT Stored Procedure for ProjectAssignment table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectAssignmentSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectAssignmentSelectAll] 
GO

CREATE PROCEDURE [ProjectAssignmentSelectAll]
AS 
BEGIN 
SELECT 
      [ProjectAssignment].[ProjectAssignmentID]
     ,[ProjectAssignment].[ProjectID]
     ,[ProjectAssignment].[EmployeeID]
     ,[ProjectAssignment].[AssignmentDate]
     ,[ProjectAssignment].[Remarks]
     ,[ProjectAssignment].[AddUserID]
     ,[ProjectAssignment].[AddDate]
     ,[ProjectAssignment].[ArchiveUserID]
     ,[ProjectAssignment].[ArchiveDate]
FROM
     [ProjectAssignment]
END 
GO

GRANT EXECUTE ON [ProjectAssignmentSelectAll] TO [Public]
GO

 
