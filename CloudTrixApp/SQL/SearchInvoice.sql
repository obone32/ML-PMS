--***********************************************************
--SEARCH Stored Procedure for Invoice table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceSearch] 
GO

CREATE PROCEDURE [InvoiceSearch] 
      @InvoiceID int
     ,@InvoiceNo varchar(8000)
     ,@InvoiceDate datetime
     ,@ProjectName varchar(-1)
     ,@ClientName varchar(-1)
     ,@ClientName varchar(8000)
     ,@ClientAddress varchar(8000)
     ,@ClientGSTIN varchar(8000)
     ,@ClientContactNo varchar(8000)
     ,@ClientEMail varchar(8000)
     ,@AdditionalDiscount decimal(18,2)
     ,@Remarks varchar(8000)
     ,@PDFUrl varchar(8000)
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
      [Invoice].[InvoiceID]
     ,[Invoice].[InvoiceNo]
     ,[Invoice].[InvoiceDate]
     ,[Invoice].[ProjectID]
     ,[Invoice].[ClientID]
     ,[Invoice].[ClientName]
     ,[Invoice].[ClientAddress]
     ,[Invoice].[ClientGSTIN]
     ,[Invoice].[ClientContactNo]
     ,[Invoice].[ClientEMail]
     ,[Invoice].[AdditionalDiscount]
     ,[Invoice].[Remarks]
     ,[Invoice].[PDFUrl]
     ,[Invoice].[CompanyID]
     ,[Invoice].[AddUserID]
     ,[Invoice].[AddDate]
     ,[Invoice].[ArchiveUserID]
     ,[Invoice].[ArchiveDate]
FROM
     [Invoice]
     INNER JOIN [Project] ON [Invoice].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Client] ON [Invoice].[ClientID] = [Client].[ClientID]
     INNER JOIN [Company] ON [Invoice].[CompanyID] = [Company].[CompanyID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] LIKE '%' + LTRIM(RTRIM(@InvoiceID)) + '%')
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] LIKE '%' + LTRIM(RTRIM(@InvoiceNo)) + '%')
AND   (@InvoiceDate IS NULL OR @InvoiceDate = '' OR [Invoice].[InvoiceDate] LIKE '%' + LTRIM(RTRIM(@InvoiceDate)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE '%' + LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] LIKE '%' + LTRIM(RTRIM(@ClientName)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Invoice].[ClientName] LIKE '%' + LTRIM(RTRIM(@ClientName)) + '%')
AND   (@ClientAddress IS NULL OR @ClientAddress = '' OR [Invoice].[ClientAddress] LIKE '%' + LTRIM(RTRIM(@ClientAddress)) + '%')
AND   (@ClientGSTIN IS NULL OR @ClientGSTIN = '' OR [Invoice].[ClientGSTIN] LIKE '%' + LTRIM(RTRIM(@ClientGSTIN)) + '%')
AND   (@ClientContactNo IS NULL OR @ClientContactNo = '' OR [Invoice].[ClientContactNo] LIKE '%' + LTRIM(RTRIM(@ClientContactNo)) + '%')
AND   (@ClientEMail IS NULL OR @ClientEMail = '' OR [Invoice].[ClientEMail] LIKE '%' + LTRIM(RTRIM(@ClientEMail)) + '%')
AND   (@AdditionalDiscount IS NULL OR @AdditionalDiscount = '' OR [Invoice].[AdditionalDiscount] LIKE '%' + LTRIM(RTRIM(@AdditionalDiscount)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [Invoice].[Remarks] LIKE '%' + LTRIM(RTRIM(@Remarks)) + '%')
AND   (@PDFUrl IS NULL OR @PDFUrl = '' OR [Invoice].[PDFUrl] LIKE '%' + LTRIM(RTRIM(@PDFUrl)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE '%' + LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Invoice].[AddUserID] LIKE '%' + LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Invoice].[AddDate] LIKE '%' + LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Invoice].[ArchiveUserID] LIKE '%' + LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Invoice].[ArchiveDate] LIKE '%' + LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Invoice].[InvoiceID]
     ,[Invoice].[InvoiceNo]
     ,[Invoice].[InvoiceDate]
     ,[Invoice].[ProjectID]
     ,[Invoice].[ClientID]
     ,[Invoice].[ClientName]
     ,[Invoice].[ClientAddress]
     ,[Invoice].[ClientGSTIN]
     ,[Invoice].[ClientContactNo]
     ,[Invoice].[ClientEMail]
     ,[Invoice].[AdditionalDiscount]
     ,[Invoice].[Remarks]
     ,[Invoice].[PDFUrl]
     ,[Invoice].[CompanyID]
     ,[Invoice].[AddUserID]
     ,[Invoice].[AddDate]
     ,[Invoice].[ArchiveUserID]
     ,[Invoice].[ArchiveDate]
