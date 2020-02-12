--***********************************************************
--SEARCH Stored Procedure for Client table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ClientSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [ClientSearch] 
GO

CREATE PROCEDURE [ClientSearch] 
      @ClientID int
     ,@ClientName varchar(8000)
     ,@Address1 varchar(8000)
     ,@Address2 varchar(8000)
     ,@City varchar(8000)
     ,@District varchar(8000)
     ,@State varchar(8000)
     ,@PinCode varchar(8000)
     ,@ContactNo varchar(8000)
     ,@EMail varchar(8000)
     ,@GSTIN varchar(8000)
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
      [Client].[ClientID]
     ,[Client].[ClientName]
     ,[Client].[Address1]
     ,[Client].[Address2]
     ,[Client].[City]
     ,[Client].[District]
     ,[Client].[State]
     ,[Client].[PinCode]
     ,[Client].[ContactNo]
     ,[Client].[EMail]
     ,[Client].[GSTIN]
     ,[Client].[CompanyID]
     ,[Client].[AddUserID]
     ,[Client].[AddDate]
     ,[Client].[ArchiveUserID]
     ,[Client].[ArchiveDate]
FROM
     [Client]
     INNER JOIN [Company] ON [Client].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ClientID IS NULL OR @ClientID = '' OR [Client].[ClientID] LIKE '%' + LTRIM(RTRIM(@ClientID)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] LIKE '%' + LTRIM(RTRIM(@ClientName)) + '%')
AND   (@Address1 IS NULL OR @Address1 = '' OR [Client].[Address1] LIKE '%' + LTRIM(RTRIM(@Address1)) + '%')
AND   (@Address2 IS NULL OR @Address2 = '' OR [Client].[Address2] LIKE '%' + LTRIM(RTRIM(@Address2)) + '%')
AND   (@City IS NULL OR @City = '' OR [Client].[City] LIKE '%' + LTRIM(RTRIM(@City)) + '%')
AND   (@District IS NULL OR @District = '' OR [Client].[District] LIKE '%' + LTRIM(RTRIM(@District)) + '%')
AND   (@State IS NULL OR @State = '' OR [Client].[State] LIKE '%' + LTRIM(RTRIM(@State)) + '%')
AND   (@PinCode IS NULL OR @PinCode = '' OR [Client].[PinCode] LIKE '%' + LTRIM(RTRIM(@PinCode)) + '%')
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Client].[ContactNo] LIKE '%' + LTRIM(RTRIM(@ContactNo)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [Client].[EMail] LIKE '%' + LTRIM(RTRIM(@EMail)) + '%')
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Client].[GSTIN] LIKE '%' + LTRIM(RTRIM(@GSTIN)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE '%' + LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Client].[AddUserID] LIKE '%' + LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Client].[AddDate] LIKE '%' + LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Client].[ArchiveUserID] LIKE '%' + LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Client].[ArchiveDate] LIKE '%' + LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Client].[ClientID]
     ,[Client].[ClientName]
     ,[Client].[Address1]
     ,[Client].[Address2]
     ,[Client].[City]
     ,[Client].[District]
     ,[Client].[State]
     ,[Client].[PinCode]
     ,[Client].[ContactNo]
     ,[Client].[EMail]
     ,[Client].[GSTIN]
     ,[Client].[CompanyID]
     ,[Client].[AddUserID]
     ,[Client].[AddDate]
     ,[Client].[ArchiveUserID]
     ,[Client].[ArchiveDate]
