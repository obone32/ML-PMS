--***********************************************************
--SELECT Stored Procedure for Employee table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'EmployeeSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [EmployeeSelect] 
GO

CREATE PROCEDURE [EmployeeSelect] 
      @EmployeeID int
AS 
BEGIN 
SELECT 
      [EmployeeID]
     ,[FirstName]
     ,[LastName]
     ,[DOB]
     ,[DOJ]
     ,[Gender]
     ,[EMail]
     ,[Mobile]
     ,[Address1]
     ,[Address2]
     ,[Salary]
     ,[SignatureURL]
     ,[CompanyID]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
FROM
     [Employee]
WHERE
    [EmployeeID] = @EmployeeID
END 
GO

GRANT EXECUTE ON [EmployeeSelect] TO [Public]
GO

 
