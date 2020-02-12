--***********************************************************
--DELETE Stored Procedure for ArchitectAssociate table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectAssociateDelete'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectAssociateDelete] 
GO

CREATE PROCEDURE [ArchitectAssociateDelete]
      @OldArchitectID int
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

DELETE FROM [ArchitectAssociate]
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

GRANT EXECUTE ON [ArchitectAssociateDelete] TO [Public]
GO
 