FROM
     [Invoice]
     INNER JOIN [Project] ON [Invoice].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Client] ON [Invoice].[ClientID] = [Client].[ClientID]
     INNER JOIN [Company] ON [Invoice].[CompanyID] = [Company].[CompanyID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] = LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] = LTRIM(RTRIM(@InvoiceNo)))
AND   (@InvoiceDate IS NULL OR @InvoiceDate = '' OR [Invoice].[InvoiceDate] = LTRIM(RTRIM(@InvoiceDate)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] = LTRIM(RTRIM(@ProjectName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] = LTRIM(RTRIM(@ClientName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Invoice].[ClientName] = LTRIM(RTRIM(@ClientName)))
AND   (@ClientAddress IS NULL OR @ClientAddress = '' OR [Invoice].[ClientAddress] = LTRIM(RTRIM(@ClientAddress)))
AND   (@ClientGSTIN IS NULL OR @ClientGSTIN = '' OR [Invoice].[ClientGSTIN] = LTRIM(RTRIM(@ClientGSTIN)))
AND   (@ClientContactNo IS NULL OR @ClientContactNo = '' OR [Invoice].[ClientContactNo] = LTRIM(RTRIM(@ClientContactNo)))
AND   (@ClientEMail IS NULL OR @ClientEMail = '' OR [Invoice].[ClientEMail] = LTRIM(RTRIM(@ClientEMail)))
AND   (@AdditionalDiscount IS NULL OR @AdditionalDiscount = '' OR [Invoice].[AdditionalDiscount] = LTRIM(RTRIM(@AdditionalDiscount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Invoice].[Remarks] = LTRIM(RTRIM(@Remarks)))
AND   (@PDFUrl IS NULL OR @PDFUrl = '' OR [Invoice].[PDFUrl] = LTRIM(RTRIM(@PDFUrl)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] = LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Invoice].[AddUserID] = LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Invoice].[AddDate] = LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Invoice].[ArchiveUserID] = LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Invoice].[ArchiveDate] = LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Invoice].[InvoiceID]
     ,[Invoice].[InvoiceNo]
     ,[Invoice].[InvoiceDate]
     ,[Invoice].[ProjectID]
     ,[Invoice].[ClientID]
     ,[Invoice].[ClientName]
     ,[Invoice].[ClientAddress]
     ,[Invoice].[ClientGSTIN]
     ,[Invoice].[ClientContactNo]
     ,[Invoice].[ClientEMail]
     ,[Invoice].[AdditionalDiscount]
     ,[Invoice].[Remarks]
     ,[Invoice].[PDFUrl]
     ,[Invoice].[CompanyID]
     ,[Invoice].[AddUserID]
     ,[Invoice].[AddDate]
     ,[Invoice].[ArchiveUserID]
     ,[Invoice].[ArchiveDate]
