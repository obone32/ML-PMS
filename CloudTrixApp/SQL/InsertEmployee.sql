--***********************************************************
--INSERT Stored Procedure for Employee table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'EmployeeInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [EmployeeInsert] 
GO

CREATE PROCEDURE [EmployeeInsert] 
      @FirstName varchar(8000)
     ,@LastName varchar(8000)
     ,@DOB datetime
     ,@DOJ datetime
     ,@Gender varchar(8000)
     ,@EMail varchar(8000)
     ,@Mobile varchar(8000)
     ,@Address1 varchar(8000)
     ,@Address2 varchar(8000)
     ,@Salary decimal(18,2)
     ,@SignatureURL varchar(8000)
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

INSERT [Employee]
     (
      [FirstName]
     ,[LastName]
     ,[DOB]
     ,[DOJ]
     ,[Gender]
     ,[EMail]
     ,[Mobile]
     ,[Address1]
     ,[Address2]
     ,[Salary]
     ,[SignatureURL]
     ,[CompanyID]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
     )
VALUES
     (
      @FirstName
     ,@LastName
     ,@DOB
     ,@DOJ
     ,@Gender
     ,@EMail
     ,@Mobile
     ,@Address1
     ,@Address2
     ,@Salary
     ,@SignatureURL
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

GRANT EXECUTE ON [EmployeeInsert] TO [Public]
GO
 
