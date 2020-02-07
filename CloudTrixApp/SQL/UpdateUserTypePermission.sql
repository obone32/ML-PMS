--***********************************************************
--UPDATE Stored Procedure for UserTypePermission table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypePermissionUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypePermissionUpdate]
GO

CREATE PROCEDURE [UserTypePermissionUpdate] 
      @NewUserTypeID int
     ,@NewUserTypePermissionID int
     ,@NewPermissionID int
     ,@OldUserTypeID int
     ,@OldUserTypePermissionID int
     ,@OldPermissionID int
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [UserTypePermission]
SET
      [UserTypeID] = @NewUserTypeID
     ,[UserTypePermissionID] = @NewUserTypePermissionID
     ,[PermissionID] = @NewPermissionID
WHERE
     [UserTypeID] = @OldUserTypeID
AND [UserTypePermissionID] = @OldUserTypePermissionID
AND [PermissionID] = @OldPermissionID

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

GRANT EXECUTE ON [UserTypePermissionUpdate] TO [Public]
GO
 
