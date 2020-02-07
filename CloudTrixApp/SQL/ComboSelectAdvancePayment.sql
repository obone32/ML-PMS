--***********************************************************
--SELECT Stored Procedure for AdvancePayment Company table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'AdvancePayment_CompanySelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [AdvancePayment_CompanySelect] 
GO

CREATE PROCEDURE [AdvancePayment_CompanySelect]
AS 
BEGIN 
SELECT
     [CompanyID]
    ,[CompanyName]
FROM 
     [Company]
END 
GO

GRANT EXECUTE ON [AdvancePayment_CompanySelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for AdvancePayment Client table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'AdvancePayment_ClientSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [AdvancePayment_ClientSelect] 
GO

CREATE PROCEDURE [AdvancePayment_ClientSelect]
AS 
BEGIN 
SELECT
     [ClientID]
    ,[ClientName]
FROM 
     [Client]
END 
GO

GRANT EXECUTE ON [AdvancePayment_ClientSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for AdvancePayment Project table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'AdvancePayment_ProjectSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [AdvancePayment_ProjectSelect] 
GO

CREATE PROCEDURE [AdvancePayment_ProjectSelect]
AS 
BEGIN 
SELECT
     [ProjectID]
    ,[ProjectName]
FROM 
     [Project]
END 
GO

GRANT EXECUTE ON [AdvancePayment_ProjectSelect] TO [Public]
GO


 
