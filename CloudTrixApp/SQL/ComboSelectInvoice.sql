--***********************************************************
--SELECT Stored Procedure for Invoice Project table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Invoice_ProjectSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Invoice_ProjectSelect] 
GO

CREATE PROCEDURE [Invoice_ProjectSelect]
AS 
BEGIN 
SELECT
     [ProjectID]
    ,[ProjectName]
FROM 
     [Project]
END 
GO

GRANT EXECUTE ON [Invoice_ProjectSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for Invoice Client table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Invoice_ClientSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Invoice_ClientSelect] 
GO

CREATE PROCEDURE [Invoice_ClientSelect]
AS 
BEGIN 
SELECT
     [ClientID]
    ,[ClientName]
FROM 
     [Client]
END 
GO

GRANT EXECUTE ON [Invoice_ClientSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for Invoice Company table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Invoice_CompanySelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Invoice_CompanySelect] 
GO

CREATE PROCEDURE [Invoice_CompanySelect]
AS 
BEGIN 
SELECT
     [CompanyID]
    ,[CompanyName]
FROM 
     [Company]
END 
GO

GRANT EXECUTE ON [Invoice_CompanySelect] TO [Public]
GO


 
