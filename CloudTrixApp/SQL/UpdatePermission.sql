--***********************************************************
--UPDATE Stored Procedure for Permission table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'PermissionUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [PermissionUpdate]
GO

CREATE PROCEDURE [PermissionUpdate] 
      @NewPermissionName varchar(8000)
     ,@OldPermissionID int
     ,@OldPermissionName varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [Permission]
SET
      [PermissionName] = @NewPermissionName
WHERE
     [PermissionID] = @OldPermissionID
AND [PermissionName] = @OldPermissionName

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

GRANT EXECUTE ON [PermissionUpdate] TO [Public]
GO
 
