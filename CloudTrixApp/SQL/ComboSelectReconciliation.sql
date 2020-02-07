--***********************************************************
--SELECT Stored Procedure for Reconciliation Invoice table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Reconciliation_InvoiceSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Reconciliation_InvoiceSelect] 
GO

CREATE PROCEDURE [Reconciliation_InvoiceSelect]
AS 
BEGIN 
SELECT
     [InvoiceID]
    ,[InvoiceNo]
FROM 
     [Invoice]
END 
GO

GRANT EXECUTE ON [Reconciliation_InvoiceSelect] TO [Public]
GO


 
