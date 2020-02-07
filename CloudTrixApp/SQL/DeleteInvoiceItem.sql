--***********************************************************
--DELETE Stored Procedure for InvoiceItem table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceItemDelete'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceItemDelete] 
GO

CREATE PROCEDURE [InvoiceItemDelete]
      @OldInvoiceID int
     ,@OldInvoiceItemID int
     ,@OldDescription varchar(8000)
     ,@OldQuantity decimal(18,2)
     ,@OldRate decimal(18,2)
     ,@OldDiscountAmount decimal(18,2)
     ,@OldCGSTRate decimal(18,2)
     ,@OldSGSTRate decimal(18,2)
     ,@OldIGSTRate decimal(18,2)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

DELETE FROM [InvoiceItem]
WHERE
     [InvoiceID] = @OldInvoiceID
AND [InvoiceItemID] = @OldInvoiceItemID
AND ((@OldDescription IS NULL AND [Description] IS NULL) OR [Description] = @OldDescription)
AND [Quantity] = @OldQuantity
AND [Rate] = @OldRate
AND [DiscountAmount] = @OldDiscountAmount
AND [CGSTRate] = @OldCGSTRate
AND [SGSTRate] = @OldSGSTRate
AND [IGSTRate] = @OldIGSTRate

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

GRANT EXECUTE ON [InvoiceItemDelete] TO [Public]
GO
 
