--***********************************************************
--UPDATE Stored Procedure for AdvancePayment table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'AdvancePaymentUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [AdvancePaymentUpdate]
GO

CREATE PROCEDURE [AdvancePaymentUpdate] 
      @NewPaymentDate datetime
     ,@NewCompanyID int
     ,@NewClientID int
     ,@NewProjectID int
     ,@NewGrossAmount decimal(18,2)
     ,@NewTDSRate decimal(18,2)
     ,@NewCGSTRate decimal(18,2)
     ,@NewSGSTRate decimal(18,2)
     ,@NewIGSTRate decimal(18,2)
     ,@NewRemarks varchar(8000)
     ,@OldAdvancePaymentID int
     ,@OldPaymentDate datetime
     ,@OldCompanyID int
     ,@OldClientID int
     ,@OldProjectID int
     ,@OldGrossAmount decimal(18,2)
     ,@OldTDSRate decimal(18,2)
     ,@OldCGSTRate decimal(18,2)
     ,@OldSGSTRate decimal(18,2)
     ,@OldIGSTRate decimal(18,2)
     ,@OldRemarks varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [AdvancePayment]
SET
      [PaymentDate] = @NewPaymentDate
     ,[CompanyID] = @NewCompanyID
     ,[ClientID] = @NewClientID
     ,[ProjectID] = @NewProjectID
     ,[GrossAmount] = @NewGrossAmount
     ,[TDSRate] = @NewTDSRate
     ,[CGSTRate] = @NewCGSTRate
     ,[SGSTRate] = @NewSGSTRate
     ,[IGSTRate] = @NewIGSTRate
     ,[Remarks] = @NewRemarks
WHERE
     [AdvancePaymentID] = @OldAdvancePaymentID
AND [PaymentDate] = @OldPaymentDate
AND [CompanyID] = @OldCompanyID
AND [ClientID] = @OldClientID
AND [ProjectID] = @OldProjectID
AND [GrossAmount] = @OldGrossAmount
AND [TDSRate] = @OldTDSRate
AND [CGSTRate] = @OldCGSTRate
AND [SGSTRate] = @OldSGSTRate
AND [IGSTRate] = @OldIGSTRate
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

GRANT EXECUTE ON [AdvancePaymentUpdate] TO [Public]
GO
 
