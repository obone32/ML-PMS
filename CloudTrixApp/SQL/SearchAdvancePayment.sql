--***********************************************************
--SEARCH Stored Procedure for AdvancePayment table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'AdvancePaymentSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [AdvancePaymentSearch] 
GO

CREATE PROCEDURE [AdvancePaymentSearch] 
      @AdvancePaymentID int
     ,@PaymentDate datetime
     ,@CompanyName varchar(-1)
     ,@ClientName varchar(-1)
     ,@ProjectName varchar(-1)
     ,@GrossAmount decimal(18,2)
     ,@TDSRate decimal(18,2)
     ,@CGSTRate decimal(18,2)
     ,@SGSTRate decimal(18,2)
     ,@IGSTRate decimal(18,2)
     ,@Remarks varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [AdvancePayment].[AdvancePaymentID]
     ,[AdvancePayment].[PaymentDate]
     ,[AdvancePayment].[CompanyID]
     ,[AdvancePayment].[ClientID]
     ,[AdvancePayment].[ProjectID]
     ,[AdvancePayment].[GrossAmount]
     ,[AdvancePayment].[TDSRate]
     ,[AdvancePayment].[CGSTRate]
     ,[AdvancePayment].[SGSTRate]
     ,[AdvancePayment].[IGSTRate]
     ,[AdvancePayment].[Remarks]
FROM
     [AdvancePayment]
     INNER JOIN [Company] ON [AdvancePayment].[CompanyID] = [Company].[CompanyID]
     INNER JOIN [Client] ON [AdvancePayment].[ClientID] = [Client].[ClientID]
     INNER JOIN [Project] ON [AdvancePayment].[ProjectID] = [Project].[ProjectID]