FROM
     [Client]
     INNER JOIN [Company] ON [Client].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ClientID IS NULL OR @ClientID = '' OR [Client].[ClientID] = LTRIM(RTRIM(@ClientID)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] = LTRIM(RTRIM(@ClientName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Client].[Address1] = LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Client].[Address2] = LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Client].[City] = LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Client].[District] = LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Client].[State] = LTRIM(RTRIM(@State)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Client].[PinCode] = LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Client].[ContactNo] = LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Client].[EMail] = LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Client].[GSTIN] = LTRIM(RTRIM(@GSTIN)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] = LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Client].[AddUserID] = LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Client].[AddDate] = LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Client].[ArchiveUserID] = LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Client].[ArchiveDate] = LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Client].[ClientID]
     ,[Client].[ClientName]
     ,[Client].[Address1]
     ,[Client].[Address2]
     ,[Client].[City]
     ,[Client].[District]
     ,[Client].[State]
     ,[Client].[PinCode]
     ,[Client].[ContactNo]
     ,[Client].[EMail]
     ,[Client].[GSTIN]
     ,[Client].[CompanyID]
     ,[Client].[AddUserID]
     ,[Client].[AddDate]
     ,[Client].[ArchiveUserID]
     ,[Client].[ArchiveDate]
FROM
     [Client]
     INNER JOIN [Company] ON [Client].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ClientID IS NULL OR @ClientID = '' OR [Client].[ClientID] LIKE LTRIM(RTRIM(@ClientID)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] LIKE LTRIM(RTRIM(@ClientName)) + '%')
AND   (@Address1 IS NULL OR @Address1 = '' OR [Client].[Address1] LIKE LTRIM(RTRIM(@Address1)) + '%')
AND   (@Address2 IS NULL OR @Address2 = '' OR [Client].[Address2] LIKE LTRIM(RTRIM(@Address2)) + '%')
AND   (@City IS NULL OR @City = '' OR [Client].[City] LIKE LTRIM(RTRIM(@City)) + '%')
AND   (@District IS NULL OR @District = '' OR [Client].[District] LIKE LTRIM(RTRIM(@District)) + '%')
AND   (@State IS NULL OR @State = '' OR [Client].[State] LIKE LTRIM(RTRIM(@State)) + '%')
AND   (@PinCode IS NULL OR @PinCode = '' OR [Client].[PinCode] LIKE LTRIM(RTRIM(@PinCode)) + '%')
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Client].[ContactNo] LIKE LTRIM(RTRIM(@ContactNo)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [Client].[EMail] LIKE LTRIM(RTRIM(@EMail)) + '%')
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Client].[GSTIN] LIKE LTRIM(RTRIM(@GSTIN)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Client].[AddUserID] LIKE LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Client].[AddDate] LIKE LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Client].[ArchiveUserID] LIKE LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Client].[ArchiveDate] LIKE LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Client].[ClientID]
     ,[Client].[ClientName]
     ,[Client].[Address1]
     ,[Client].[Address2]
     ,[Client].[City]
     ,[Client].[District]
     ,[Client].[State]
     ,[Client].[PinCode]
     ,[Client].[ContactNo]
     ,[Client].[EMail]
     ,[Client].[GSTIN]
     ,[Client].[CompanyID]
     ,[Client].[AddUserID]
     ,[Client].[AddDate]
     ,[Client].[ArchiveUserID]
     ,[Client].[ArchiveDate]
FROM
     [Client]
     INNER JOIN [Company] ON [Client].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ClientID IS NULL OR @ClientID = '' OR [Client].[ClientID] > LTRIM(RTRIM(@ClientID)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] > LTRIM(RTRIM(@ClientName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Client].[Address1] > LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Client].[Address2] > LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Client].[City] > LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Client].[District] > LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Client].[State] > LTRIM(RTRIM(@State)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Client].[PinCode] > LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Client].[ContactNo] > LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Client].[EMail] > LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Client].[GSTIN] > LTRIM(RTRIM(@GSTIN)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] > LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Client].[AddUserID] > LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Client].[AddDate] > LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Client].[ArchiveUserID] > LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Client].[ArchiveDate] > LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Client].[ClientID]
     ,[Client].[ClientName]
     ,[Client].[Address1]
     ,[Client].[Address2]
     ,[Client].[City]
     ,[Client].[District]
     ,[Client].[State]
     ,[Client].[PinCode]
     ,[Client].[ContactNo]
     ,[Client].[EMail]
     ,[Client].[GSTIN]
     ,[Client].[CompanyID]
     ,[Client].[AddUserID]
     ,[Client].[AddDate]
     ,[Client].[ArchiveUserID]
     ,[Client].[ArchiveDate]
