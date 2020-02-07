--***********************************************************
--SEARCH Stored Procedure for Employee table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'EmployeeSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [EmployeeSearch] 
GO

CREATE PROCEDURE [EmployeeSearch] 
      @EmployeeID int
     ,@FirstName varchar(8000)
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
     ,@CompanyName varchar(-1)
     ,@AddUserID int
     ,@AddDate datetime
     ,@ArchiveUserID int
     ,@ArchiveDate datetime
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [Employee].[EmployeeID]
     ,[Employee].[FirstName]
     ,[Employee].[LastName]
     ,[Employee].[DOB]
     ,[Employee].[DOJ]
     ,[Employee].[Gender]
     ,[Employee].[EMail]
     ,[Employee].[Mobile]
     ,[Employee].[Address1]
     ,[Employee].[Address2]
     ,[Employee].[Salary]
     ,[Employee].[SignatureURL]
     ,[Employee].[CompanyID]
     ,[Employee].[AddUserID]
     ,[Employee].[AddDate]
     ,[Employee].[ArchiveUserID]
     ,[Employee].[ArchiveDate]
FROM
     [Employee]
     INNER JOIN [Company] ON [Employee].[CompanyID] = [Company].[CompanyID]
WHERE
      (@EmployeeID IS NULL OR @EmployeeID = '' OR [Employee].[EmployeeID] LIKE '%' + LTRIM(RTRIM(@EmployeeID)) + '%')
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] LIKE '%' + LTRIM(RTRIM(@FirstName)) + '%')
AND   (@LastName IS NULL OR @LastName = '' OR [Employee].[LastName] LIKE '%' + LTRIM(RTRIM(@LastName)) + '%')
AND   (@DOB IS NULL OR @DOB = '' OR [Employee].[DOB] LIKE '%' + LTRIM(RTRIM(@DOB)) + '%')
AND   (@DOJ IS NULL OR @DOJ = '' OR [Employee].[DOJ] LIKE '%' + LTRIM(RTRIM(@DOJ)) + '%')
AND   (@Gender IS NULL OR @Gender = '' OR [Employee].[Gender] LIKE '%' + LTRIM(RTRIM(@Gender)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [Employee].[EMail] LIKE '%' + LTRIM(RTRIM(@EMail)) + '%')
AND   (@Mobile IS NULL OR @Mobile = '' OR [Employee].[Mobile] LIKE '%' + LTRIM(RTRIM(@Mobile)) + '%')
AND   (@Address1 IS NULL OR @Address1 = '' OR [Employee].[Address1] LIKE '%' + LTRIM(RTRIM(@Address1)) + '%')
AND   (@Address2 IS NULL OR @Address2 = '' OR [Employee].[Address2] LIKE '%' + LTRIM(RTRIM(@Address2)) + '%')
AND   (@Salary IS NULL OR @Salary = '' OR [Employee].[Salary] LIKE '%' + LTRIM(RTRIM(@Salary)) + '%')
AND   (@SignatureURL IS NULL OR @SignatureURL = '' OR [Employee].[SignatureURL] LIKE '%' + LTRIM(RTRIM(@SignatureURL)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE '%' + LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Employee].[AddUserID] LIKE '%' + LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Employee].[AddDate] LIKE '%' + LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Employee].[ArchiveUserID] LIKE '%' + LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Employee].[ArchiveDate] LIKE '%' + LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Employee].[EmployeeID]
     ,[Employee].[FirstName]
     ,[Employee].[LastName]
     ,[Employee].[DOB]
     ,[Employee].[DOJ]
     ,[Employee].[Gender]
     ,[Employee].[EMail]
     ,[Employee].[Mobile]
     ,[Employee].[Address1]
     ,[Employee].[Address2]
     ,[Employee].[Salary]
     ,[Employee].[SignatureURL]
     ,[Employee].[CompanyID]
     ,[Employee].[AddUserID]
     ,[Employee].[AddDate]
     ,[Employee].[ArchiveUserID]
     ,[Employee].[ArchiveDate]
FROM
     [Employee]
     INNER JOIN [Company] ON [Employee].[CompanyID] = [Company].[CompanyID]
