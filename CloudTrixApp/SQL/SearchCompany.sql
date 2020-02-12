--***********************************************************
--SEARCH Stored Procedure for Company table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'CompanySearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [CompanySearch] 
GO

CREATE PROCEDURE [CompanySearch] 
      @CompanyID int
     ,@CompanyName varchar(8000)
     ,@Address1 varchar(8000)
     ,@Address2 varchar(8000)
     ,@City varchar(8000)
     ,@District varchar(8000)
     ,@State varchar(8000)
     ,@Country varchar(8000)
     ,@PinCode varchar(8000)
     ,@ContactNo varchar(8000)
     ,@EMail varchar(8000)
     ,@GSTIN varchar(8000)
     ,@InvoiceInitials varchar(8000)
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
      [Company].[CompanyID]
     ,[Company].[CompanyName]
     ,[Company].[Address1]
     ,[Company].[Address2]
     ,[Company].[City]
     ,[Company].[District]
     ,[Company].[State]
     ,[Company].[Country]
     ,[Company].[PinCode]
     ,[Company].[ContactNo]
     ,[Company].[EMail]
     ,[Company].[GSTIN]
     ,[Company].[InvoiceInitials]
     ,[Company].[AddUserID]
     ,[Company].[AddDate]
     ,[Company].[ArchiveUserID]
     ,[Company].[ArchiveDate]
FROM
     [Company]
WHERE
      (@CompanyID IS NULL OR @CompanyID = '' OR [Company].[CompanyID] LIKE '%' + LTRIM(RTRIM(@CompanyID)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE '%' + LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@Address1 IS NULL OR @Address1 = '' OR [Company].[Address1] LIKE '%' + LTRIM(RTRIM(@Address1)) + '%')
AND   (@Address2 IS NULL OR @Address2 = '' OR [Company].[Address2] LIKE '%' + LTRIM(RTRIM(@Address2)) + '%')
AND   (@City IS NULL OR @City = '' OR [Company].[City] LIKE '%' + LTRIM(RTRIM(@City)) + '%')
AND   (@District IS NULL OR @District = '' OR [Company].[District] LIKE '%' + LTRIM(RTRIM(@District)) + '%')
AND   (@State IS NULL OR @State = '' OR [Company].[State] LIKE '%' + LTRIM(RTRIM(@State)) + '%')
AND   (@Country IS NULL OR @Country = '' OR [Company].[Country] LIKE '%' + LTRIM(RTRIM(@Country)) + '%')
AND   (@PinCode IS NULL OR @PinCode = '' OR [Company].[PinCode] LIKE '%' + LTRIM(RTRIM(@PinCode)) + '%')
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Company].[ContactNo] LIKE '%' + LTRIM(RTRIM(@ContactNo)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [Company].[EMail] LIKE '%' + LTRIM(RTRIM(@EMail)) + '%')
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Company].[GSTIN] LIKE '%' + LTRIM(RTRIM(@GSTIN)) + '%')
AND   (@InvoiceInitials IS NULL OR @InvoiceInitials = '' OR [Company].[InvoiceInitials] LIKE '%' + LTRIM(RTRIM(@InvoiceInitials)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Company].[AddUserID] LIKE '%' + LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Company].[AddDate] LIKE '%' + LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Company].[ArchiveUserID] LIKE '%' + LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Company].[ArchiveDate] LIKE '%' + LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Company].[CompanyID]
     ,[Company].[CompanyName]
     ,[Company].[Address1]
     ,[Company].[Address2]
     ,[Company].[City]
     ,[Company].[District]
     ,[Company].[State]
     ,[Company].[Country]
     ,[Company].[PinCode]
     ,[Company].[ContactNo]
     ,[Company].[EMail]
     ,[Company].[GSTIN]
     ,[Company].[InvoiceInitials]
     ,[Company].[AddUserID]
     ,[Company].[AddDate]
     ,[Company].[ArchiveUserID]
     ,[Company].[ArchiveDate]
FROM
     [Company]
WHERE
      (@CompanyID IS NULL OR @CompanyID = '' OR [Company].[CompanyID] = LTRIM(RTRIM(@CompanyID)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] = LTRIM(RTRIM(@CompanyName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Company].[Address1] = LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Company].[Address2] = LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Company].[City] = LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Company].[District] = LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Company].[State] = LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Company].[Country] = LTRIM(RTRIM(@Country)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Company].[PinCode] = LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Company].[ContactNo] = LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Company].[EMail] = LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Company].[GSTIN] = LTRIM(RTRIM(@GSTIN)))