FROM
     [Client]
     INNER JOIN [Company] ON [Client].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ClientID IS NULL OR @ClientID = '' OR [Client].[ClientID] < LTRIM(RTRIM(@ClientID)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] < LTRIM(RTRIM(@ClientName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Client].[Address1] < LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Client].[Address2] < LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Client].[City] < LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Client].[District] < LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Client].[State] < LTRIM(RTRIM(@State)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Client].[PinCode] < LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Client].[ContactNo] < LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Client].[EMail] < LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Client].[GSTIN] < LTRIM(RTRIM(@GSTIN)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] < LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Client].[AddUserID] < LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Client].[AddDate] < LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Client].[ArchiveUserID] < LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Client].[ArchiveDate] < LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Client].[ClientID]
     ,[Client].[ClientName]
     ,[Client].[Address1]
     ,[Client].[Address2]
     ,[Client].[City]
     ,[Client].[District]
     ,[Client].[State]
     ,[Client].[PinCode]
     ,[Client].[ContactNo]
     ,[Client].[EMail]
     ,[Client].[GSTIN]
     ,[Client].[CompanyID]
     ,[Client].[AddUserID]
     ,[Client].[AddDate]
     ,[Client].[ArchiveUserID]
     ,[Client].[ArchiveDate]
FROM
     [Client]
     INNER JOIN [Company] ON [Client].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ClientID IS NULL OR @ClientID = '' OR [Client].[ClientID] >= LTRIM(RTRIM(@ClientID)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] >= LTRIM(RTRIM(@ClientName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Client].[Address1] >= LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Client].[Address2] >= LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Client].[City] >= LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Client].[District] >= LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Client].[State] >= LTRIM(RTRIM(@State)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Client].[PinCode] >= LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Client].[ContactNo] >= LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Client].[EMail] >= LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Client].[GSTIN] >= LTRIM(RTRIM(@GSTIN)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] >= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Client].[AddUserID] >= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Client].[AddDate] >= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Client].[ArchiveUserID] >= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Client].[ArchiveDate] >= LTRIM(RTRIM(@ArchiveDate)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Client].[ClientID]
     ,[Client].[ClientName]
     ,[Client].[Address1]
     ,[Client].[Address2]
     ,[Client].[City]
     ,[Client].[District]
     ,[Client].[State]
     ,[Client].[PinCode]
     ,[Client].[ContactNo]
     ,[Client].[EMail]
     ,[Client].[GSTIN]
     ,[Client].[CompanyID]
     ,[Client].[AddUserID]
     ,[Client].[AddDate]
     ,[Client].[ArchiveUserID]
     ,[Client].[ArchiveDate]
FROM
     [Client]
     INNER JOIN [Company] ON [Client].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ClientID IS NULL OR @ClientID = '' OR [Client].[ClientID] <= LTRIM(RTRIM(@ClientID)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] <= LTRIM(RTRIM(@ClientName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Client].[Address1] <= LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Client].[Address2] <= LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Client].[City] <= LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Client].[District] <= LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Client].[State] <= LTRIM(RTRIM(@State)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Client].[PinCode] <= LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Client].[ContactNo] <= LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Client].[EMail] <= LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Client].[GSTIN] <= LTRIM(RTRIM(@GSTIN)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] <= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Client].[AddUserID] <= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Client].[AddDate] <= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Client].[ArchiveUserID] <= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Client].[ArchiveDate] <= LTRIM(RTRIM(@ArchiveDate)))

END

END
GO

GRANT EXECUTE ON [ClientSearch] TO [Public]
GO

 
