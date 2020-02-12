--***********************************************************
--DELETE Stored Procedure for UserTypePermission table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypePermissionDelete'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypePermissionDelete] 
GO

CREATE PROCEDURE [UserTypePermissionDelete]
      @OldUserTypeID int
     ,@OldUserTypePermissionID int
     ,@OldPermissionID int
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

DELETE FROM [UserTypePermission]
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

GRANT EXECUTE ON [UserTypePermissionDelete] TO [Public]
GO
 
