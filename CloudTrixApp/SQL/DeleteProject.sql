--***********************************************************
--DELETE Stored Procedure for Project table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectDelete'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectDelete] 
GO

CREATE PROCEDURE [ProjectDelete]
      @OldProjectID int
     ,@OldProjectName varchar(8000)
     ,@OldBillingName varchar(8000)
     ,@OldDescription varchar(8000)
     ,@OldLocation varchar(8000)
     ,@OldStartDate datetime
     ,@OldEndDate datetime
     ,@OldProjectStatusID int
     ,@OldClientID int
     ,@OldArchitectID int
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

DELETE FROM [Project]
WHERE
     [ProjectID] = @OldProjectID
AND [ProjectName] = @OldProjectName
AND ((@OldBillingName IS NULL AND [BillingName] IS NULL) OR [BillingName] = @OldBillingName)
AND ((@OldDescription IS NULL AND [Description] IS NULL) OR [Description] = @OldDescription)
AND ((@OldLocation IS NULL AND [Location] IS NULL) OR [Location] = @OldLocation)
AND [StartDate] = @OldStartDate
AND ((@OldEndDate IS NULL AND [EndDate] IS NULL) OR [EndDate] = @OldEndDate)
AND [ProjectStatusID] = @OldProjectStatusID
AND [ClientID] = @OldClientID
AND [ArchitectID] = @OldArchitectID
AND ((@OldCompanyID IS NULL AND [CompanyID] IS NULL) OR [CompanyID] = @OldCompanyID)
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

GRANT EXECUTE ON [ProjectDelete] TO [Public]
GO
 