WHERE
      (@EmployeeID IS NULL OR @EmployeeID = '' OR [Employee].[EmployeeID] = LTRIM(RTRIM(@EmployeeID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] = LTRIM(RTRIM(@FirstName)))
AND   (@LastName IS NULL OR @LastName = '' OR [Employee].[LastName] = LTRIM(RTRIM(@LastName)))
AND   (@DOB IS NULL OR @DOB = '' OR [Employee].[DOB] = LTRIM(RTRIM(@DOB)))
AND   (@DOJ IS NULL OR @DOJ = '' OR [Employee].[DOJ] = LTRIM(RTRIM(@DOJ)))
AND   (@Gender IS NULL OR @Gender = '' OR [Employee].[Gender] = LTRIM(RTRIM(@Gender)))
AND   (@EMail IS NULL OR @EMail = '' OR [Employee].[EMail] = LTRIM(RTRIM(@EMail)))
AND   (@Mobile IS NULL OR @Mobile = '' OR [Employee].[Mobile] = LTRIM(RTRIM(@Mobile)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Employee].[Address1] = LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Employee].[Address2] = LTRIM(RTRIM(@Address2)))
AND   (@Salary IS NULL OR @Salary = '' OR [Employee].[Salary] = LTRIM(RTRIM(@Salary)))
AND   (@SignatureURL IS NULL OR @SignatureURL = '' OR [Employee].[SignatureURL] = LTRIM(RTRIM(@SignatureURL)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] = LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Employee].[AddUserID] = LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Employee].[AddDate] = LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Employee].[ArchiveUserID] = LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Employee].[ArchiveDate] = LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Employee].[EmployeeID]
     ,[Employee].[FirstName]
     ,[Employee].[LastName]
     ,[Employee].[DOB]
     ,[Employee].[DOJ]
     ,[Employee].[Gender]
     ,[Employee].[EMail]
     ,[Employee].[Mobile]
     ,[Employee].[Address1]
     ,[Employee].[Address2]
     ,[Employee].[Salary]
     ,[Employee].[SignatureURL]
     ,[Employee].[CompanyID]
     ,[Employee].[AddUserID]
     ,[Employee].[AddDate]
     ,[Employee].[ArchiveUserID]
     ,[Employee].[ArchiveDate]
FROM
     [Employee]
     INNER JOIN [Company] ON [Employee].[CompanyID] = [Company].[CompanyID]
WHERE
      (@EmployeeID IS NULL OR @EmployeeID = '' OR [Employee].[EmployeeID] LIKE LTRIM(RTRIM(@EmployeeID)) + '%')
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] LIKE LTRIM(RTRIM(@FirstName)) + '%')
AND   (@LastName IS NULL OR @LastName = '' OR [Employee].[LastName] LIKE LTRIM(RTRIM(@LastName)) + '%')
AND   (@DOB IS NULL OR @DOB = '' OR [Employee].[DOB] LIKE LTRIM(RTRIM(@DOB)) + '%')
AND   (@DOJ IS NULL OR @DOJ = '' OR [Employee].[DOJ] LIKE LTRIM(RTRIM(@DOJ)) + '%')
AND   (@Gender IS NULL OR @Gender = '' OR [Employee].[Gender] LIKE LTRIM(RTRIM(@Gender)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [Employee].[EMail] LIKE LTRIM(RTRIM(@EMail)) + '%')
AND   (@Mobile IS NULL OR @Mobile = '' OR [Employee].[Mobile] LIKE LTRIM(RTRIM(@Mobile)) + '%')
AND   (@Address1 IS NULL OR @Address1 = '' OR [Employee].[Address1] LIKE LTRIM(RTRIM(@Address1)) + '%')
AND   (@Address2 IS NULL OR @Address2 = '' OR [Employee].[Address2] LIKE LTRIM(RTRIM(@Address2)) + '%')
AND   (@Salary IS NULL OR @Salary = '' OR [Employee].[Salary] LIKE LTRIM(RTRIM(@Salary)) + '%')
AND   (@SignatureURL IS NULL OR @SignatureURL = '' OR [Employee].[SignatureURL] LIKE LTRIM(RTRIM(@SignatureURL)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Employee].[AddUserID] LIKE LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Employee].[AddDate] LIKE LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Employee].[ArchiveUserID] LIKE LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Employee].[ArchiveDate] LIKE LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Employee].[EmployeeID]
     ,[Employee].[FirstName]
     ,[Employee].[LastName]
     ,[Employee].[DOB]
     ,[Employee].[DOJ]
     ,[Employee].[Gender]
     ,[Employee].[EMail]
     ,[Employee].[Mobile]
     ,[Employee].[Address1]
     ,[Employee].[Address2]
     ,[Employee].[Salary]
     ,[Employee].[SignatureURL]
     ,[Employee].[CompanyID]
     ,[Employee].[AddUserID]
     ,[Employee].[AddDate]
     ,[Employee].[ArchiveUserID]
     ,[Employee].[ArchiveDate]
