--***********************************************************
--SELECT Stored Procedure for AdvancePayment table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'AdvancePaymentSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [AdvancePaymentSelectAll] 
GO

CREATE PROCEDURE [AdvancePaymentSelectAll]
AS 
BEGIN 
SELECT 
      [AdvancePayment].[AdvancePaymentID]
     ,[AdvancePayment].[PaymentDate]
     ,[AdvancePayment].[CompanyID]
     ,[AdvancePayment].[ClientID]
     ,[AdvancePayment].[ProjectID]
     ,[AdvancePayment].[GrossAmount]
     ,[AdvancePayment].[TDSRate]
     ,[AdvancePayment].[CGSTRate]
     ,[AdvancePayment].[SGSTRate]
     ,[AdvancePayment].[IGSTRate]
     ,[AdvancePayment].[Remarks]
FROM
     [AdvancePayment]
END 
GO

GRANT EXECUTE ON [AdvancePaymentSelectAll] TO [Public]
GO

 
