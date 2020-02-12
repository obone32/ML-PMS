--***********************************************************
--SELECT Stored Procedure for InvoiceItem table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceItemSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceItemSelect] 
GO

CREATE PROCEDURE [InvoiceItemSelect] 
      @InvoiceID int
     ,@InvoiceItemID int
AS 
BEGIN 
SELECT 
      [InvoiceID]
     ,[InvoiceItemID]
     ,[Description]
     ,[Quantity]
     ,[Rate]
     ,[DiscountAmount]
     ,[CGSTRate]
     ,[SGSTRate]
     ,[IGSTRate]
FROM
     [InvoiceItem]
WHERE
    [InvoiceID] = @InvoiceID
AND [InvoiceItemID] = @InvoiceItemID
END 
GO

GRANT EXECUTE ON [InvoiceItemSelect] TO [Public]
GO

 
