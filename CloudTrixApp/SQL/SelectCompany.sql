--***********************************************************
--SELECT Stored Procedure for Company table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'CompanySelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [CompanySelect] 
GO

CREATE PROCEDURE [CompanySelect] 
      @CompanyID int
AS 
BEGIN 
SELECT 
      [CompanyID]
     ,[CompanyName]
     ,[Address1]
     ,[Address2]
     ,[City]
     ,[District]
     ,[State]
     ,[Country]
     ,[PinCode]
     ,[ContactNo]
     ,[EMail]
     ,[GSTIN]
     ,[InvoiceInitials]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
FROM
     [Company]
WHERE
    [CompanyID] = @CompanyID
END 
GO

GRANT EXECUTE ON [CompanySelect] TO [Public]
GO

 
