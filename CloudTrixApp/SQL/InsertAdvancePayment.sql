--***********************************************************
--INSERT Stored Procedure for AdvancePayment table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'AdvancePaymentInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [AdvancePaymentInsert] 
GO

CREATE PROCEDURE [AdvancePaymentInsert] 
      @PaymentDate datetime
     ,@CompanyID int
     ,@ClientID int
     ,@ProjectID int
     ,@GrossAmount decimal(18,2)
     ,@TDSRate decimal(18,2)
     ,@CGSTRate decimal(18,2)
     ,@SGSTRate decimal(18,2)
     ,@IGSTRate decimal(18,2)
     ,@Remarks varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [AdvancePayment]
     (
      [PaymentDate]
     ,[CompanyID]
     ,[ClientID]
     ,[ProjectID]
     ,[GrossAmount]
     ,[TDSRate]
     ,[CGSTRate]
     ,[SGSTRate]
     ,[IGSTRate]
     ,[Remarks]
     )
VALUES
     (
      @PaymentDate
     ,@CompanyID
     ,@ClientID
     ,@ProjectID
     ,@GrossAmount
     ,@TDSRate
     ,@CGSTRate
     ,@SGSTRate
     ,@IGSTRate
     ,@Remarks
     )

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

GRANT EXECUTE ON [AdvancePaymentInsert] TO [Public]
GO
 