AND   (@InvoiceInitials IS NULL OR @InvoiceInitials = '' OR [Company].[InvoiceInitials] = LTRIM(RTRIM(@InvoiceInitials)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Company].[AddUserID] = LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Company].[AddDate] = LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Company].[ArchiveUserID] = LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Company].[ArchiveDate] = LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Company].[CompanyID]
     ,[Company].[CompanyName]
     ,[Company].[Address1]
     ,[Company].[Address2]
     ,[Company].[City]
     ,[Company].[District]
     ,[Company].[State]
     ,[Company].[Country]
     ,[Company].[PinCode]
     ,[Company].[ContactNo]
     ,[Company].[EMail]
     ,[Company].[GSTIN]
     ,[Company].[InvoiceInitials]
     ,[Company].[AddUserID]
     ,[Company].[AddDate]
     ,[Company].[ArchiveUserID]
     ,[Company].[ArchiveDate]
FROM
     [Company]
WHERE
      (@CompanyID IS NULL OR @CompanyID = '' OR [Company].[CompanyID] LIKE LTRIM(RTRIM(@CompanyID)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@Address1 IS NULL OR @Address1 = '' OR [Company].[Address1] LIKE LTRIM(RTRIM(@Address1)) + '%')
AND   (@Address2 IS NULL OR @Address2 = '' OR [Company].[Address2] LIKE LTRIM(RTRIM(@Address2)) + '%')
AND   (@City IS NULL OR @City = '' OR [Company].[City] LIKE LTRIM(RTRIM(@City)) + '%')
AND   (@District IS NULL OR @District = '' OR [Company].[District] LIKE LTRIM(RTRIM(@District)) + '%')
AND   (@State IS NULL OR @State = '' OR [Company].[State] LIKE LTRIM(RTRIM(@State)) + '%')
AND   (@Country IS NULL OR @Country = '' OR [Company].[Country] LIKE LTRIM(RTRIM(@Country)) + '%')
AND   (@PinCode IS NULL OR @PinCode = '' OR [Company].[PinCode] LIKE LTRIM(RTRIM(@PinCode)) + '%')
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Company].[ContactNo] LIKE LTRIM(RTRIM(@ContactNo)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [Company].[EMail] LIKE LTRIM(RTRIM(@EMail)) + '%')
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Company].[GSTIN] LIKE LTRIM(RTRIM(@GSTIN)) + '%')
AND   (@InvoiceInitials IS NULL OR @InvoiceInitials = '' OR [Company].[InvoiceInitials] LIKE LTRIM(RTRIM(@InvoiceInitials)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Company].[AddUserID] LIKE LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Company].[AddDate] LIKE LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Company].[ArchiveUserID] LIKE LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Company].[ArchiveDate] LIKE LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Company].[CompanyID]
     ,[Company].[CompanyName]
     ,[Company].[Address1]
     ,[Company].[Address2]
     ,[Company].[City]
     ,[Company].[District]
     ,[Company].[State]
     ,[Company].[Country]
     ,[Company].[PinCode]
     ,[Company].[ContactNo]
     ,[Company].[EMail]
     ,[Company].[GSTIN]
     ,[Company].[InvoiceInitials]
     ,[Company].[AddUserID]
     ,[Company].[AddDate]
     ,[Company].[ArchiveUserID]
     ,[Company].[ArchiveDate]
FROM
     [Company]
WHERE
      (@CompanyID IS NULL OR @CompanyID = '' OR [Company].[CompanyID] > LTRIM(RTRIM(@CompanyID)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] > LTRIM(RTRIM(@CompanyName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Company].[Address1] > LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Company].[Address2] > LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Company].[City] > LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Company].[District] > LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Company].[State] > LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Company].[Country] > LTRIM(RTRIM(@Country)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Company].[PinCode] > LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Company].[ContactNo] > LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Company].[EMail] > LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Company].[GSTIN] > LTRIM(RTRIM(@GSTIN)))
AND   (@InvoiceInitials IS NULL OR @InvoiceInitials = '' OR [Company].[InvoiceInitials] > LTRIM(RTRIM(@InvoiceInitials)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Company].[AddUserID] > LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Company].[AddDate] > LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Company].[ArchiveUserID] > LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Company].[ArchiveDate] > LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Company].[CompanyID]
     ,[Company].[CompanyName]
     ,[Company].[Address1]
     ,[Company].[Address2]
     ,[Company].[City]
     ,[Company].[District]
     ,[Company].[State]
     ,[Company].[Country]
     ,[Company].[PinCode]
     ,[Company].[ContactNo]
     ,[Company].[EMail]
     ,[Company].[GSTIN]
     ,[Company].[InvoiceInitials]
     ,[Company].[AddUserID]
     ,[Company].[AddDate]
     ,[Company].[ArchiveUserID]
     ,[Company].[ArchiveDate]
FROM
     [Company]