WHERE
      (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] LIKE '%' + LTRIM(RTRIM(@AdvancePaymentID)) + '%')
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [AdvancePayment].[PaymentDate] LIKE '%' + LTRIM(RTRIM(@PaymentDate)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE '%' + LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] LIKE '%' + LTRIM(RTRIM(@ClientName)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE '%' + LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@GrossAmount IS NULL OR @GrossAmount = '' OR [AdvancePayment].[GrossAmount] LIKE '%' + LTRIM(RTRIM(@GrossAmount)) + '%')
AND   (@TDSRate IS NULL OR @TDSRate = '' OR [AdvancePayment].[TDSRate] LIKE '%' + LTRIM(RTRIM(@TDSRate)) + '%')
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [AdvancePayment].[CGSTRate] LIKE '%' + LTRIM(RTRIM(@CGSTRate)) + '%')
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [AdvancePayment].[SGSTRate] LIKE '%' + LTRIM(RTRIM(@SGSTRate)) + '%')
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [AdvancePayment].[IGSTRate] LIKE '%' + LTRIM(RTRIM(@IGSTRate)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [AdvancePayment].[Remarks] LIKE '%' + LTRIM(RTRIM(@Remarks)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [AdvancePayment].[AdvancePaymentID]
     ,[AdvancePayment].[PaymentDate]
     ,[AdvancePayment].[CompanyID]
     ,[AdvancePayment].[ClientID]
     ,[AdvancePayment].[ProjectID]
     ,[AdvancePayment].[GrossAmount]
     ,[AdvancePayment].[TDSRate]
     ,[AdvancePayment].[CGSTRate]
     ,[AdvancePayment].[SGSTRate]
     ,[AdvancePayment].[IGSTRate]
     ,[AdvancePayment].[Remarks]
FROM
     [AdvancePayment]
     INNER JOIN [Company] ON [AdvancePayment].[CompanyID] = [Company].[CompanyID]
     INNER JOIN [Client] ON [AdvancePayment].[ClientID] = [Client].[ClientID]
     INNER JOIN [Project] ON [AdvancePayment].[ProjectID] = [Project].[ProjectID]
WHERE
      (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] = LTRIM(RTRIM(@AdvancePaymentID)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [AdvancePayment].[PaymentDate] = LTRIM(RTRIM(@PaymentDate)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] = LTRIM(RTRIM(@CompanyName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] = LTRIM(RTRIM(@ClientName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] = LTRIM(RTRIM(@ProjectName)))
AND   (@GrossAmount IS NULL OR @GrossAmount = '' OR [AdvancePayment].[GrossAmount] = LTRIM(RTRIM(@GrossAmount)))
AND   (@TDSRate IS NULL OR @TDSRate = '' OR [AdvancePayment].[TDSRate] = LTRIM(RTRIM(@TDSRate)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [AdvancePayment].[CGSTRate] = LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [AdvancePayment].[SGSTRate] = LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [AdvancePayment].[IGSTRate] = LTRIM(RTRIM(@IGSTRate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [AdvancePayment].[Remarks] = LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [AdvancePayment].[AdvancePaymentID]
     ,[AdvancePayment].[PaymentDate]
     ,[AdvancePayment].[CompanyID]
     ,[AdvancePayment].[ClientID]
     ,[AdvancePayment].[ProjectID]
     ,[AdvancePayment].[GrossAmount]
     ,[AdvancePayment].[TDSRate]
     ,[AdvancePayment].[CGSTRate]
     ,[AdvancePayment].[SGSTRate]
     ,[AdvancePayment].[IGSTRate]
     ,[AdvancePayment].[Remarks]
FROM
     [AdvancePayment]
     INNER JOIN [Company] ON [AdvancePayment].[CompanyID] = [Company].[CompanyID]
     INNER JOIN [Client] ON [AdvancePayment].[ClientID] = [Client].[ClientID]
     INNER JOIN [Project] ON [AdvancePayment].[ProjectID] = [Project].[ProjectID]
WHERE
      (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] LIKE LTRIM(RTRIM(@AdvancePaymentID)) + '%')
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [AdvancePayment].[PaymentDate] LIKE LTRIM(RTRIM(@PaymentDate)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] LIKE LTRIM(RTRIM(@ClientName)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@GrossAmount IS NULL OR @GrossAmount = '' OR [AdvancePayment].[GrossAmount] LIKE LTRIM(RTRIM(@GrossAmount)) + '%')
AND   (@TDSRate IS NULL OR @TDSRate = '' OR [AdvancePayment].[TDSRate] LIKE LTRIM(RTRIM(@TDSRate)) + '%')
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [AdvancePayment].[CGSTRate] LIKE LTRIM(RTRIM(@CGSTRate)) + '%')
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [AdvancePayment].[SGSTRate] LIKE LTRIM(RTRIM(@SGSTRate)) + '%')
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [AdvancePayment].[IGSTRate] LIKE LTRIM(RTRIM(@IGSTRate)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [AdvancePayment].[Remarks] LIKE LTRIM(RTRIM(@Remarks)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [AdvancePayment].[AdvancePaymentID]
     ,[AdvancePayment].[PaymentDate]
     ,[AdvancePayment].[CompanyID]
     ,[AdvancePayment].[ClientID]
     ,[AdvancePayment].[ProjectID]
     ,[AdvancePayment].[GrossAmount]
     ,[AdvancePayment].[TDSRate]
     ,[AdvancePayment].[CGSTRate]
     ,[AdvancePayment].[SGSTRate]
     ,[AdvancePayment].[IGSTRate]
     ,[AdvancePayment].[Remarks]
FROM
     [AdvancePayment]
     INNER JOIN [Company] ON [AdvancePayment].[CompanyID] = [Company].[CompanyID]
     INNER JOIN [Client] ON [AdvancePayment].[ClientID] = [Client].[ClientID]
     INNER JOIN [Project] ON [AdvancePayment].[ProjectID] = [Project].[ProjectID]
WHERE
      (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] > LTRIM(RTRIM(@AdvancePaymentID)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [AdvancePayment].[PaymentDate] > LTRIM(RTRIM(@PaymentDate)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] > LTRIM(RTRIM(@CompanyName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] > LTRIM(RTRIM(@ClientName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] > LTRIM(RTRIM(@ProjectName)))
AND   (@GrossAmount IS NULL OR @GrossAmount = '' OR [AdvancePayment].[GrossAmount] > LTRIM(RTRIM(@GrossAmount)))
AND   (@TDSRate IS NULL OR @TDSRate = '' OR [AdvancePayment].[TDSRate] > LTRIM(RTRIM(@TDSRate)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [AdvancePayment].[CGSTRate] > LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [AdvancePayment].[SGSTRate] > LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [AdvancePayment].[IGSTRate] > LTRIM(RTRIM(@IGSTRate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [AdvancePayment].[Remarks] > LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [AdvancePayment].[AdvancePaymentID]
     ,[AdvancePayment].[PaymentDate]
     ,[AdvancePayment].[CompanyID]
     ,[AdvancePayment].[ClientID]
     ,[AdvancePayment].[ProjectID]
     ,[AdvancePayment].[GrossAmount]
     ,[AdvancePayment].[TDSRate]
     ,[AdvancePayment].[CGSTRate]
     ,[AdvancePayment].[SGSTRate]
     ,[AdvancePayment].[IGSTRate]
     ,[AdvancePayment].[Remarks]
FROM
     [AdvancePayment]
     INNER JOIN [Company] ON [AdvancePayment].[CompanyID] = [Company].[CompanyID]
     INNER JOIN [Client] ON [AdvancePayment].[ClientID] = [Client].[ClientID]
     INNER JOIN [Project] ON [AdvancePayment].[ProjectID] = [Project].[ProjectID]
WHERE
      (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] < LTRIM(RTRIM(@AdvancePaymentID)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [AdvancePayment].[PaymentDate] < LTRIM(RTRIM(@PaymentDate)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] < LTRIM(RTRIM(@CompanyName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] < LTRIM(RTRIM(@ClientName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] < LTRIM(RTRIM(@ProjectName)))
AND   (@GrossAmount IS NULL OR @GrossAmount = '' OR [AdvancePayment].[GrossAmount] < LTRIM(RTRIM(@GrossAmount)))
AND   (@TDSRate IS NULL OR @TDSRate = '' OR [AdvancePayment].[TDSRate] < LTRIM(RTRIM(@TDSRate)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [AdvancePayment].[CGSTRate] < LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [AdvancePayment].[SGSTRate] < LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [AdvancePayment].[IGSTRate] < LTRIM(RTRIM(@IGSTRate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [AdvancePayment].[Remarks] < LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [AdvancePayment].[AdvancePaymentID]
     ,[AdvancePayment].[PaymentDate]
     ,[AdvancePayment].[CompanyID]
     ,[AdvancePayment].[ClientID]
     ,[AdvancePayment].[ProjectID]
     ,[AdvancePayment].[GrossAmount]
     ,[AdvancePayment].[TDSRate]
     ,[AdvancePayment].[CGSTRate]
     ,[AdvancePayment].[SGSTRate]
     ,[AdvancePayment].[IGSTRate]
     ,[AdvancePayment].[Remarks]
FROM
     [AdvancePayment]
     INNER JOIN [Company] ON [AdvancePayment].[CompanyID] = [Company].[CompanyID]
     INNER JOIN [Client] ON [AdvancePayment].[ClientID] = [Client].[ClientID]
     INNER JOIN [Project] ON [AdvancePayment].[ProjectID] = [Project].[ProjectID]
WHERE
      (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] >= LTRIM(RTRIM(@AdvancePaymentID)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [AdvancePayment].[PaymentDate] >= LTRIM(RTRIM(@PaymentDate)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] >= LTRIM(RTRIM(@CompanyName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] >= LTRIM(RTRIM(@ClientName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] >= LTRIM(RTRIM(@ProjectName)))
AND   (@GrossAmount IS NULL OR @GrossAmount = '' OR [AdvancePayment].[GrossAmount] >= LTRIM(RTRIM(@GrossAmount)))
AND   (@TDSRate IS NULL OR @TDSRate = '' OR [AdvancePayment].[TDSRate] >= LTRIM(RTRIM(@TDSRate)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [AdvancePayment].[CGSTRate] >= LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [AdvancePayment].[SGSTRate] >= LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [AdvancePayment].[IGSTRate] >= LTRIM(RTRIM(@IGSTRate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [AdvancePayment].[Remarks] >= LTRIM(RTRIM(@Remarks)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [AdvancePayment].[AdvancePaymentID]
     ,[AdvancePayment].[PaymentDate]
     ,[AdvancePayment].[CompanyID]
     ,[AdvancePayment].[ClientID]
     ,[AdvancePayment].[ProjectID]
     ,[AdvancePayment].[GrossAmount]
     ,[AdvancePayment].[TDSRate]
     ,[AdvancePayment].[CGSTRate]
     ,[AdvancePayment].[SGSTRate]
     ,[AdvancePayment].[IGSTRate]
     ,[AdvancePayment].[Remarks]
FROM
     [AdvancePayment]
     INNER JOIN [Company] ON [AdvancePayment].[CompanyID] = [Company].[CompanyID]
     INNER JOIN [Client] ON [AdvancePayment].[ClientID] = [Client].[ClientID]
     INNER JOIN [Project] ON [AdvancePayment].[ProjectID] = [Project].[ProjectID]
WHERE
      (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] <= LTRIM(RTRIM(@AdvancePaymentID)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [AdvancePayment].[PaymentDate] <= LTRIM(RTRIM(@PaymentDate)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] <= LTRIM(RTRIM(@CompanyName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] <= LTRIM(RTRIM(@ClientName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] <= LTRIM(RTRIM(@ProjectName)))
AND   (@GrossAmount IS NULL OR @GrossAmount = '' OR [AdvancePayment].[GrossAmount] <= LTRIM(RTRIM(@GrossAmount)))
AND   (@TDSRate IS NULL OR @TDSRate = '' OR [AdvancePayment].[TDSRate] <= LTRIM(RTRIM(@TDSRate)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [AdvancePayment].[CGSTRate] <= LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [AdvancePayment].[SGSTRate] <= LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [AdvancePayment].[IGSTRate] <= LTRIM(RTRIM(@IGSTRate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [AdvancePayment].[Remarks] <= LTRIM(RTRIM(@Remarks)))

END

END
GO

GRANT EXECUTE ON [AdvancePaymentSearch] TO [Public]
GO

 
