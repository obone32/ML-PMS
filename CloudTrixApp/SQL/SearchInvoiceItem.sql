--***********************************************************
--SEARCH Stored Procedure for InvoiceItem table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'InvoiceItemSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [InvoiceItemSearch] 
GO

CREATE PROCEDURE [InvoiceItemSearch] 
      @InvoiceID int
     ,@InvoiceItemID int
     ,@Description varchar(8000)
     ,@Quantity decimal(18,2)
     ,@Rate decimal(18,2)
     ,@DiscountAmount decimal(18,2)
     ,@CGSTRate decimal(18,2)
     ,@SGSTRate decimal(18,2)
     ,@IGSTRate decimal(18,2)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [InvoiceItem].[InvoiceID]
     ,[InvoiceItem].[InvoiceItemID]
     ,[InvoiceItem].[Description]
     ,[InvoiceItem].[Quantity]
     ,[InvoiceItem].[Rate]
     ,[InvoiceItem].[DiscountAmount]
     ,[InvoiceItem].[CGSTRate]
     ,[InvoiceItem].[SGSTRate]
     ,[InvoiceItem].[IGSTRate]
FROM
     [InvoiceItem]
     INNER JOIN [Invoice] ON [InvoiceItem].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] LIKE '%' + LTRIM(RTRIM(@InvoiceID)) + '%')
AND   (@InvoiceItemID IS NULL OR @InvoiceItemID = '' OR [InvoiceItem].[InvoiceItemID] LIKE '%' + LTRIM(RTRIM(@InvoiceItemID)) + '%')
AND   (@Description IS NULL OR @Description = '' OR [InvoiceItem].[Description] LIKE '%' + LTRIM(RTRIM(@Description)) + '%')
AND   (@Quantity IS NULL OR @Quantity = '' OR [InvoiceItem].[Quantity] LIKE '%' + LTRIM(RTRIM(@Quantity)) + '%')
AND   (@Rate IS NULL OR @Rate = '' OR [InvoiceItem].[Rate] LIKE '%' + LTRIM(RTRIM(@Rate)) + '%')
AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [InvoiceItem].[DiscountAmount] LIKE '%' + LTRIM(RTRIM(@DiscountAmount)) + '%')
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [InvoiceItem].[CGSTRate] LIKE '%' + LTRIM(RTRIM(@CGSTRate)) + '%')
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [InvoiceItem].[SGSTRate] LIKE '%' + LTRIM(RTRIM(@SGSTRate)) + '%')
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [InvoiceItem].[IGSTRate] LIKE '%' + LTRIM(RTRIM(@IGSTRate)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [InvoiceItem].[InvoiceID]
     ,[InvoiceItem].[InvoiceItemID]
     ,[InvoiceItem].[Description]
     ,[InvoiceItem].[Quantity]
     ,[InvoiceItem].[Rate]
     ,[InvoiceItem].[DiscountAmount]
     ,[InvoiceItem].[CGSTRate]
     ,[InvoiceItem].[SGSTRate]
     ,[InvoiceItem].[IGSTRate]
FROM
     [InvoiceItem]
     INNER JOIN [Invoice] ON [InvoiceItem].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] = LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceItemID IS NULL OR @InvoiceItemID = '' OR [InvoiceItem].[InvoiceItemID] = LTRIM(RTRIM(@InvoiceItemID)))
AND   (@Description IS NULL OR @Description = '' OR [InvoiceItem].[Description] = LTRIM(RTRIM(@Description)))
AND   (@Quantity IS NULL OR @Quantity = '' OR [InvoiceItem].[Quantity] = LTRIM(RTRIM(@Quantity)))
AND   (@Rate IS NULL OR @Rate = '' OR [InvoiceItem].[Rate] = LTRIM(RTRIM(@Rate)))
AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [InvoiceItem].[DiscountAmount] = LTRIM(RTRIM(@DiscountAmount)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [InvoiceItem].[CGSTRate] = LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [InvoiceItem].[SGSTRate] = LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [InvoiceItem].[IGSTRate] = LTRIM(RTRIM(@IGSTRate)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [InvoiceItem].[InvoiceID]
     ,[InvoiceItem].[InvoiceItemID]
     ,[InvoiceItem].[Description]
     ,[InvoiceItem].[Quantity]
     ,[InvoiceItem].[Rate]
     ,[InvoiceItem].[DiscountAmount]
     ,[InvoiceItem].[CGSTRate]
     ,[InvoiceItem].[SGSTRate]
     ,[InvoiceItem].[IGSTRate]
FROM
     [InvoiceItem]
     INNER JOIN [Invoice] ON [InvoiceItem].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] LIKE LTRIM(RTRIM(@InvoiceID)) + '%')
