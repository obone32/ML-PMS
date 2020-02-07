--***********************************************************
--UPDATE Stored Procedure for Task table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskUpdate]
GO

CREATE PROCEDURE [TaskUpdate] 
      @NewTaskName varchar(8000)
     ,@NewDescription varchar(8000)
     ,@NewCreationDate datetime
     ,@OldTaskID int
     ,@OldTaskName varchar(8000)
     ,@OldDescription varchar(8000)
     ,@OldCreationDate datetime
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [Task]
SET
      [TaskName] = @NewTaskName
     ,[Description] = @NewDescription
     ,[CreationDate] = @NewCreationDate
WHERE
     [TaskID] = @OldTaskID
AND [TaskName] = @OldTaskName
AND ((@OldDescription IS NULL AND [Description] IS NULL) OR [Description] = @OldDescription)
AND [CreationDate] = @OldCreationDate

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

GRANT EXECUTE ON [TaskUpdate] TO [Public]
GO
 
