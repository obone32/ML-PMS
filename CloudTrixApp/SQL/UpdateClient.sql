--***********************************************************
--UPDATE Stored Procedure for Client table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ClientUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [ClientUpdate]
GO

CREATE PROCEDURE [ClientUpdate] 
      @NewClientName varchar(8000)
     ,@NewAddress1 varchar(8000)
     ,@NewAddress2 varchar(8000)
     ,@NewCity varchar(8000)
     ,@NewDistrict varchar(8000)
     ,@NewState varchar(8000)
     ,@NewPinCode varchar(8000)
     ,@NewContactNo varchar(8000)
     ,@NewEMail varchar(8000)
     ,@NewGSTIN varchar(8000)
     ,@NewCompanyID int
     ,@NewAddUserID int
     ,@NewAddDate datetime
     ,@NewArchiveUserID int
     ,@NewArchiveDate datetime
     ,@OldClientID int
     ,@OldClientName varchar(8000)
     ,@OldAddress1 varchar(8000)
     ,@OldAddress2 varchar(8000)
     ,@OldCity varchar(8000)
     ,@OldDistrict varchar(8000)
     ,@OldState varchar(8000)
     ,@OldPinCode varchar(8000)
     ,@OldContactNo varchar(8000)
     ,@OldEMail varchar(8000)
     ,@OldGSTIN varchar(8000)
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

UPDATE [Client]
SET
      [ClientName] = @NewClientName
     ,[Address1] = @NewAddress1
     ,[Address2] = @NewAddress2
     ,[City] = @NewCity
     ,[District] = @NewDistrict
     ,[State] = @NewState
     ,[PinCode] = @NewPinCode
     ,[ContactNo] = @NewContactNo
     ,[EMail] = @NewEMail
     ,[GSTIN] = @NewGSTIN
     ,[CompanyID] = @NewCompanyID
     ,[AddUserID] = @NewAddUserID
     ,[AddDate] = @NewAddDate
     ,[ArchiveUserID] = @NewArchiveUserID
     ,[ArchiveDate] = @NewArchiveDate
WHERE
     [ClientID] = @OldClientID
AND [ClientName] = @OldClientName
AND ((@OldAddress1 IS NULL AND [Address1] IS NULL) OR [Address1] = @OldAddress1)
AND ((@OldAddress2 IS NULL AND [Address2] IS NULL) OR [Address2] = @OldAddress2)
AND ((@OldCity IS NULL AND [City] IS NULL) OR [City] = @OldCity)
AND ((@OldDistrict IS NULL AND [District] IS NULL) OR [District] = @OldDistrict)
AND ((@OldState IS NULL AND [State] IS NULL) OR [State] = @OldState)
AND ((@OldPinCode IS NULL AND [PinCode] IS NULL) OR [PinCode] = @OldPinCode)
AND ((@OldContactNo IS NULL AND [ContactNo] IS NULL) OR [ContactNo] = @OldContactNo)
AND ((@OldEMail IS NULL AND [EMail] IS NULL) OR [EMail] = @OldEMail)
AND ((@OldGSTIN IS NULL AND [GSTIN] IS NULL) OR [GSTIN] = @OldGSTIN)
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

GRANT EXECUTE ON [ClientUpdate] TO [Public]
GO
 