AND   (@InvoiceItemID IS NULL OR @InvoiceItemID = '' OR [InvoiceItem].[InvoiceItemID] LIKE LTRIM(RTRIM(@InvoiceItemID)) + '%')
AND   (@Description IS NULL OR @Description = '' OR [InvoiceItem].[Description] LIKE LTRIM(RTRIM(@Description)) + '%')
AND   (@Quantity IS NULL OR @Quantity = '' OR [InvoiceItem].[Quantity] LIKE LTRIM(RTRIM(@Quantity)) + '%')
AND   (@Rate IS NULL OR @Rate = '' OR [InvoiceItem].[Rate] LIKE LTRIM(RTRIM(@Rate)) + '%')
AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [InvoiceItem].[DiscountAmount] LIKE LTRIM(RTRIM(@DiscountAmount)) + '%')
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [InvoiceItem].[CGSTRate] LIKE LTRIM(RTRIM(@CGSTRate)) + '%')
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [InvoiceItem].[SGSTRate] LIKE LTRIM(RTRIM(@SGSTRate)) + '%')
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [InvoiceItem].[IGSTRate] LIKE LTRIM(RTRIM(@IGSTRate)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [InvoiceItem].[InvoiceID]
     ,[InvoiceItem].[InvoiceItemID]
     ,[InvoiceItem].[Description]
     ,[InvoiceItem].[Quantity]
     ,[InvoiceItem].[Rate]
     ,[InvoiceItem].[DiscountAmount]
     ,[InvoiceItem].[CGSTRate]
     ,[InvoiceItem].[SGSTRate]
     ,[InvoiceItem].[IGSTRate]
FROM
     [InvoiceItem]
     INNER JOIN [Invoice] ON [InvoiceItem].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] > LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceItemID IS NULL OR @InvoiceItemID = '' OR [InvoiceItem].[InvoiceItemID] > LTRIM(RTRIM(@InvoiceItemID)))
AND   (@Description IS NULL OR @Description = '' OR [InvoiceItem].[Description] > LTRIM(RTRIM(@Description)))
AND   (@Quantity IS NULL OR @Quantity = '' OR [InvoiceItem].[Quantity] > LTRIM(RTRIM(@Quantity)))
AND   (@Rate IS NULL OR @Rate = '' OR [InvoiceItem].[Rate] > LTRIM(RTRIM(@Rate)))
AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [InvoiceItem].[DiscountAmount] > LTRIM(RTRIM(@DiscountAmount)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [InvoiceItem].[CGSTRate] > LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [InvoiceItem].[SGSTRate] > LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [InvoiceItem].[IGSTRate] > LTRIM(RTRIM(@IGSTRate)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [InvoiceItem].[InvoiceID]
     ,[InvoiceItem].[InvoiceItemID]
     ,[InvoiceItem].[Description]
     ,[InvoiceItem].[Quantity]
     ,[InvoiceItem].[Rate]
     ,[InvoiceItem].[DiscountAmount]
     ,[InvoiceItem].[CGSTRate]
     ,[InvoiceItem].[SGSTRate]
     ,[InvoiceItem].[IGSTRate]
FROM
     [InvoiceItem]
     INNER JOIN [Invoice] ON [InvoiceItem].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] < LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceItemID IS NULL OR @InvoiceItemID = '' OR [InvoiceItem].[InvoiceItemID] < LTRIM(RTRIM(@InvoiceItemID)))
