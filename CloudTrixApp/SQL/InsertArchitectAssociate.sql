--***********************************************************
--INSERT Stored Procedure for ArchitectAssociate table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectAssociateInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectAssociateInsert] 
GO

CREATE PROCEDURE [ArchitectAssociateInsert] 
      @ArchitectID int
     ,@ArchitectAssociateID int
     ,@AssociateName varchar(8000)
     ,@ContactNo varchar(8000)
     ,@EMail varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [ArchitectAssociate]
     (
      [ArchitectID]
     ,[ArchitectAssociateID]
     ,[AssociateName]
     ,[ContactNo]
     ,[EMail]
     )
VALUES
     (
      @ArchitectID
     ,@ArchitectAssociateID
     ,@AssociateName
     ,@ContactNo
     ,@EMail
     )

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

GRANT EXECUTE ON [ArchitectAssociateInsert] TO [Public]
GO
 
