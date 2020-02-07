--***********************************************************
--SELECT Stored Procedure for InvoiceAdvance table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceAdvanceSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceAdvanceSelectAll] 
GO

CREATE PROCEDURE [InvoiceAdvanceSelectAll]
AS 
BEGIN 
SELECT 
      [InvoiceAdvance].[InvoiceID]
     ,[InvoiceAdvance].[AdvancePaymentID]
FROM
     [InvoiceAdvance]
END 
GO

GRANT EXECUTE ON [InvoiceAdvanceSelectAll] TO [Public]
GO

 
