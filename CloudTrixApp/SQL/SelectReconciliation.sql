--***********************************************************
--SELECT Stored Procedure for Reconciliation table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ReconciliationSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ReconciliationSelect] 
GO

CREATE PROCEDURE [ReconciliationSelect] 
      @ReconciliationID int
AS 
BEGIN 
SELECT 
      [ReconciliationID]
     ,[InvoiceID]
     ,[PaymentDate]
     ,[PaymentAmount]
     ,[TDSAmount]
     ,[Remarks]
FROM
     [Reconciliation]
WHERE
    [ReconciliationID] = @ReconciliationID
END 
GO

GRANT EXECUTE ON [ReconciliationSelect] TO [Public]
GO

 
