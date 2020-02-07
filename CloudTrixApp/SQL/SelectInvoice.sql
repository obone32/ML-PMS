--***********************************************************
--SELECT Stored Procedure for Invoice table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceSelect] 
GO

CREATE PROCEDURE [InvoiceSelect] 
      @InvoiceID int
AS 
BEGIN 
SELECT 
      [InvoiceID]
     ,[InvoiceNo]
     ,[InvoiceDate]
     ,[ProjectID]
     ,[ClientID]
     ,[ClientName]
     ,[ClientAddress]
     ,[ClientGSTIN]
     ,[ClientContactNo]
     ,[ClientEMail]
     ,[AdditionalDiscount]
     ,[Remarks]
     ,[PDFUrl]
     ,[CompanyID]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
FROM
     [Invoice]
WHERE
    [InvoiceID] = @InvoiceID
END 
GO

GRANT EXECUTE ON [InvoiceSelect] TO [Public]
GO

 
