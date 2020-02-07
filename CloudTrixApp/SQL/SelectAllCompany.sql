--***********************************************************
--SELECT Stored Procedure for Company table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'CompanySelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [CompanySelectAll] 
GO

CREATE PROCEDURE [CompanySelectAll]
AS 
BEGIN 
SELECT 
      [Company].[CompanyID]
     ,[Company].[CompanyName]
     ,[Company].[Address1]
     ,[Company].[Address2]
     ,[Company].[City]
     ,[Company].[District]
     ,[Company].[State]
     ,[Company].[Country]
     ,[Company].[PinCode]
     ,[Company].[ContactNo]
     ,[Company].[EMail]
     ,[Company].[GSTIN]
     ,[Company].[InvoiceInitials]
     ,[Company].[AddUserID]
     ,[Company].[AddDate]
     ,[Company].[ArchiveUserID]
     ,[Company].[ArchiveDate]
FROM
     [Company]
END 
GO

GRANT EXECUTE ON [CompanySelectAll] TO [Public]
GO

 
