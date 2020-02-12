--***********************************************************
--UPDATE Stored Procedure for ProjectStatus table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectStatusUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectStatusUpdate]
GO

CREATE PROCEDURE [ProjectStatusUpdate] 
      @NewProjectStatusName varchar(8000)
     ,@OldProjectStatusID int
     ,@OldProjectStatusName varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [ProjectStatus]
SET
      [ProjectStatusName] = @NewProjectStatusName
WHERE
     [ProjectStatusID] = @OldProjectStatusID
AND [ProjectStatusName] = @OldProjectStatusName

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

GRANT EXECUTE ON [ProjectStatusUpdate] TO [Public]
GO
 