FROM
     [Invoice]
     INNER JOIN [Project] ON [Invoice].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Client] ON [Invoice].[ClientID] = [Client].[ClientID]
     INNER JOIN [Company] ON [Invoice].[CompanyID] = [Company].[CompanyID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] LIKE LTRIM(RTRIM(@InvoiceID)) + '%')
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] LIKE LTRIM(RTRIM(@InvoiceNo)) + '%')
AND   (@InvoiceDate IS NULL OR @InvoiceDate = '' OR [Invoice].[InvoiceDate] LIKE LTRIM(RTRIM(@InvoiceDate)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] LIKE LTRIM(RTRIM(@ClientName)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Invoice].[ClientName] LIKE LTRIM(RTRIM(@ClientName)) + '%')
AND   (@ClientAddress IS NULL OR @ClientAddress = '' OR [Invoice].[ClientAddress] LIKE LTRIM(RTRIM(@ClientAddress)) + '%')
AND   (@ClientGSTIN IS NULL OR @ClientGSTIN = '' OR [Invoice].[ClientGSTIN] LIKE LTRIM(RTRIM(@ClientGSTIN)) + '%')
AND   (@ClientContactNo IS NULL OR @ClientContactNo = '' OR [Invoice].[ClientContactNo] LIKE LTRIM(RTRIM(@ClientContactNo)) + '%')
AND   (@ClientEMail IS NULL OR @ClientEMail = '' OR [Invoice].[ClientEMail] LIKE LTRIM(RTRIM(@ClientEMail)) + '%')
AND   (@AdditionalDiscount IS NULL OR @AdditionalDiscount = '' OR [Invoice].[AdditionalDiscount] LIKE LTRIM(RTRIM(@AdditionalDiscount)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [Invoice].[Remarks] LIKE LTRIM(RTRIM(@Remarks)) + '%')
AND   (@PDFUrl IS NULL OR @PDFUrl = '' OR [Invoice].[PDFUrl] LIKE LTRIM(RTRIM(@PDFUrl)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Invoice].[AddUserID] LIKE LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Invoice].[AddDate] LIKE LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Invoice].[ArchiveUserID] LIKE LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Invoice].[ArchiveDate] LIKE LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Invoice].[InvoiceID]
     ,[Invoice].[InvoiceNo]
     ,[Invoice].[InvoiceDate]
     ,[Invoice].[ProjectID]
     ,[Invoice].[ClientID]
     ,[Invoice].[ClientName]
     ,[Invoice].[ClientAddress]
     ,[Invoice].[ClientGSTIN]
     ,[Invoice].[ClientContactNo]
     ,[Invoice].[ClientEMail]
     ,[Invoice].[AdditionalDiscount]
     ,[Invoice].[Remarks]
     ,[Invoice].[PDFUrl]
     ,[Invoice].[CompanyID]
     ,[Invoice].[AddUserID]
     ,[Invoice].[AddDate]
     ,[Invoice].[ArchiveUserID]
     ,[Invoice].[ArchiveDate]
FROM
     [Invoice]
     INNER JOIN [Project] ON [Invoice].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Client] ON [Invoice].[ClientID] = [Client].[ClientID]
     INNER JOIN [Company] ON [Invoice].[CompanyID] = [Company].[CompanyID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] > LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] > LTRIM(RTRIM(@InvoiceNo)))
AND   (@InvoiceDate IS NULL OR @InvoiceDate = '' OR [Invoice].[InvoiceDate] > LTRIM(RTRIM(@InvoiceDate)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] > LTRIM(RTRIM(@ProjectName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] > LTRIM(RTRIM(@ClientName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Invoice].[ClientName] > LTRIM(RTRIM(@ClientName)))
AND   (@ClientAddress IS NULL OR @ClientAddress = '' OR [Invoice].[ClientAddress] > LTRIM(RTRIM(@ClientAddress)))
AND   (@ClientGSTIN IS NULL OR @ClientGSTIN = '' OR [Invoice].[ClientGSTIN] > LTRIM(RTRIM(@ClientGSTIN)))
AND   (@ClientContactNo IS NULL OR @ClientContactNo = '' OR [Invoice].[ClientContactNo] > LTRIM(RTRIM(@ClientContactNo)))
AND   (@ClientEMail IS NULL OR @ClientEMail = '' OR [Invoice].[ClientEMail] > LTRIM(RTRIM(@ClientEMail)))
AND   (@AdditionalDiscount IS NULL OR @AdditionalDiscount = '' OR [Invoice].[AdditionalDiscount] > LTRIM(RTRIM(@AdditionalDiscount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Invoice].[Remarks] > LTRIM(RTRIM(@Remarks)))
AND   (@PDFUrl IS NULL OR @PDFUrl = '' OR [Invoice].[PDFUrl] > LTRIM(RTRIM(@PDFUrl)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] > LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Invoice].[AddUserID] > LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Invoice].[AddDate] > LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Invoice].[ArchiveUserID] > LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Invoice].[ArchiveDate] > LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Invoice].[InvoiceID]
     ,[Invoice].[InvoiceNo]
     ,[Invoice].[InvoiceDate]
     ,[Invoice].[ProjectID]
     ,[Invoice].[ClientID]
     ,[Invoice].[ClientName]
     ,[Invoice].[ClientAddress]
     ,[Invoice].[ClientGSTIN]
     ,[Invoice].[ClientContactNo]
     ,[Invoice].[ClientEMail]
     ,[Invoice].[AdditionalDiscount]
     ,[Invoice].[Remarks]
     ,[Invoice].[PDFUrl]
     ,[Invoice].[CompanyID]
     ,[Invoice].[AddUserID]
     ,[Invoice].[AddDate]
     ,[Invoice].[ArchiveUserID]
     ,[Invoice].[ArchiveDate]
