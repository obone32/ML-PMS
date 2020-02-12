--***********************************************************
--SELECT Stored Procedure for Client Company table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Client_CompanySelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Client_CompanySelect] 
GO

CREATE PROCEDURE [Client_CompanySelect]
AS 
BEGIN 
SELECT
     [CompanyID]
    ,[CompanyName]
FROM 
     [Company]
END 
GO

GRANT EXECUTE ON [Client_CompanySelect] TO [Public]
GO


 
