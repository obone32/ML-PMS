--***********************************************************
--SELECT Stored Procedure for TaskAssignment Task table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAssignment_TaskSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAssignment_TaskSelect] 
GO

CREATE PROCEDURE [TaskAssignment_TaskSelect]
AS 
BEGIN 
SELECT
     [TaskID]
    ,[TaskName]
FROM 
     [Task]
END 
GO

GRANT EXECUTE ON [TaskAssignment_TaskSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for TaskAssignment Employee table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAssignment_EmployeeSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAssignment_EmployeeSelect] 
GO

CREATE PROCEDURE [TaskAssignment_EmployeeSelect]
AS 
BEGIN 
SELECT
     [EmployeeID]
    ,[FirstName]
FROM 
     [Employee]
END 
GO

GRANT EXECUTE ON [TaskAssignment_EmployeeSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for TaskAssignment TaskState table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAssignment_TaskStateSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAssignment_TaskStateSelect] 
GO

CREATE PROCEDURE [TaskAssignment_TaskStateSelect]
AS 
BEGIN 
SELECT
     [TaskStateID]
    ,[TaskStateName]
FROM 
     [TaskState]
END 
GO

GRANT EXECUTE ON [TaskAssignment_TaskStateSelect] TO [Public]
GO


 
