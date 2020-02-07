--***********************************************************
--SELECT Stored Procedure for ProjectAssignment Project table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectAssignment_ProjectSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectAssignment_ProjectSelect] 
GO

CREATE PROCEDURE [ProjectAssignment_ProjectSelect]
AS 
BEGIN 
SELECT
     [ProjectID]
    ,[ProjectName]
FROM 
     [Project]
END 
GO

GRANT EXECUTE ON [ProjectAssignment_ProjectSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for ProjectAssignment Employee table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectAssignment_EmployeeSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectAssignment_EmployeeSelect] 
GO

CREATE PROCEDURE [ProjectAssignment_EmployeeSelect]
AS 
BEGIN 
SELECT
     [EmployeeID]
    ,[FirstName]
FROM 
     [Employee]
END 
GO

GRANT EXECUTE ON [ProjectAssignment_EmployeeSelect] TO [Public]
GO


 
