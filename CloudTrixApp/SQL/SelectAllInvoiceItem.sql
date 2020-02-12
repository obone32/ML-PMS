--***********************************************************
--SELECT Stored Procedure for InvoiceItem table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceItemSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceItemSelectAll] 
GO

CREATE PROCEDURE [InvoiceItemSelectAll]
AS 
BEGIN 
SELECT 
      [InvoiceItem].[InvoiceID]
     ,[InvoiceItem].[InvoiceItemID]
     ,[InvoiceItem].[Description]
     ,[InvoiceItem].[Quantity]
     ,[InvoiceItem].[Rate]
     ,[InvoiceItem].[DiscountAmount]
     ,[InvoiceItem].[CGSTRate]
     ,[InvoiceItem].[SGSTRate]
     ,[InvoiceItem].[IGSTRate]
FROM
     [InvoiceItem]
END 
GO

GRANT EXECUTE ON [InvoiceItemSelectAll] TO [Public]
GO

 
