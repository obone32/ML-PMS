--***********************************************************
--INSERT Stored Procedure for Project table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectInsert] 
GO

CREATE PROCEDURE [ProjectInsert] 
      @ProjectName varchar(8000)
     ,@BillingName varchar(8000)
     ,@Description varchar(8000)
     ,@Location varchar(8000)
     ,@StartDate datetime
     ,@EndDate datetime
     ,@ProjectStatusID int
     ,@ClientID int
     ,@ArchitectID int
     ,@CompanyID int
     ,@AddUserID int
     ,@AddDate datetime
     ,@ArchiveUserID int
     ,@ArchiveDate datetime
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [Project]
     (
      [ProjectName]
     ,[BillingName]
     ,[Description]
     ,[Location]
     ,[StartDate]
     ,[EndDate]
     ,[ProjectStatusID]
     ,[ClientID]
     ,[ArchitectID]
     ,[CompanyID]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
     )
VALUES
     (
      @ProjectName
     ,@BillingName
     ,@Description
     ,@Location
     ,@StartDate
     ,@EndDate
     ,@ProjectStatusID
     ,@ClientID
     ,@ArchitectID
     ,@CompanyID
     ,@AddUserID
     ,@AddDate
     ,@ArchiveUserID
     ,@ArchiveDate
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

GRANT EXECUTE ON [ProjectInsert] TO [Public]
GO
 