FROM
     [Employee]
     INNER JOIN [Company] ON [Employee].[CompanyID] = [Company].[CompanyID]
WHERE
      (@EmployeeID IS NULL OR @EmployeeID = '' OR [Employee].[EmployeeID] > LTRIM(RTRIM(@EmployeeID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] > LTRIM(RTRIM(@FirstName)))
AND   (@LastName IS NULL OR @LastName = '' OR [Employee].[LastName] > LTRIM(RTRIM(@LastName)))
AND   (@DOB IS NULL OR @DOB = '' OR [Employee].[DOB] > LTRIM(RTRIM(@DOB)))
AND   (@DOJ IS NULL OR @DOJ = '' OR [Employee].[DOJ] > LTRIM(RTRIM(@DOJ)))
AND   (@Gender IS NULL OR @Gender = '' OR [Employee].[Gender] > LTRIM(RTRIM(@Gender)))
AND   (@EMail IS NULL OR @EMail = '' OR [Employee].[EMail] > LTRIM(RTRIM(@EMail)))
AND   (@Mobile IS NULL OR @Mobile = '' OR [Employee].[Mobile] > LTRIM(RTRIM(@Mobile)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Employee].[Address1] > LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Employee].[Address2] > LTRIM(RTRIM(@Address2)))
AND   (@Salary IS NULL OR @Salary = '' OR [Employee].[Salary] > LTRIM(RTRIM(@Salary)))
AND   (@SignatureURL IS NULL OR @SignatureURL = '' OR [Employee].[SignatureURL] > LTRIM(RTRIM(@SignatureURL)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] > LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Employee].[AddUserID] > LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Employee].[AddDate] > LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Employee].[ArchiveUserID] > LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Employee].[ArchiveDate] > LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Employee].[EmployeeID]
     ,[Employee].[FirstName]
     ,[Employee].[LastName]
     ,[Employee].[DOB]
     ,[Employee].[DOJ]
     ,[Employee].[Gender]
     ,[Employee].[EMail]
     ,[Employee].[Mobile]
     ,[Employee].[Address1]
     ,[Employee].[Address2]
     ,[Employee].[Salary]
     ,[Employee].[SignatureURL]
     ,[Employee].[CompanyID]
     ,[Employee].[AddUserID]
     ,[Employee].[AddDate]
     ,[Employee].[ArchiveUserID]
     ,[Employee].[ArchiveDate]
FROM
     [Employee]
     INNER JOIN [Company] ON [Employee].[CompanyID] = [Company].[CompanyID]
WHERE
      (@EmployeeID IS NULL OR @EmployeeID = '' OR [Employee].[EmployeeID] < LTRIM(RTRIM(@EmployeeID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] < LTRIM(RTRIM(@FirstName)))
AND   (@LastName IS NULL OR @LastName = '' OR [Employee].[LastName] < LTRIM(RTRIM(@LastName)))
AND   (@DOB IS NULL OR @DOB = '' OR [Employee].[DOB] < LTRIM(RTRIM(@DOB)))
AND   (@DOJ IS NULL OR @DOJ = '' OR [Employee].[DOJ] < LTRIM(RTRIM(@DOJ)))
AND   (@Gender IS NULL OR @Gender = '' OR [Employee].[Gender] < LTRIM(RTRIM(@Gender)))
AND   (@EMail IS NULL OR @EMail = '' OR [Employee].[EMail] < LTRIM(RTRIM(@EMail)))
AND   (@Mobile IS NULL OR @Mobile = '' OR [Employee].[Mobile] < LTRIM(RTRIM(@Mobile)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Employee].[Address1] < LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Employee].[Address2] < LTRIM(RTRIM(@Address2)))
AND   (@Salary IS NULL OR @Salary = '' OR [Employee].[Salary] < LTRIM(RTRIM(@Salary)))
AND   (@SignatureURL IS NULL OR @SignatureURL = '' OR [Employee].[SignatureURL] < LTRIM(RTRIM(@SignatureURL)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] < LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Employee].[AddUserID] < LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Employee].[AddDate] < LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Employee].[ArchiveUserID] < LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Employee].[ArchiveDate] < LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Employee].[EmployeeID]
     ,[Employee].[FirstName]
     ,[Employee].[LastName]
     ,[Employee].[DOB]
     ,[Employee].[DOJ]
     ,[Employee].[Gender]
     ,[Employee].[EMail]
     ,[Employee].[Mobile]
     ,[Employee].[Address1]
     ,[Employee].[Address2]
     ,[Employee].[Salary]
     ,[Employee].[SignatureURL]
     ,[Employee].[CompanyID]
     ,[Employee].[AddUserID]
     ,[Employee].[AddDate]
     ,[Employee].[ArchiveUserID]
     ,[Employee].[ArchiveDate]
