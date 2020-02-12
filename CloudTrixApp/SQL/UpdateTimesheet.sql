--***********************************************************
--UPDATE Stored Procedure for Timesheet table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TimesheetUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [TimesheetUpdate]
GO

CREATE PROCEDURE [TimesheetUpdate] 
      @NewEmployeeID int
     ,@NewProjectID int
     ,@NewStartTime datetime
     ,@NewEndTime datetime
     ,@NewRemarks varchar(8000)
     ,@OldTimesheetID int
     ,@OldEmployeeID int
     ,@OldProjectID int
     ,@OldStartTime datetime
     ,@OldEndTime datetime
     ,@OldRemarks varchar(8000)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [Timesheet]
SET
      [EmployeeID] = @NewEmployeeID
     ,[ProjectID] = @NewProjectID
     ,[StartTime] = @NewStartTime
     ,[EndTime] = @NewEndTime
     ,[Remarks] = @NewRemarks
WHERE
     [TimesheetID] = @OldTimesheetID
AND [EmployeeID] = @OldEmployeeID
AND [ProjectID] = @OldProjectID
AND [StartTime] = @OldStartTime
AND [EndTime] = @OldEndTime
AND [Remarks] = @OldRemarks

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

GRANT EXECUTE ON [TimesheetUpdate] TO [Public]
GO
 