FROM
     [Invoice]
     INNER JOIN [Project] ON [Invoice].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Client] ON [Invoice].[ClientID] = [Client].[ClientID]
     INNER JOIN [Company] ON [Invoice].[CompanyID] = [Company].[CompanyID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] < LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] < LTRIM(RTRIM(@InvoiceNo)))
AND   (@InvoiceDate IS NULL OR @InvoiceDate = '' OR [Invoice].[InvoiceDate] < LTRIM(RTRIM(@InvoiceDate)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] < LTRIM(RTRIM(@ProjectName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] < LTRIM(RTRIM(@ClientName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Invoice].[ClientName] < LTRIM(RTRIM(@ClientName)))
AND   (@ClientAddress IS NULL OR @ClientAddress = '' OR [Invoice].[ClientAddress] < LTRIM(RTRIM(@ClientAddress)))
AND   (@ClientGSTIN IS NULL OR @ClientGSTIN = '' OR [Invoice].[ClientGSTIN] < LTRIM(RTRIM(@ClientGSTIN)))
AND   (@ClientContactNo IS NULL OR @ClientContactNo = '' OR [Invoice].[ClientContactNo] < LTRIM(RTRIM(@ClientContactNo)))
AND   (@ClientEMail IS NULL OR @ClientEMail = '' OR [Invoice].[ClientEMail] < LTRIM(RTRIM(@ClientEMail)))
AND   (@AdditionalDiscount IS NULL OR @AdditionalDiscount = '' OR [Invoice].[AdditionalDiscount] < LTRIM(RTRIM(@AdditionalDiscount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Invoice].[Remarks] < LTRIM(RTRIM(@Remarks)))
AND   (@PDFUrl IS NULL OR @PDFUrl = '' OR [Invoice].[PDFUrl] < LTRIM(RTRIM(@PDFUrl)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] < LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Invoice].[AddUserID] < LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Invoice].[AddDate] < LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Invoice].[ArchiveUserID] < LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Invoice].[ArchiveDate] < LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Invoice].[InvoiceID]
     ,[Invoice].[InvoiceNo]
     ,[Invoice].[InvoiceDate]
     ,[Invoice].[ProjectID]
     ,[Invoice].[ClientID]
     ,[Invoice].[ClientName]
     ,[Invoice].[ClientAddress]
     ,[Invoice].[ClientGSTIN]
     ,[Invoice].[ClientContactNo]
     ,[Invoice].[ClientEMail]
     ,[Invoice].[AdditionalDiscount]
     ,[Invoice].[Remarks]
     ,[Invoice].[PDFUrl]
     ,[Invoice].[CompanyID]
     ,[Invoice].[AddUserID]
     ,[Invoice].[AddDate]
     ,[Invoice].[ArchiveUserID]
     ,[Invoice].[ArchiveDate]
FROM
     [Invoice]
     INNER JOIN [Project] ON [Invoice].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Client] ON [Invoice].[ClientID] = [Client].[ClientID]
     INNER JOIN [Company] ON [Invoice].[CompanyID] = [Company].[CompanyID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] >= LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] >= LTRIM(RTRIM(@InvoiceNo)))
