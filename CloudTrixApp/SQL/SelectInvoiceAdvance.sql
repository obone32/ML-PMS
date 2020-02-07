--***********************************************************
--SELECT Stored Procedure for InvoiceAdvance table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceAdvanceSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceAdvanceSelect] 
GO

CREATE PROCEDURE [InvoiceAdvanceSelect] 
      @InvoiceID int
     ,@AdvancePaymentID int
AS 
BEGIN 
SELECT 
      [InvoiceID]
     ,[AdvancePaymentID]
FROM
     [InvoiceAdvance]
WHERE
    [InvoiceID] = @InvoiceID
AND [AdvancePaymentID] = @AdvancePaymentID
END 
GO

GRANT EXECUTE ON [InvoiceAdvanceSelect] TO [Public]
GO

 