AND   (@Description IS NULL OR @Description = '' OR [InvoiceItem].[Description] < LTRIM(RTRIM(@Description)))
AND   (@Quantity IS NULL OR @Quantity = '' OR [InvoiceItem].[Quantity] < LTRIM(RTRIM(@Quantity)))
AND   (@Rate IS NULL OR @Rate = '' OR [InvoiceItem].[Rate] < LTRIM(RTRIM(@Rate)))
AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [InvoiceItem].[DiscountAmount] < LTRIM(RTRIM(@DiscountAmount)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [InvoiceItem].[CGSTRate] < LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [InvoiceItem].[SGSTRate] < LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [InvoiceItem].[IGSTRate] < LTRIM(RTRIM(@IGSTRate)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [InvoiceItem].[InvoiceID]
     ,[InvoiceItem].[InvoiceItemID]
     ,[InvoiceItem].[Description]
     ,[InvoiceItem].[Quantity]
     ,[InvoiceItem].[Rate]
     ,[InvoiceItem].[DiscountAmount]
     ,[InvoiceItem].[CGSTRate]
     ,[InvoiceItem].[SGSTRate]
     ,[InvoiceItem].[IGSTRate]
FROM
     [InvoiceItem]
     INNER JOIN [Invoice] ON [InvoiceItem].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] >= LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceItemID IS NULL OR @InvoiceItemID = '' OR [InvoiceItem].[InvoiceItemID] >= LTRIM(RTRIM(@InvoiceItemID)))
AND   (@Description IS NULL OR @Description = '' OR [InvoiceItem].[Description] >= LTRIM(RTRIM(@Description)))
AND   (@Quantity IS NULL OR @Quantity = '' OR [InvoiceItem].[Quantity] >= LTRIM(RTRIM(@Quantity)))
AND   (@Rate IS NULL OR @Rate = '' OR [InvoiceItem].[Rate] >= LTRIM(RTRIM(@Rate)))
AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [InvoiceItem].[DiscountAmount] >= LTRIM(RTRIM(@DiscountAmount)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [InvoiceItem].[CGSTRate] >= LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [InvoiceItem].[SGSTRate] >= LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [InvoiceItem].[IGSTRate] >= LTRIM(RTRIM(@IGSTRate)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [InvoiceItem].[InvoiceID]
     ,[InvoiceItem].[InvoiceItemID]
     ,[InvoiceItem].[Description]
     ,[InvoiceItem].[Quantity]
     ,[InvoiceItem].[Rate]
     ,[InvoiceItem].[DiscountAmount]
     ,[InvoiceItem].[CGSTRate]
     ,[InvoiceItem].[SGSTRate]
     ,[InvoiceItem].[IGSTRate]
FROM
     [InvoiceItem]
     INNER JOIN [Invoice] ON [InvoiceItem].[InvoiceID] = [Invoice].[InvoiceID]
WHERE
      (@InvoiceID IS NULL OR @InvoiceID = '' OR [Invoice].[InvoiceID] <= LTRIM(RTRIM(@InvoiceID)))
AND   (@InvoiceItemID IS NULL OR @InvoiceItemID = '' OR [InvoiceItem].[InvoiceItemID] <= LTRIM(RTRIM(@InvoiceItemID)))
AND   (@Description IS NULL OR @Description = '' OR [InvoiceItem].[Description] <= LTRIM(RTRIM(@Description)))
AND   (@Quantity IS NULL OR @Quantity = '' OR [InvoiceItem].[Quantity] <= LTRIM(RTRIM(@Quantity)))
AND   (@Rate IS NULL OR @Rate = '' OR [InvoiceItem].[Rate] <= LTRIM(RTRIM(@Rate)))
AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [InvoiceItem].[DiscountAmount] <= LTRIM(RTRIM(@DiscountAmount)))
AND   (@CGSTRate IS NULL OR @CGSTRate = '' OR [InvoiceItem].[CGSTRate] <= LTRIM(RTRIM(@CGSTRate)))
AND   (@SGSTRate IS NULL OR @SGSTRate = '' OR [InvoiceItem].[SGSTRate] <= LTRIM(RTRIM(@SGSTRate)))
AND   (@IGSTRate IS NULL OR @IGSTRate = '' OR [InvoiceItem].[IGSTRate] <= LTRIM(RTRIM(@IGSTRate)))

END

END
GO

GRANT EXECUTE ON [InvoiceItemSearch] TO [Public]
GO

 
