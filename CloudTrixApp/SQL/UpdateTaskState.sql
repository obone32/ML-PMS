--***********************************************************
--UPDATE Stored Procedure for TaskState table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskStateUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskStateUpdate]
GO

CREATE PROCEDURE [TaskStateUpdate] 
      @NewTaskStateName varchar(8000)
     ,@OldTaskStateID int
     ,@OldTaskStateName varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [TaskState]
SET
      [TaskStateName] = @NewTaskStateName
WHERE
     [TaskStateID] = @OldTaskStateID
AND [TaskStateName] = @OldTaskStateName

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

GRANT EXECUTE ON [TaskStateUpdate] TO [Public]
GO
 
