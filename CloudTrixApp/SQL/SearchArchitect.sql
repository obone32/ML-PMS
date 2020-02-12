--***********************************************************
--SEARCH Stored Procedure for Architect table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectSearch] 
GO

CREATE PROCEDURE [ArchitectSearch] 
      @ArchitectID int
     ,@ArchitectName varchar(8000)
     ,@Address1 varchar(8000)
     ,@Address2 varchar(8000)
     ,@City varchar(8000)
     ,@District varchar(8000)
     ,@State varchar(8000)
     ,@Country varchar(8000)
     ,@Pincode varchar(8000)
     ,@EMail varchar(8000)
     ,@ContactNo varchar(8000)
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
      [Architect].[ArchitectID]
     ,[Architect].[ArchitectName]
     ,[Architect].[Address1]
     ,[Architect].[Address2]
     ,[Architect].[City]
     ,[Architect].[District]
     ,[Architect].[State]
     ,[Architect].[Country]
     ,[Architect].[Pincode]
     ,[Architect].[EMail]
     ,[Architect].[ContactNo]
     ,[Architect].[CompanyID]
     ,[Architect].[AddUserID]
     ,[Architect].[AddDate]
     ,[Architect].[ArchiveUserID]
     ,[Architect].[ArchiveDate]
FROM
     [Architect]
     INNER JOIN [Company] ON [Architect].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ArchitectID IS NULL OR @ArchitectID = '' OR [Architect].[ArchitectID] LIKE '%' + LTRIM(RTRIM(@ArchitectID)) + '%')
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] LIKE '%' + LTRIM(RTRIM(@ArchitectName)) + '%')
AND   (@Address1 IS NULL OR @Address1 = '' OR [Architect].[Address1] LIKE '%' + LTRIM(RTRIM(@Address1)) + '%')
AND   (@Address2 IS NULL OR @Address2 = '' OR [Architect].[Address2] LIKE '%' + LTRIM(RTRIM(@Address2)) + '%')
AND   (@City IS NULL OR @City = '' OR [Architect].[City] LIKE '%' + LTRIM(RTRIM(@City)) + '%')
AND   (@District IS NULL OR @District = '' OR [Architect].[District] LIKE '%' + LTRIM(RTRIM(@District)) + '%')
AND   (@State IS NULL OR @State = '' OR [Architect].[State] LIKE '%' + LTRIM(RTRIM(@State)) + '%')
AND   (@Country IS NULL OR @Country = '' OR [Architect].[Country] LIKE '%' + LTRIM(RTRIM(@Country)) + '%')
AND   (@Pincode IS NULL OR @Pincode = '' OR [Architect].[Pincode] LIKE '%' + LTRIM(RTRIM(@Pincode)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [Architect].[EMail] LIKE '%' + LTRIM(RTRIM(@EMail)) + '%')
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Architect].[ContactNo] LIKE '%' + LTRIM(RTRIM(@ContactNo)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE '%' + LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Architect].[AddUserID] LIKE '%' + LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Architect].[AddDate] LIKE '%' + LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Architect].[ArchiveUserID] LIKE '%' + LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Architect].[ArchiveDate] LIKE '%' + LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Architect].[ArchitectID]
     ,[Architect].[ArchitectName]
     ,[Architect].[Address1]
     ,[Architect].[Address2]
     ,[Architect].[City]
     ,[Architect].[District]
     ,[Architect].[State]
     ,[Architect].[Country]
     ,[Architect].[Pincode]
     ,[Architect].[EMail]
     ,[Architect].[ContactNo]
     ,[Architect].[CompanyID]
     ,[Architect].[AddUserID]
     ,[Architect].[AddDate]
     ,[Architect].[ArchiveUserID]
     ,[Architect].[ArchiveDate]
FROM
     [Architect]
     INNER JOIN [Company] ON [Architect].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ArchitectID IS NULL OR @ArchitectID = '' OR [Architect].[ArchitectID] = LTRIM(RTRIM(@ArchitectID)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] = LTRIM(RTRIM(@ArchitectName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Architect].[Address1] = LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Architect].[Address2] = LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Architect].[City] = LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Architect].[District] = LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Architect].[State] = LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Architect].[Country] = LTRIM(RTRIM(@Country)))
