--***********************************************************
--SELECT Stored Procedure for Timesheet table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TimesheetSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [TimesheetSelect] 
GO

CREATE PROCEDURE [TimesheetSelect] 
      @TimesheetID int
AS 
BEGIN 
SELECT 
      [TimesheetID]
     ,[EmployeeID]
     ,[ProjectID]
     ,[StartTime]
     ,[EndTime]
     ,[Remarks]
FROM
     [Timesheet]
WHERE
    [TimesheetID] = @TimesheetID
END 
GO

GRANT EXECUTE ON [TimesheetSelect] TO [Public]
GO

 
