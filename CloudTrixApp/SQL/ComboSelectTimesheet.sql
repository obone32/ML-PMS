--***********************************************************
--SELECT Stored Procedure for Timesheet Employee table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Timesheet_EmployeeSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Timesheet_EmployeeSelect] 
GO

CREATE PROCEDURE [Timesheet_EmployeeSelect]
AS 
BEGIN 
SELECT
     [EmployeeID]
    ,[FirstName]
FROM 
     [Employee]
END 
GO

GRANT EXECUTE ON [Timesheet_EmployeeSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for Timesheet Project table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Timesheet_ProjectSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Timesheet_ProjectSelect] 
GO

CREATE PROCEDURE [Timesheet_ProjectSelect]
AS 
BEGIN 
SELECT
     [ProjectID]
    ,[ProjectName]
FROM 
     [Project]
END 
GO

GRANT EXECUTE ON [Timesheet_ProjectSelect] TO [Public]
GO


 
