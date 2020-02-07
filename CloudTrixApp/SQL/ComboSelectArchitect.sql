--***********************************************************
--SELECT Stored Procedure for Architect Company table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Architect_CompanySelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Architect_CompanySelect] 
GO

CREATE PROCEDURE [Architect_CompanySelect]
AS 
BEGIN 
SELECT
     [CompanyID]
    ,[CompanyName]
FROM 
     [Company]
END 
GO

GRANT EXECUTE ON [Architect_CompanySelect] TO [Public]
GO


 
