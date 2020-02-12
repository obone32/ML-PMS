--***********************************************************
--UPDATE Stored Procedure for Architect table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectUpdate]
GO

CREATE PROCEDURE [ArchitectUpdate] 
      @NewArchitectName varchar(8000)
     ,@NewAddress1 varchar(8000)
     ,@NewAddress2 varchar(8000)
     ,@NewCity varchar(8000)
     ,@NewDistrict varchar(8000)
     ,@NewState varchar(8000)
     ,@NewCountry varchar(8000)
     ,@NewPincode varchar(8000)
     ,@NewEMail varchar(8000)
     ,@NewContactNo varchar(8000)
     ,@NewCompanyID int
     ,@NewAddUserID int
     ,@NewAddDate datetime
     ,@NewArchiveUserID int
     ,@NewArchiveDate datetime
     ,@OldArchitectID int
     ,@OldArchitectName varchar(8000)
     ,@OldAddress1 varchar(8000)
     ,@OldAddress2 varchar(8000)
     ,@OldCity varchar(8000)
     ,@OldDistrict varchar(8000)
     ,@OldState varchar(8000)
     ,@OldCountry varchar(8000)
     ,@OldPincode varchar(8000)
     ,@OldEMail varchar(8000)
     ,@OldContactNo varchar(8000)
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

UPDATE [Architect]
SET
      [ArchitectName] = @NewArchitectName
     ,[Address1] = @NewAddress1
     ,[Address2] = @NewAddress2
     ,[City] = @NewCity
     ,[District] = @NewDistrict
     ,[State] = @NewState
     ,[Country] = @NewCountry
     ,[Pincode] = @NewPincode
     ,[EMail] = @NewEMail
     ,[ContactNo] = @NewContactNo
     ,[CompanyID] = @NewCompanyID
     ,[AddUserID] = @NewAddUserID
     ,[AddDate] = @NewAddDate
     ,[ArchiveUserID] = @NewArchiveUserID
     ,[ArchiveDate] = @NewArchiveDate
WHERE
     [ArchitectID] = @OldArchitectID
AND [ArchitectName] = @OldArchitectName
AND ((@OldAddress1 IS NULL AND [Address1] IS NULL) OR [Address1] = @OldAddress1)
AND ((@OldAddress2 IS NULL AND [Address2] IS NULL) OR [Address2] = @OldAddress2)
AND ((@OldCity IS NULL AND [City] IS NULL) OR [City] = @OldCity)
AND ((@OldDistrict IS NULL AND [District] IS NULL) OR [District] = @OldDistrict)
AND ((@OldState IS NULL AND [State] IS NULL) OR [State] = @OldState)
AND ((@OldCountry IS NULL AND [Country] IS NULL) OR [Country] = @OldCountry)
AND ((@OldPincode IS NULL AND [Pincode] IS NULL) OR [Pincode] = @OldPincode)
AND ((@OldEMail IS NULL AND [EMail] IS NULL) OR [EMail] = @OldEMail)
AND ((@OldContactNo IS NULL AND [ContactNo] IS NULL) OR [ContactNo] = @OldContactNo)
AND [CompanyID] = @OldCompanyID
AND ((@OldAddUserID IS NULL AND [AddUserID] IS NULL) OR [AddUserID] = @OldAddUserID)
AND ((@OldAddDate IS NULL AND [AddDate] IS NULL) OR [AddDate] = @OldAddDate)
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

GRANT EXECUTE ON [ArchitectUpdate] TO [Public]
GO
 
