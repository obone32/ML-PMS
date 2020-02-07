--***********************************************************
--UPDATE Stored Procedure for Reconciliation table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ReconciliationUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [ReconciliationUpdate]
GO

CREATE PROCEDURE [ReconciliationUpdate] 
      @NewInvoiceID int
     ,@NewPaymentDate datetime
     ,@NewPaymentAmount decimal(18,2)
     ,@NewTDSAmount decimal(18,2)
     ,@NewRemarks varchar(8000)
     ,@OldReconciliationID int
     ,@OldInvoiceID int
     ,@OldPaymentDate datetime
     ,@OldPaymentAmount decimal(18,2)
     ,@OldTDSAmount decimal(18,2)
     ,@OldRemarks varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [Reconciliation]
SET
      [InvoiceID] = @NewInvoiceID
     ,[PaymentDate] = @NewPaymentDate
     ,[PaymentAmount] = @NewPaymentAmount
     ,[TDSAmount] = @NewTDSAmount
     ,[Remarks] = @NewRemarks
WHERE
     [ReconciliationID] = @OldReconciliationID
AND [InvoiceID] = @OldInvoiceID
AND [PaymentDate] = @OldPaymentDate
AND [PaymentAmount] = @OldPaymentAmount
AND [TDSAmount] = @OldTDSAmount
AND ((@OldRemarks IS NULL AND [Remarks] IS NULL) OR [Remarks] = @OldRemarks)

IF @@ROWCOUNT = 0
BEGIN
     ROLLBACK TRANSACTION
     SET @ReturnValue = 0
     RETURN @ReturnValue
END
ELSE
BEGIN
     COMMIT TRANSACTION
     SET @ReturnValue = 1
     RETURN @ReturnValue
END

END TRY

BEGIN CATCH
     DECLARE @Error_Message varchar(150)
     SET @Error_Message = ERROR_NUMBER() + ' ' + ERROR_MESSAGE()
     ROLLBACK TRANSACTION
     RAISERROR(@Error_Message,16,1)
      SET @ReturnValue = -1
      RETURN @ReturnValue
END CATCH

END
GO

GRANT EXECUTE ON [ReconciliationUpdate] TO [Public]
GO
 
