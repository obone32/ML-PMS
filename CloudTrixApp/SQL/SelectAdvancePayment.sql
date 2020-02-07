--***********************************************************
--SELECT Stored Procedure for AdvancePayment table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'AdvancePaymentSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [AdvancePaymentSelect] 
GO

CREATE PROCEDURE [AdvancePaymentSelect] 
      @AdvancePaymentID int
AS 
BEGIN 
SELECT 
      [AdvancePaymentID]
     ,[PaymentDate]
     ,[CompanyID]
     ,[ClientID]
     ,[ProjectID]
     ,[GrossAmount]
     ,[TDSRate]
     ,[CGSTRate]
     ,[SGSTRate]
     ,[IGSTRate]
     ,[Remarks]
FROM
     [AdvancePayment]
WHERE
    [AdvancePaymentID] = @AdvancePaymentID
END 
GO

GRANT EXECUTE ON [AdvancePaymentSelect] TO [Public]
GO

 