WHERE
      (@CompanyID IS NULL OR @CompanyID = '' OR [Company].[CompanyID] < LTRIM(RTRIM(@CompanyID)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] < LTRIM(RTRIM(@CompanyName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Company].[Address1] < LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Company].[Address2] < LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Company].[City] < LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Company].[District] < LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Company].[State] < LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Company].[Country] < LTRIM(RTRIM(@Country)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Company].[PinCode] < LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Company].[ContactNo] < LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Company].[EMail] < LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Company].[GSTIN] < LTRIM(RTRIM(@GSTIN)))
AND   (@InvoiceInitials IS NULL OR @InvoiceInitials = '' OR [Company].[InvoiceInitials] < LTRIM(RTRIM(@InvoiceInitials)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Company].[AddUserID] < LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Company].[AddDate] < LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Company].[ArchiveUserID] < LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Company].[ArchiveDate] < LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Company].[CompanyID]
     ,[Company].[CompanyName]
     ,[Company].[Address1]
     ,[Company].[Address2]
     ,[Company].[City]
     ,[Company].[District]
     ,[Company].[State]
     ,[Company].[Country]
     ,[Company].[PinCode]
     ,[Company].[ContactNo]
     ,[Company].[EMail]
     ,[Company].[GSTIN]
     ,[Company].[InvoiceInitials]
     ,[Company].[AddUserID]
     ,[Company].[AddDate]
     ,[Company].[ArchiveUserID]
     ,[Company].[ArchiveDate]
FROM
     [Company]
WHERE
      (@CompanyID IS NULL OR @CompanyID = '' OR [Company].[CompanyID] >= LTRIM(RTRIM(@CompanyID)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] >= LTRIM(RTRIM(@CompanyName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Company].[Address1] >= LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Company].[Address2] >= LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Company].[City] >= LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Company].[District] >= LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Company].[State] >= LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Company].[Country] >= LTRIM(RTRIM(@Country)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Company].[PinCode] >= LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Company].[ContactNo] >= LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Company].[EMail] >= LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Company].[GSTIN] >= LTRIM(RTRIM(@GSTIN)))
AND   (@InvoiceInitials IS NULL OR @InvoiceInitials = '' OR [Company].[InvoiceInitials] >= LTRIM(RTRIM(@InvoiceInitials)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Company].[AddUserID] >= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Company].[AddDate] >= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Company].[ArchiveUserID] >= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Company].[ArchiveDate] >= LTRIM(RTRIM(@ArchiveDate)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Company].[CompanyID]
     ,[Company].[CompanyName]
     ,[Company].[Address1]
     ,[Company].[Address2]
     ,[Company].[City]
     ,[Company].[District]
     ,[Company].[State]
     ,[Company].[Country]
     ,[Company].[PinCode]
     ,[Company].[ContactNo]
     ,[Company].[EMail]
     ,[Company].[GSTIN]
     ,[Company].[InvoiceInitials]
     ,[Company].[AddUserID]
     ,[Company].[AddDate]
     ,[Company].[ArchiveUserID]
     ,[Company].[ArchiveDate]
FROM
     [Company]
WHERE
      (@CompanyID IS NULL OR @CompanyID = '' OR [Company].[CompanyID] <= LTRIM(RTRIM(@CompanyID)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] <= LTRIM(RTRIM(@CompanyName)))
AND   (@Address1 IS NULL OR @Address1 = '' OR [Company].[Address1] <= LTRIM(RTRIM(@Address1)))
AND   (@Address2 IS NULL OR @Address2 = '' OR [Company].[Address2] <= LTRIM(RTRIM(@Address2)))
AND   (@City IS NULL OR @City = '' OR [Company].[City] <= LTRIM(RTRIM(@City)))
AND   (@District IS NULL OR @District = '' OR [Company].[District] <= LTRIM(RTRIM(@District)))
AND   (@State IS NULL OR @State = '' OR [Company].[State] <= LTRIM(RTRIM(@State)))
AND   (@Country IS NULL OR @Country = '' OR [Company].[Country] <= LTRIM(RTRIM(@Country)))
AND   (@PinCode IS NULL OR @PinCode = '' OR [Company].[PinCode] <= LTRIM(RTRIM(@PinCode)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [Company].[ContactNo] <= LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [Company].[EMail] <= LTRIM(RTRIM(@EMail)))
AND   (@GSTIN IS NULL OR @GSTIN = '' OR [Company].[GSTIN] <= LTRIM(RTRIM(@GSTIN)))
AND   (@InvoiceInitials IS NULL OR @InvoiceInitials = '' OR [Company].[InvoiceInitials] <= LTRIM(RTRIM(@InvoiceInitials)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Company].[AddUserID] <= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Company].[AddDate] <= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Company].[ArchiveUserID] <= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Company].[ArchiveDate] <= LTRIM(RTRIM(@ArchiveDate)))

END

END
GO

GRANT EXECUTE ON [CompanySearch] TO [Public]
GO

 