FROM
     [Employee]
     INNER JOIN [Company] ON [Employee].[CompanyID] = [Company].[CompanyID]
WHERE
      (@EmployeeID IS NULL OR @EmployeeID = '' OR [Employee].[EmployeeID] >= LTRIM(RTRIM(@EmployeeID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] >= LTRIM(RTRIM(@FirstName)))
AND   (@LastName IS NULL OR @LastName = '' OR [Employee].[LastName] >= LTRIM(RTRIM(@LastName)))
AND   (@DOB IS NULL OR @DOB = '' OR [Employee].[DOB] >= LTRIM(RTRIM(@DOB)))
AND   (@DOJ IS NULL OR @DOJ = '' OR [Employee].[DOJ] >= LTRIM(RTRIM(@DOJ)))
AND   (@Gender IS NULL OR @Gender = '' OR [Employee].[Gender] >= LTRIM(RTRIM(@Gender)))
AND   (@EMail IS NULL OR @EMail = '' OR [Employee].[EMail] >= LTRIM(RTRIM(@EMail)))
AND   (@Mobile IS NULL OR @Mobile = '' OR [Employee].[Mobile] >= LTRIM(RTRIM(@Mobile)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Employee].[Address1] >= LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Employee].[Address2] >= LTRIM(RTRIM(@Address2)))
AND   (@Salary IS NULL OR @Salary = '' OR [Employee].[Salary] >= LTRIM(RTRIM(@Salary)))
AND   (@SignatureURL IS NULL OR @SignatureURL = '' OR [Employee].[SignatureURL] >= LTRIM(RTRIM(@SignatureURL)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] >= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Employee].[AddUserID] >= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Employee].[AddDate] >= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Employee].[ArchiveUserID] >= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Employee].[ArchiveDate] >= LTRIM(RTRIM(@ArchiveDate)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Employee].[EmployeeID]
     ,[Employee].[FirstName]
     ,[Employee].[LastName]
     ,[Employee].[DOB]
     ,[Employee].[DOJ]
     ,[Employee].[Gender]
     ,[Employee].[EMail]
     ,[Employee].[Mobile]
     ,[Employee].[Address1]
     ,[Employee].[Address2]
     ,[Employee].[Salary]
     ,[Employee].[SignatureURL]
     ,[Employee].[CompanyID]
     ,[Employee].[AddUserID]
     ,[Employee].[AddDate]
     ,[Employee].[ArchiveUserID]
     ,[Employee].[ArchiveDate]
FROM
     [Employee]
     INNER JOIN [Company] ON [Employee].[CompanyID] = [Company].[CompanyID]
WHERE
      (@EmployeeID IS NULL OR @EmployeeID = '' OR [Employee].[EmployeeID] <= LTRIM(RTRIM(@EmployeeID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] <= LTRIM(RTRIM(@FirstName)))
AND   (@LastName IS NULL OR @LastName = '' OR [Employee].[LastName] <= LTRIM(RTRIM(@LastName)))
AND   (@DOB IS NULL OR @DOB = '' OR [Employee].[DOB] <= LTRIM(RTRIM(@DOB)))
AND   (@DOJ IS NULL OR @DOJ = '' OR [Employee].[DOJ] <= LTRIM(RTRIM(@DOJ)))
AND   (@Gender IS NULL OR @Gender = '' OR [Employee].[Gender] <= LTRIM(RTRIM(@Gender)))
AND   (@EMail IS NULL OR @EMail = '' OR [Employee].[EMail] <= LTRIM(RTRIM(@EMail)))
AND   (@Mobile IS NULL OR @Mobile = '' OR [Employee].[Mobile] <= LTRIM(RTRIM(@Mobile)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Employee].[Address1] <= LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Employee].[Address2] <= LTRIM(RTRIM(@Address2)))
AND   (@Salary IS NULL OR @Salary = '' OR [Employee].[Salary] <= LTRIM(RTRIM(@Salary)))
AND   (@SignatureURL IS NULL OR @SignatureURL = '' OR [Employee].[SignatureURL] <= LTRIM(RTRIM(@SignatureURL)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] <= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Employee].[AddUserID] <= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Employee].[AddDate] <= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Employee].[ArchiveUserID] <= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Employee].[ArchiveDate] <= LTRIM(RTRIM(@ArchiveDate)))

END

END
GO

GRANT EXECUTE ON [EmployeeSearch] TO [Public]
GO

 
