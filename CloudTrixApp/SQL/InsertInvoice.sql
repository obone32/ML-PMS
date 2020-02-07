--***********************************************************
--INSERT Stored Procedure for Invoice table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceInsert] 
GO

CREATE PROCEDURE [InvoiceInsert] 
      @InvoiceNo varchar(8000)
     ,@InvoiceDate datetime
     ,@ProjectID int
     ,@ClientID int
     ,@ClientName varchar(8000)
     ,@ClientAddress varchar(8000)
     ,@ClientGSTIN varchar(8000)
     ,@ClientContactNo varchar(8000)
     ,@ClientEMail varchar(8000)
     ,@AdditionalDiscount decimal(18,2)
     ,@Remarks varchar(8000)
     ,@PDFUrl varchar(8000)
     ,@CompanyID int
     ,@AddUserID int
     ,@AddDate datetime
     ,@ArchiveUserID int
     ,@ArchiveDate datetime
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [Invoice]
     (
      [InvoiceNo]
     ,[InvoiceDate]
     ,[ProjectID]
     ,[ClientID]
     ,[ClientName]
     ,[ClientAddress]
     ,[ClientGSTIN]
     ,[ClientContactNo]
     ,[ClientEMail]
     ,[AdditionalDiscount]
     ,[Remarks]
     ,[PDFUrl]
     ,[CompanyID]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
     )
VALUES
     (
      @InvoiceNo
     ,@InvoiceDate
     ,@ProjectID
     ,@ClientID
     ,@ClientName
     ,@ClientAddress
     ,@ClientGSTIN
     ,@ClientContactNo
     ,@ClientEMail
     ,@AdditionalDiscount
     ,@Remarks
     ,@PDFUrl
     ,@CompanyID
     ,@AddUserID
     ,@AddDate
     ,@ArchiveUserID
     ,@ArchiveDate
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

GRANT EXECUTE ON [InvoiceInsert] TO [Public]
GO
 
