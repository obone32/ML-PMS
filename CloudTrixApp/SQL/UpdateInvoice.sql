--***********************************************************
--UPDATE Stored Procedure for Invoice table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceUpdate]
GO

CREATE PROCEDURE [InvoiceUpdate] 
      @NewInvoiceNo varchar(8000)
     ,@NewInvoiceDate datetime
     ,@NewProjectID int
     ,@NewClientID int
     ,@NewClientName varchar(8000)
     ,@NewClientAddress varchar(8000)
     ,@NewClientGSTIN varchar(8000)
     ,@NewClientContactNo varchar(8000)
     ,@NewClientEMail varchar(8000)
     ,@NewAdditionalDiscount decimal(18,2)
     ,@NewRemarks varchar(8000)
     ,@NewPDFUrl varchar(8000)
     ,@NewCompanyID int
     ,@NewAddUserID int
     ,@NewAddDate datetime
     ,@NewArchiveUserID int
     ,@NewArchiveDate datetime
     ,@OldInvoiceID int
     ,@OldInvoiceNo varchar(8000)
     ,@OldInvoiceDate datetime
     ,@OldProjectID int
     ,@OldClientID int
     ,@OldClientName varchar(8000)
     ,@OldClientAddress varchar(8000)
     ,@OldClientGSTIN varchar(8000)
     ,@OldClientContactNo varchar(8000)
     ,@OldClientEMail varchar(8000)
     ,@OldAdditionalDiscount decimal(18,2)
     ,@OldRemarks varchar(8000)
     ,@OldPDFUrl varchar(8000)
     ,@OldCompanyID int
     ,@OldAddUserID int
     ,@OldAddDate datetime
     ,@OldArchiveUserID int
     ,@OldArchiveDate datetime
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [Invoice]
SET
      [InvoiceNo] = @NewInvoiceNo
     ,[InvoiceDate] = @NewInvoiceDate
     ,[ProjectID] = @NewProjectID
     ,[ClientID] = @NewClientID
     ,[ClientName] = @NewClientName
     ,[ClientAddress] = @NewClientAddress
     ,[ClientGSTIN] = @NewClientGSTIN
     ,[ClientContactNo] = @NewClientContactNo
     ,[ClientEMail] = @NewClientEMail
     ,[AdditionalDiscount] = @NewAdditionalDiscount
     ,[Remarks] = @NewRemarks
     ,[PDFUrl] = @NewPDFUrl
     ,[CompanyID] = @NewCompanyID
     ,[AddUserID] = @NewAddUserID
     ,[AddDate] = @NewAddDate
     ,[ArchiveUserID] = @NewArchiveUserID
     ,[ArchiveDate] = @NewArchiveDate
WHERE
     [InvoiceID] = @OldInvoiceID
AND [InvoiceNo] = @OldInvoiceNo
AND [InvoiceDate] = @OldInvoiceDate
AND ((@OldProjectID IS NULL AND [ProjectID] IS NULL) OR [ProjectID] = @OldProjectID)
AND [ClientID] = @OldClientID
AND [ClientName] = @OldClientName
AND ((@OldClientAddress IS NULL AND [ClientAddress] IS NULL) OR [ClientAddress] = @OldClientAddress)
AND ((@OldClientGSTIN IS NULL AND [ClientGSTIN] IS NULL) OR [ClientGSTIN] = @OldClientGSTIN)
AND ((@OldClientContactNo IS NULL AND [ClientContactNo] IS NULL) OR [ClientContactNo] = @OldClientContactNo)
AND ((@OldClientEMail IS NULL AND [ClientEMail] IS NULL) OR [ClientEMail] = @OldClientEMail)
AND [AdditionalDiscount] = @OldAdditionalDiscount
AND ((@OldRemarks IS NULL AND [Remarks] IS NULL) OR [Remarks] = @OldRemarks)
AND ((@OldPDFUrl IS NULL AND [PDFUrl] IS NULL) OR [PDFUrl] = @OldPDFUrl)
AND [CompanyID] = @OldCompanyID
AND [AddUserID] = @OldAddUserID
AND [AddDate] = @OldAddDate
AND ((@OldArchiveUserID IS NULL AND [ArchiveUserID] IS NULL) OR [ArchiveUserID] = @OldArchiveUserID)
AND ((@OldArchiveDate IS NULL AND [ArchiveDate] IS NULL) OR [ArchiveDate] = @OldArchiveDate)

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

GRANT EXECUTE ON [InvoiceUpdate] TO [Public]
GO
 
