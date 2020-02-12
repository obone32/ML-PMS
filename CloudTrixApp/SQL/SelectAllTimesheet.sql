--***********************************************************
--SELECT Stored Procedure for Timesheet table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TimesheetSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [TimesheetSelectAll] 
GO

CREATE PROCEDURE [TimesheetSelectAll]
AS 
BEGIN 
SELECT 
      [Timesheet].[TimesheetID]
     ,[Timesheet].[EmployeeID]
     ,[Timesheet].[ProjectID]
     ,[Timesheet].[StartTime]
     ,[Timesheet].[EndTime]
     ,[Timesheet].[Remarks]
FROM
     [Timesheet]
END 
GO

GRANT EXECUTE ON [TimesheetSelectAll] TO [Public]
GO

 
