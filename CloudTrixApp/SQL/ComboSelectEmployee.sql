--***********************************************************
--SELECT Stored Procedure for Employee Company table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Employee_CompanySelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Employee_CompanySelect] 
GO

CREATE PROCEDURE [Employee_CompanySelect]
AS 
BEGIN 
SELECT
     [CompanyID]
    ,[CompanyName]
FROM 
     [Company]
END 
GO

GRANT EXECUTE ON [Employee_CompanySelect] TO [Public]
GO


 
