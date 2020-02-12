--***********************************************************
--SELECT Stored Procedure for Employee table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'EmployeeSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [EmployeeSelectAll] 
GO

CREATE PROCEDURE [EmployeeSelectAll]
AS 
BEGIN 
SELECT 
      [Employee].[EmployeeID]
     ,[Employee].[FirstName]
     ,[Employee].[LastName]
     ,[Employee].[DOB]
     ,[Employee].[DOJ]
     ,[Employee].[Gender]
     ,[Employee].[EMail]
     ,[Employee].[Mobile]
     ,[Employee].[Address1]
     ,[Employee].[Address2]
     ,[Employee].[Salary]
     ,[Employee].[SignatureURL]
     ,[Employee].[CompanyID]
     ,[Employee].[AddUserID]
     ,[Employee].[AddDate]
     ,[Employee].[ArchiveUserID]
     ,[Employee].[ArchiveDate]
FROM
     [Employee]
END 
GO

GRANT EXECUTE ON [EmployeeSelectAll] TO [Public]
GO

 
