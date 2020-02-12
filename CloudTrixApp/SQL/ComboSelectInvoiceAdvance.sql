--***********************************************************
--SELECT Stored Procedure for InvoiceAdvance Invoice table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceAdvance_InvoiceSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceAdvance_InvoiceSelect] 
GO

CREATE PROCEDURE [InvoiceAdvance_InvoiceSelect]
AS 
BEGIN 
SELECT
     [InvoiceID]
FROM 
     [Invoice]
END 
GO

GRANT EXECUTE ON [InvoiceAdvance_InvoiceSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for InvoiceAdvance AdvancePayment table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceAdvance_AdvancePaymentSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceAdvance_AdvancePaymentSelect] 
GO

CREATE PROCEDURE [InvoiceAdvance_AdvancePaymentSelect]
AS 
BEGIN 
SELECT
     [AdvancePaymentID]
FROM 
     [AdvancePayment]
END 
GO

GRANT EXECUTE ON [InvoiceAdvance_AdvancePaymentSelect] TO [Public]
GO


 
