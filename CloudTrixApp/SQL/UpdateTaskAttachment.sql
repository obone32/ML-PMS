--***********************************************************
--UPDATE Stored Procedure for TaskAttachment table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAttachmentUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAttachmentUpdate]
GO

CREATE PROCEDURE [TaskAttachmentUpdate] 
      @NewTaskID int
     ,@NewTaskAttachmentID int
     ,@NewAttachmentName varchar(8000)
     ,@NewDecription varchar(8000)
     ,@NewFilePath varchar(8000)
     ,@OldTaskID int
     ,@OldTaskAttachmentID int
     ,@OldAttachmentName varchar(8000)
     ,@OldDecription varchar(8000)
     ,@OldFilePath varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [TaskAttachment]
SET
      [TaskID] = @NewTaskID
     ,[TaskAttachmentID] = @NewTaskAttachmentID
     ,[AttachmentName] = @NewAttachmentName
     ,[Decription] = @NewDecription
     ,[FilePath] = @NewFilePath
WHERE
     [TaskID] = @OldTaskID
AND [TaskAttachmentID] = @OldTaskAttachmentID
AND [AttachmentName] = @OldAttachmentName
AND ((@OldDecription IS NULL AND [Decription] IS NULL) OR [Decription] = @OldDecription)
AND [FilePath] = @OldFilePath

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

GRANT EXECUTE ON [TaskAttachmentUpdate] TO [Public]
GO
 
