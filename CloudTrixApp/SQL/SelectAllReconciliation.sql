--***********************************************************
--SELECT Stored Procedure for Reconciliation table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ReconciliationSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [ReconciliationSelectAll] 
GO

CREATE PROCEDURE [ReconciliationSelectAll]
AS 
BEGIN 
SELECT 
      [Reconciliation].[ReconciliationID]
     ,[Reconciliation].[InvoiceID]
     ,[Reconciliation].[PaymentDate]
     ,[Reconciliation].[PaymentAmount]
     ,[Reconciliation].[TDSAmount]
     ,[Reconciliation].[Remarks]
FROM
     [Reconciliation]
END 
GO

GRANT EXECUTE ON [ReconciliationSelectAll] TO [Public]
GO

 
