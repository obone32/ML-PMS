--***********************************************************
--SEARCH Stored Procedure for Reconciliation table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ReconciliationSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [ReconciliationSearch] 
GO

CREATE PROCEDURE [ReconciliationSearch] 
      @ReconciliationID int
     ,@InvoiceNo varchar(-1)
     ,@PaymentDate datetime
     ,@PaymentAmount decimal(18,2)
     ,@TDSAmount decimal(18,2)
     ,@Remarks varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [Reconciliation].[ReconciliationID]
     ,[Reconciliation].[InvoiceID]
     ,[Reconciliation].[PaymentDate]
     ,[Reconciliation].[PaymentAmount]
     ,[Reconciliation].[TDSAmount]
     ,[Reconciliation].[Remarks]
FROM
     [Reconciliation]
     INNER JOIN [Invoice] ON [Reconciliation].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@ReconciliationID IS NULL OR @ReconciliationID = '' OR [Reconciliation].[ReconciliationID] LIKE '%' + LTRIM(RTRIM(@ReconciliationID)) + '%')
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] LIKE '%' + LTRIM(RTRIM(@InvoiceNo)) + '%')
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [Reconciliation].[PaymentDate] LIKE '%' + LTRIM(RTRIM(@PaymentDate)) + '%')
AND   (@PaymentAmount IS NULL OR @PaymentAmount = '' OR [Reconciliation].[PaymentAmount] LIKE '%' + LTRIM(RTRIM(@PaymentAmount)) + '%')
AND   (@TDSAmount IS NULL OR @TDSAmount = '' OR [Reconciliation].[TDSAmount] LIKE '%' + LTRIM(RTRIM(@TDSAmount)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [Reconciliation].[Remarks] LIKE '%' + LTRIM(RTRIM(@Remarks)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Reconciliation].[ReconciliationID]
     ,[Reconciliation].[InvoiceID]
     ,[Reconciliation].[PaymentDate]
     ,[Reconciliation].[PaymentAmount]
     ,[Reconciliation].[TDSAmount]
     ,[Reconciliation].[Remarks]
FROM
     [Reconciliation]
     INNER JOIN [Invoice] ON [Reconciliation].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@ReconciliationID IS NULL OR @ReconciliationID = '' OR [Reconciliation].[ReconciliationID] = LTRIM(RTRIM(@ReconciliationID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] = LTRIM(RTRIM(@InvoiceNo)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [Reconciliation].[PaymentDate] = LTRIM(RTRIM(@PaymentDate)))
AND   (@PaymentAmount IS NULL OR @PaymentAmount = '' OR [Reconciliation].[PaymentAmount] = LTRIM(RTRIM(@PaymentAmount)))
AND   (@TDSAmount IS NULL OR @TDSAmount = '' OR [Reconciliation].[TDSAmount] = LTRIM(RTRIM(@TDSAmount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Reconciliation].[Remarks] = LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Reconciliation].[ReconciliationID]
     ,[Reconciliation].[InvoiceID]
     ,[Reconciliation].[PaymentDate]
     ,[Reconciliation].[PaymentAmount]
     ,[Reconciliation].[TDSAmount]
     ,[Reconciliation].[Remarks]
FROM
     [Reconciliation]
     INNER JOIN [Invoice] ON [Reconciliation].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@ReconciliationID IS NULL OR @ReconciliationID = '' OR [Reconciliation].[ReconciliationID] LIKE LTRIM(RTRIM(@ReconciliationID)) + '%')
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] LIKE LTRIM(RTRIM(@InvoiceNo)) + '%')
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [Reconciliation].[PaymentDate] LIKE LTRIM(RTRIM(@PaymentDate)) + '%')
AND   (@PaymentAmount IS NULL OR @PaymentAmount = '' OR [Reconciliation].[PaymentAmount] LIKE LTRIM(RTRIM(@PaymentAmount)) + '%')
AND   (@TDSAmount IS NULL OR @TDSAmount = '' OR [Reconciliation].[TDSAmount] LIKE LTRIM(RTRIM(@TDSAmount)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [Reconciliation].[Remarks] LIKE LTRIM(RTRIM(@Remarks)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Reconciliation].[ReconciliationID]
     ,[Reconciliation].[InvoiceID]
     ,[Reconciliation].[PaymentDate]
     ,[Reconciliation].[PaymentAmount]
     ,[Reconciliation].[TDSAmount]
     ,[Reconciliation].[Remarks]
FROM
     [Reconciliation]
     INNER JOIN [Invoice] ON [Reconciliation].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@ReconciliationID IS NULL OR @ReconciliationID = '' OR [Reconciliation].[ReconciliationID] > LTRIM(RTRIM(@ReconciliationID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] > LTRIM(RTRIM(@InvoiceNo)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [Reconciliation].[PaymentDate] > LTRIM(RTRIM(@PaymentDate)))
AND   (@PaymentAmount IS NULL OR @PaymentAmount = '' OR [Reconciliation].[PaymentAmount] > LTRIM(RTRIM(@PaymentAmount)))
AND   (@TDSAmount IS NULL OR @TDSAmount = '' OR [Reconciliation].[TDSAmount] > LTRIM(RTRIM(@TDSAmount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Reconciliation].[Remarks] > LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Reconciliation].[ReconciliationID]
     ,[Reconciliation].[InvoiceID]
     ,[Reconciliation].[PaymentDate]
     ,[Reconciliation].[PaymentAmount]
     ,[Reconciliation].[TDSAmount]
     ,[Reconciliation].[Remarks]
FROM
     [Reconciliation]
     INNER JOIN [Invoice] ON [Reconciliation].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@ReconciliationID IS NULL OR @ReconciliationID = '' OR [Reconciliation].[ReconciliationID] < LTRIM(RTRIM(@ReconciliationID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] < LTRIM(RTRIM(@InvoiceNo)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [Reconciliation].[PaymentDate] < LTRIM(RTRIM(@PaymentDate)))
AND   (@PaymentAmount IS NULL OR @PaymentAmount = '' OR [Reconciliation].[PaymentAmount] < LTRIM(RTRIM(@PaymentAmount)))
AND   (@TDSAmount IS NULL OR @TDSAmount = '' OR [Reconciliation].[TDSAmount] < LTRIM(RTRIM(@TDSAmount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Reconciliation].[Remarks] < LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Reconciliation].[ReconciliationID]
     ,[Reconciliation].[InvoiceID]
     ,[Reconciliation].[PaymentDate]
     ,[Reconciliation].[PaymentAmount]
     ,[Reconciliation].[TDSAmount]
     ,[Reconciliation].[Remarks]
FROM
     [Reconciliation]
     INNER JOIN [Invoice] ON [Reconciliation].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@ReconciliationID IS NULL OR @ReconciliationID = '' OR [Reconciliation].[ReconciliationID] >= LTRIM(RTRIM(@ReconciliationID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] >= LTRIM(RTRIM(@InvoiceNo)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [Reconciliation].[PaymentDate] >= LTRIM(RTRIM(@PaymentDate)))
AND   (@PaymentAmount IS NULL OR @PaymentAmount = '' OR [Reconciliation].[PaymentAmount] >= LTRIM(RTRIM(@PaymentAmount)))
AND   (@TDSAmount IS NULL OR @TDSAmount = '' OR [Reconciliation].[TDSAmount] >= LTRIM(RTRIM(@TDSAmount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Reconciliation].[Remarks] >= LTRIM(RTRIM(@Remarks)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Reconciliation].[ReconciliationID]
     ,[Reconciliation].[InvoiceID]
     ,[Reconciliation].[PaymentDate]
     ,[Reconciliation].[PaymentAmount]
     ,[Reconciliation].[TDSAmount]
     ,[Reconciliation].[Remarks]
FROM
     [Reconciliation]
     INNER JOIN [Invoice] ON [Reconciliation].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@ReconciliationID IS NULL OR @ReconciliationID = '' OR [Reconciliation].[ReconciliationID] <= LTRIM(RTRIM(@ReconciliationID)))
AND   (@InvoiceNo IS NULL OR @InvoiceNo = '' OR [Invoice].[InvoiceNo] <= LTRIM(RTRIM(@InvoiceNo)))
AND   (@PaymentDate IS NULL OR @PaymentDate = '' OR [Reconciliation].[PaymentDate] <= LTRIM(RTRIM(@PaymentDate)))
AND   (@PaymentAmount IS NULL OR @PaymentAmount = '' OR [Reconciliation].[PaymentAmount] <= LTRIM(RTRIM(@PaymentAmount)))
AND   (@TDSAmount IS NULL OR @TDSAmount = '' OR [Reconciliation].[TDSAmount] <= LTRIM(RTRIM(@TDSAmount)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Reconciliation].[Remarks] <= LTRIM(RTRIM(@Remarks)))

END

END
GO

GRANT EXECUTE ON [ReconciliationSearch] TO [Public]
GO

 