AND   (@InvoiceDate IS NULL OR @InvoiceDate = '' OR [Invoice].[InvoiceDate] >= LTRIM(RTRIM(@InvoiceDate)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] >= LTRIM(RTRIM(@ProjectName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] >= LTRIM(RTRIM(@ClientName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Invoice].[ClientName] >= LTRIM(RTRIM(@ClientName)))
AND   (@ClientAddress IS NULL OR @ClientAddress = '' OR [Invoice].[ClientAddress] >= LTRIM(RTRIM(@ClientAddress)))
AND   (@ClientGSTIN IS NULL OR @ClientGSTIN = '' OR [Invoice].[ClientGSTIN] >= LTRIM(RTRIM(@ClientGSTIN)))
AND   (@ClientContactNo IS NULL OR @ClientContactNo = '' OR [Invoice].[ClientContactNo] >= LTRIM(RTRIM(@ClientContactNo)))
AND   (@ClientEMail IS NULL OR @ClientEMail = '' OR [Invoice].[ClientEMail] >= LTRIM(RTRIM(@ClientEMail)))
AND   (@AdditionalDiscount IS NULL OR @AdditionalDiscount = '' OR [Invoice].[AdditionalDiscount] >= LTRIM(RTRIM(@AdditionalDiscount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Invoice].[Remarks] >= LTRIM(RTRIM(@Remarks)))
AND   (@PDFUrl IS NULL OR @PDFUrl = '' OR [Invoice].[PDFUrl] >= LTRIM(RTRIM(@PDFUrl)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] >= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Invoice].[AddUserID] >= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Invoice].[AddDate] >= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Invoice].[ArchiveUserID] >= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Invoice].[ArchiveDate] >= LTRIM(RTRIM(@ArchiveDate)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Invoice].[InvoiceID]
     ,[Invoice].[InvoiceNo]
     ,[Invoice].[InvoiceDate]
     ,[Invoice].[ProjectID]
     ,[Invoice].[ClientID]
     ,[Invoice].[ClientName]
     ,[Invoice].[ClientAddress]
     ,[Invoice].[ClientGSTIN]
     ,[Invoice].[ClientContactNo]
     ,[Invoice].[ClientEMail]
     ,[Invoice].[AdditionalDiscount]
     ,[Invoice].[Remarks]
     ,[Invoice].[PDFUrl]
     ,[Invoice].[CompanyID]
     ,[Invoice].[AddUserID]
     ,[Invoice].[AddDate]
     ,[Invoice].[ArchiveUserID]
     ,[Invoice].[ArchiveDate]
FROM
     [Invoice]
     INNER JOIN [Project] ON [Invoice].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Client] ON [Invoice].[ClientID] = [Client].[ClientID]
     INNER JOIN [Company] ON [Invoice].[CompanyID] = [Company].[CompanyID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] <= LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] <= LTRIM(RTRIM(@InvoiceNo)))
AND   (@InvoiceDate IS NULL OR @InvoiceDate = '' OR [Invoice].[InvoiceDate] <= LTRIM(RTRIM(@InvoiceDate)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] <= LTRIM(RTRIM(@ProjectName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] <= LTRIM(RTRIM(@ClientName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Invoice].[ClientName] <= LTRIM(RTRIM(@ClientName)))
AND   (@ClientAddress IS NULL OR @ClientAddress = '' OR [Invoice].[ClientAddress] <= LTRIM(RTRIM(@ClientAddress)))
AND   (@ClientGSTIN IS NULL OR @ClientGSTIN = '' OR [Invoice].[ClientGSTIN] <= LTRIM(RTRIM(@ClientGSTIN)))
AND   (@ClientContactNo IS NULL OR @ClientContactNo = '' OR [Invoice].[ClientContactNo] <= LTRIM(RTRIM(@ClientContactNo)))
AND   (@ClientEMail IS NULL OR @ClientEMail = '' OR [Invoice].[ClientEMail] <= LTRIM(RTRIM(@ClientEMail)))
AND   (@AdditionalDiscount IS NULL OR @AdditionalDiscount = '' OR [Invoice].[AdditionalDiscount] <= LTRIM(RTRIM(@AdditionalDiscount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Invoice].[Remarks] <= LTRIM(RTRIM(@Remarks)))
AND   (@PDFUrl IS NULL OR @PDFUrl = '' OR [Invoice].[PDFUrl] <= LTRIM(RTRIM(@PDFUrl)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] <= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Invoice].[AddUserID] <= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Invoice].[AddDate] <= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Invoice].[ArchiveUserID] <= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Invoice].[ArchiveDate] <= LTRIM(RTRIM(@ArchiveDate)))

END

END
GO

GRANT EXECUTE ON [InvoiceSearch] TO [Public]
GO

 
