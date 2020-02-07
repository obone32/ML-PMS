--***********************************************************
--SELECT Stored Procedure for InvoiceItem Invoice table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceItem_InvoiceSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceItem_InvoiceSelect] 
GO

CREATE PROCEDURE [InvoiceItem_InvoiceSelect]
AS 
BEGIN 
SELECT
     [InvoiceID]
FROM 
     [Invoice]
END 
GO

GRANT EXECUTE ON [InvoiceItem_InvoiceSelect] TO [Public]
GO


 
