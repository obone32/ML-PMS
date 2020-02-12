--***********************************************************
--UPDATE Stored Procedure for ProjectAssignment table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectAssignmentUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectAssignmentUpdate]
GO

CREATE PROCEDURE [ProjectAssignmentUpdate] 
      @NewProjectID int
     ,@NewEmployeeID int
     ,@NewAssignmentDate datetime
     ,@NewRemarks varchar(8000)
     ,@NewAddUserID int
     ,@NewAddDate datetime
     ,@NewArchiveUserID int
     ,@NewArchiveDate datetime
     ,@OldProjectAssignmentID int
     ,@OldProjectID int
     ,@OldEmployeeID int
     ,@OldAssignmentDate datetime
     ,@OldRemarks varchar(8000)
     ,@OldAddUserID int
     ,@OldAddDate datetime
     ,@OldArchiveUserID int
     ,@OldArchiveDate datetime
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [ProjectAssignment]
SET
      [ProjectID] = @NewProjectID
     ,[EmployeeID] = @NewEmployeeID
     ,[AssignmentDate] = @NewAssignmentDate
     ,[Remarks] = @NewRemarks
     ,[AddUserID] = @NewAddUserID
     ,[AddDate] = @NewAddDate
     ,[ArchiveUserID] = @NewArchiveUserID
     ,[ArchiveDate] = @NewArchiveDate
WHERE
     [ProjectAssignmentID] = @OldProjectAssignmentID
AND [ProjectID] = @OldProjectID
AND [EmployeeID] = @OldEmployeeID
AND [AssignmentDate] = @OldAssignmentDate
AND ((@OldRemarks IS NULL AND [Remarks] IS NULL) OR [Remarks] = @OldRemarks)
AND [AddUserID] = @OldAddUserID
AND [AddDate] = @OldAddDate
AND ((@OldArchiveUserID IS NULL AND [ArchiveUserID] IS NULL) OR [ArchiveUserID] = @OldArchiveUserID)
AND ((@OldArchiveDate IS NULL AND [ArchiveDate] IS NULL) OR [ArchiveDate] = @OldArchiveDate)

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

GRANT EXECUTE ON [ProjectAssignmentUpdate] TO [Public]
GO
 
