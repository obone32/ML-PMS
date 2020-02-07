--***********************************************************
--UPDATE Stored Procedure for ArchitectAssociate table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectAssociateUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectAssociateUpdate]
GO

CREATE PROCEDURE [ArchitectAssociateUpdate] 
      @NewArchitectID int
     ,@NewArchitectAssociateID int
     ,@NewAssociateName varchar(8000)
     ,@NewContactNo varchar(8000)
     ,@NewEMail varchar(8000)
     ,@OldArchitectID int
     ,@OldArchitectAssociateID int
     ,@OldAssociateName varchar(8000)
     ,@OldContactNo varchar(8000)
     ,@OldEMail varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [ArchitectAssociate]
SET
      [ArchitectID] = @NewArchitectID
     ,[ArchitectAssociateID] = @NewArchitectAssociateID
     ,[AssociateName] = @NewAssociateName
     ,[ContactNo] = @NewContactNo
     ,[EMail] = @NewEMail
WHERE
     [ArchitectID] = @OldArchitectID
AND [ArchitectAssociateID] = @OldArchitectAssociateID
AND [AssociateName] = @OldAssociateName
AND ((@OldContactNo IS NULL AND [ContactNo] IS NULL) OR [ContactNo] = @OldContactNo)
AND ((@OldEMail IS NULL AND [EMail] IS NULL) OR [EMail] = @OldEMail)

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

GRANT EXECUTE ON [ArchitectAssociateUpdate] TO [Public]
GO
 
