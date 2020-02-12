--***********************************************************
--SEARCH Stored Procedure for InvoiceAdvance table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceAdvanceSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceAdvanceSearch] 
GO

CREATE PROCEDURE [InvoiceAdvanceSearch] 
      @InvoiceID int
     ,@AdvancePaymentID int
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [InvoiceAdvance].[InvoiceID]
     ,[InvoiceAdvance].[AdvancePaymentID]
FROM
     [InvoiceAdvance]
     INNER JOIN [Invoice] ON [InvoiceAdvance].[InvoiceID] = [Invoice].[InvoiceID]
     INNER JOIN [AdvancePayment] ON [InvoiceAdvance].[AdvancePaymentID] = [AdvancePayment].[AdvancePaymentID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] LIKE '%' + LTRIM(RTRIM(@InvoiceID)) + '%')
AND   (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] LIKE '%' + LTRIM(RTRIM(@AdvancePaymentID)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [InvoiceAdvance].[InvoiceID]
     ,[InvoiceAdvance].[AdvancePaymentID]
FROM
     [InvoiceAdvance]
     INNER JOIN [Invoice] ON [InvoiceAdvance].[InvoiceID] = [Invoice].[InvoiceID]
     INNER JOIN [AdvancePayment] ON [InvoiceAdvance].[AdvancePaymentID] = [AdvancePayment].[AdvancePaymentID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] = LTRIM(RTRIM(@InvoiceID)))
AND   (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] = LTRIM(RTRIM(@AdvancePaymentID)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [InvoiceAdvance].[InvoiceID]
     ,[InvoiceAdvance].[AdvancePaymentID]
FROM
     [InvoiceAdvance]
     INNER JOIN [Invoice] ON [InvoiceAdvance].[InvoiceID] = [Invoice].[InvoiceID]
     INNER JOIN [AdvancePayment] ON [InvoiceAdvance].[AdvancePaymentID] = [AdvancePayment].[AdvancePaymentID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] LIKE LTRIM(RTRIM(@InvoiceID)) + '%')
AND   (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] LIKE LTRIM(RTRIM(@AdvancePaymentID)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [InvoiceAdvance].[InvoiceID]
     ,[InvoiceAdvance].[AdvancePaymentID]
FROM
     [InvoiceAdvance]
     INNER JOIN [Invoice] ON [InvoiceAdvance].[InvoiceID] = [Invoice].[InvoiceID]
     INNER JOIN [AdvancePayment] ON [InvoiceAdvance].[AdvancePaymentID] = [AdvancePayment].[AdvancePaymentID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] > LTRIM(RTRIM(@InvoiceID)))
AND   (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] > LTRIM(RTRIM(@AdvancePaymentID)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [InvoiceAdvance].[InvoiceID]
     ,[InvoiceAdvance].[AdvancePaymentID]
FROM
     [InvoiceAdvance]
     INNER JOIN [Invoice] ON [InvoiceAdvance].[InvoiceID] = [Invoice].[InvoiceID]
     INNER JOIN [AdvancePayment] ON [InvoiceAdvance].[AdvancePaymentID] = [AdvancePayment].[AdvancePaymentID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] < LTRIM(RTRIM(@InvoiceID)))
AND   (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] < LTRIM(RTRIM(@AdvancePaymentID)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [InvoiceAdvance].[InvoiceID]
     ,[InvoiceAdvance].[AdvancePaymentID]
FROM
     [InvoiceAdvance]
     INNER JOIN [Invoice] ON [InvoiceAdvance].[InvoiceID] = [Invoice].[InvoiceID]
     INNER JOIN [AdvancePayment] ON [InvoiceAdvance].[AdvancePaymentID] = [AdvancePayment].[AdvancePaymentID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] >= LTRIM(RTRIM(@InvoiceID)))
AND   (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] >= LTRIM(RTRIM(@AdvancePaymentID)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [InvoiceAdvance].[InvoiceID]
     ,[InvoiceAdvance].[AdvancePaymentID]
FROM
     [InvoiceAdvance]
     INNER JOIN [Invoice] ON [InvoiceAdvance].[InvoiceID] = [Invoice].[InvoiceID]
     INNER JOIN [AdvancePayment] ON [InvoiceAdvance].[AdvancePaymentID] = [AdvancePayment].[AdvancePaymentID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] <= LTRIM(RTRIM(@InvoiceID)))
AND   (@AdvancePaymentID IS NULL OR @AdvancePaymentID = '' OR [AdvancePayment].[AdvancePaymentID] <= LTRIM(RTRIM(@AdvancePaymentID)))

END

END
GO

GRANT EXECUTE ON [InvoiceAdvanceSearch] TO [Public]
GO

 
