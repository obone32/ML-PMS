--***********************************************************
--SELECT Stored Procedure for Invoice table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceSelectAll] 
GO

CREATE PROCEDURE [InvoiceSelectAll]
AS 
BEGIN 
SELECT 
      [Invoice].[InvoiceID]
     ,[Invoice].[InvoiceNo]
     ,[Invoice].[InvoiceDate]
     ,[Invoice].[ProjectID]
     ,[Invoice].[ClientID]
     ,[Invoice].[ClientName]
     ,[Invoice].[ClientAddress]
     ,[Invoice].[ClientGSTIN]
     ,[Invoice].[ClientContactNo]
     ,[Invoice].[ClientEMail]
     ,[Invoice].[AdditionalDiscount]
     ,[Invoice].[Remarks]
     ,[Invoice].[PDFUrl]
     ,[Invoice].[CompanyID]
     ,[Invoice].[AddUserID]
     ,[Invoice].[AddDate]
     ,[Invoice].[ArchiveUserID]
     ,[Invoice].[ArchiveDate]
FROM
     [Invoice]
END 
GO

GRANT EXECUTE ON [InvoiceSelectAll] TO [Public]
GO

 