AND   (@Pincode IS NULL OR @Pincode = '' OR [Architect].[Pincode] = LTRIM(RTRIM(@Pincode)))
AND   (@EMail IS NULL OR @EMail = '' OR [Architect].[EMail] = LTRIM(RTRIM(@EMail)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Architect].[ContactNo] = LTRIM(RTRIM(@ContactNo)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] = LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Architect].[AddUserID] = LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Architect].[AddDate] = LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Architect].[ArchiveUserID] = LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Architect].[ArchiveDate] = LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Architect].[ArchitectID]
     ,[Architect].[ArchitectName]
     ,[Architect].[Address1]
     ,[Architect].[Address2]
     ,[Architect].[City]
     ,[Architect].[District]
     ,[Architect].[State]
     ,[Architect].[Country]
     ,[Architect].[Pincode]
     ,[Architect].[EMail]
     ,[Architect].[ContactNo]
     ,[Architect].[CompanyID]
     ,[Architect].[AddUserID]
     ,[Architect].[AddDate]
     ,[Architect].[ArchiveUserID]
     ,[Architect].[ArchiveDate]
FROM
     [Architect]
     INNER JOIN [Company] ON [Architect].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ArchitectID IS NULL OR @ArchitectID = '' OR [Architect].[ArchitectID] LIKE LTRIM(RTRIM(@ArchitectID)) + '%')
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] LIKE LTRIM(RTRIM(@ArchitectName)) + '%')
AND   (@Address1 IS NULL OR @Address1 = '' OR [Architect].[Address1] LIKE LTRIM(RTRIM(@Address1)) + '%')
AND   (@Address2 IS NULL OR @Address2 = '' OR [Architect].[Address2] LIKE LTRIM(RTRIM(@Address2)) + '%')
AND   (@City IS NULL OR @City = '' OR [Architect].[City] LIKE LTRIM(RTRIM(@City)) + '%')
AND   (@District IS NULL OR @District = '' OR [Architect].[District] LIKE LTRIM(RTRIM(@District)) + '%')
AND   (@State IS NULL OR @State = '' OR [Architect].[State] LIKE LTRIM(RTRIM(@State)) + '%')
AND   (@Country IS NULL OR @Country = '' OR [Architect].[Country] LIKE LTRIM(RTRIM(@Country)) + '%')
AND   (@Pincode IS NULL OR @Pincode = '' OR [Architect].[Pincode] LIKE LTRIM(RTRIM(@Pincode)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [Architect].[EMail] LIKE LTRIM(RTRIM(@EMail)) + '%')
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Architect].[ContactNo] LIKE LTRIM(RTRIM(@ContactNo)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Architect].[AddUserID] LIKE LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Architect].[AddDate] LIKE LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Architect].[ArchiveUserID] LIKE LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Architect].[ArchiveDate] LIKE LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Architect].[ArchitectID]
     ,[Architect].[ArchitectName]
     ,[Architect].[Address1]
     ,[Architect].[Address2]
     ,[Architect].[City]
     ,[Architect].[District]
     ,[Architect].[State]
     ,[Architect].[Country]
     ,[Architect].[Pincode]
     ,[Architect].[EMail]
     ,[Architect].[ContactNo]
     ,[Architect].[CompanyID]
     ,[Architect].[AddUserID]
     ,[Architect].[AddDate]
     ,[Architect].[ArchiveUserID]
     ,[Architect].[ArchiveDate]
FROM
     [Architect]
     INNER JOIN [Company] ON [Architect].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ArchitectID IS NULL OR @ArchitectID = '' OR [Architect].[ArchitectID] > LTRIM(RTRIM(@ArchitectID)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] > LTRIM(RTRIM(@ArchitectName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Architect].[Address1] > LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Architect].[Address2] > LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Architect].[City] > LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Architect].[District] > LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Architect].[State] > LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Architect].[Country] > LTRIM(RTRIM(@Country)))
AND   (@Pincode IS NULL OR @Pincode = '' OR [Architect].[Pincode] > LTRIM(RTRIM(@Pincode)))
AND   (@EMail IS NULL OR @EMail = '' OR [Architect].[EMail] > LTRIM(RTRIM(@EMail)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Architect].[ContactNo] > LTRIM(RTRIM(@ContactNo)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] > LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Architect].[AddUserID] > LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Architect].[AddDate] > LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Architect].[ArchiveUserID] > LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Architect].[ArchiveDate] > LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Architect].[ArchitectID]
     ,[Architect].[ArchitectName]
     ,[Architect].[Address1]
     ,[Architect].[Address2]
     ,[Architect].[City]
     ,[Architect].[District]
     ,[Architect].[State]
     ,[Architect].[Country]
     ,[Architect].[Pincode]
     ,[Architect].[EMail]
     ,[Architect].[ContactNo]
     ,[Architect].[CompanyID]
     ,[Architect].[AddUserID]
     ,[Architect].[AddDate]
     ,[Architect].[ArchiveUserID]
     ,[Architect].[ArchiveDate]
