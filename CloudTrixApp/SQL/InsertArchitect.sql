--***********************************************************
--INSERT Stored Procedure for Architect table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectInsert] 
GO

CREATE PROCEDURE [ArchitectInsert] 
      @ArchitectName varchar(8000)
     ,@Address1 varchar(8000)
     ,@Address2 varchar(8000)
     ,@City varchar(8000)
     ,@District varchar(8000)
     ,@State varchar(8000)
     ,@Country varchar(8000)
     ,@Pincode varchar(8000)
     ,@EMail varchar(8000)
     ,@ContactNo varchar(8000)
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

INSERT [Architect]
     (
      [ArchitectName]
     ,[Address1]
     ,[Address2]
     ,[City]
     ,[District]
     ,[State]
     ,[Country]
     ,[Pincode]
     ,[EMail]
     ,[ContactNo]
     ,[CompanyID]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
     )
VALUES
     (
      @ArchitectName
     ,@Address1
     ,@Address2
     ,@City
     ,@District
     ,@State
     ,@Country
     ,@Pincode
     ,@EMail
     ,@ContactNo
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

GRANT EXECUTE ON [ArchitectInsert] TO [Public]
GO
 