FROM
     [Architect]
     INNER JOIN [Company] ON [Architect].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ArchitectID IS NULL OR @ArchitectID = '' OR [Architect].[ArchitectID] < LTRIM(RTRIM(@ArchitectID)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] < LTRIM(RTRIM(@ArchitectName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Architect].[Address1] < LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Architect].[Address2] < LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Architect].[City] < LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Architect].[District] < LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Architect].[State] < LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Architect].[Country] < LTRIM(RTRIM(@Country)))
AND   (@Pincode IS NULL OR @Pincode = '' OR [Architect].[Pincode] < LTRIM(RTRIM(@Pincode)))
AND   (@EMail IS NULL OR @EMail = '' OR [Architect].[EMail] < LTRIM(RTRIM(@EMail)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Architect].[ContactNo] < LTRIM(RTRIM(@ContactNo)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] < LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Architect].[AddUserID] < LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Architect].[AddDate] < LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Architect].[ArchiveUserID] < LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Architect].[ArchiveDate] < LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Architect].[ArchitectID]
     ,[Architect].[ArchitectName]
     ,[Architect].[Address1]
     ,[Architect].[Address2]
     ,[Architect].[City]
     ,[Architect].[District]
     ,[Architect].[State]
     ,[Architect].[Country]
     ,[Architect].[Pincode]
     ,[Architect].[EMail]
     ,[Architect].[ContactNo]
     ,[Architect].[CompanyID]
     ,[Architect].[AddUserID]
     ,[Architect].[AddDate]
     ,[Architect].[ArchiveUserID]
     ,[Architect].[ArchiveDate]
FROM
     [Architect]
     INNER JOIN [Company] ON [Architect].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ArchitectID IS NULL OR @ArchitectID = '' OR [Architect].[ArchitectID] >= LTRIM(RTRIM(@ArchitectID)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] >= LTRIM(RTRIM(@ArchitectName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Architect].[Address1] >= LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Architect].[Address2] >= LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Architect].[City] >= LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Architect].[District] >= LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Architect].[State] >= LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Architect].[Country] >= LTRIM(RTRIM(@Country)))
AND   (@Pincode IS NULL OR @Pincode = '' OR [Architect].[Pincode] >= LTRIM(RTRIM(@Pincode)))
AND   (@EMail IS NULL OR @EMail = '' OR [Architect].[EMail] >= LTRIM(RTRIM(@EMail)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Architect].[ContactNo] >= LTRIM(RTRIM(@ContactNo)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] >= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Architect].[AddUserID] >= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Architect].[AddDate] >= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Architect].[ArchiveUserID] >= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Architect].[ArchiveDate] >= LTRIM(RTRIM(@ArchiveDate)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Architect].[ArchitectID]
     ,[Architect].[ArchitectName]
     ,[Architect].[Address1]
     ,[Architect].[Address2]
     ,[Architect].[City]
     ,[Architect].[District]
     ,[Architect].[State]
     ,[Architect].[Country]
     ,[Architect].[Pincode]
     ,[Architect].[EMail]
     ,[Architect].[ContactNo]
     ,[Architect].[CompanyID]
     ,[Architect].[AddUserID]
     ,[Architect].[AddDate]
     ,[Architect].[ArchiveUserID]
     ,[Architect].[ArchiveDate]
FROM
     [Architect]
     INNER JOIN [Company] ON [Architect].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ArchitectID IS NULL OR @ArchitectID = '' OR [Architect].[ArchitectID] <= LTRIM(RTRIM(@ArchitectID)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] <= LTRIM(RTRIM(@ArchitectName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Architect].[Address1] <= LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Architect].[Address2] <= LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Architect].[City] <= LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Architect].[District] <= LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Architect].[State] <= LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Architect].[Country] <= LTRIM(RTRIM(@Country)))
AND   (@Pincode IS NULL OR @Pincode = '' OR [Architect].[Pincode] <= LTRIM(RTRIM(@Pincode)))
AND   (@EMail IS NULL OR @EMail = '' OR [Architect].[EMail] <= LTRIM(RTRIM(@EMail)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Architect].[ContactNo] <= LTRIM(RTRIM(@ContactNo)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] <= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Architect].[AddUserID] <= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Architect].[AddDate] <= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Architect].[ArchiveUserID] <= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Architect].[ArchiveDate] <= LTRIM(RTRIM(@ArchiveDate)))

END

END
GO

GRANT EXECUTE ON [ArchitectSearch] TO [Public]
GO

 
