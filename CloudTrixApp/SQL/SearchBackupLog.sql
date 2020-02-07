--***********************************************************
--SEARCH Stored Procedure for BackupLog table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'BackupLogSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [BackupLogSearch] 
GO

CREATE PROCEDURE [BackupLogSearch] 
      @BackupLogID int
     ,@BackupDate datetime
     ,@FilePath varchar(8000)
     ,@Remarks varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [BackupLog].[BackupLogID]
     ,[BackupLog].[BackupDate]
     ,[BackupLog].[FilePath]
     ,[BackupLog].[Remarks]
FROM
     [BackupLog]
WHERE
      (@BackupLogID IS NULL OR @BackupLogID = '' OR [BackupLog].[BackupLogID] LIKE '%' + LTRIM(RTRIM(@BackupLogID)) + '%')
AND   (@BackupDate IS NULL OR @BackupDate = '' OR [BackupLog].[BackupDate] LIKE '%' + LTRIM(RTRIM(@BackupDate)) + '%')
AND   (@FilePath IS NULL OR @FilePath = '' OR [BackupLog].[FilePath] LIKE '%' + LTRIM(RTRIM(@FilePath)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [BackupLog].[Remarks] LIKE '%' + LTRIM(RTRIM(@Remarks)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [BackupLog].[BackupLogID]
     ,[BackupLog].[BackupDate]
     ,[BackupLog].[FilePath]
     ,[BackupLog].[Remarks]
FROM
     [BackupLog]
WHERE
      (@BackupLogID IS NULL OR @BackupLogID = '' OR [BackupLog].[BackupLogID] = LTRIM(RTRIM(@BackupLogID)))
AND   (@BackupDate IS NULL OR @BackupDate = '' OR [BackupLog].[BackupDate] = LTRIM(RTRIM(@BackupDate)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [BackupLog].[FilePath] = LTRIM(RTRIM(@FilePath)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [BackupLog].[Remarks] = LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [BackupLog].[BackupLogID]
     ,[BackupLog].[BackupDate]
     ,[BackupLog].[FilePath]
     ,[BackupLog].[Remarks]
FROM
     [BackupLog]
WHERE
      (@BackupLogID IS NULL OR @BackupLogID = '' OR [BackupLog].[BackupLogID] LIKE LTRIM(RTRIM(@BackupLogID)) + '%')
AND   (@BackupDate IS NULL OR @BackupDate = '' OR [BackupLog].[BackupDate] LIKE LTRIM(RTRIM(@BackupDate)) + '%')
AND   (@FilePath IS NULL OR @FilePath = '' OR [BackupLog].[FilePath] LIKE LTRIM(RTRIM(@FilePath)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [BackupLog].[Remarks] LIKE LTRIM(RTRIM(@Remarks)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [BackupLog].[BackupLogID]
     ,[BackupLog].[BackupDate]
     ,[BackupLog].[FilePath]
     ,[BackupLog].[Remarks]
FROM
     [BackupLog]
WHERE
      (@BackupLogID IS NULL OR @BackupLogID = '' OR [BackupLog].[BackupLogID] > LTRIM(RTRIM(@BackupLogID)))
AND   (@BackupDate IS NULL OR @BackupDate = '' OR [BackupLog].[BackupDate] > LTRIM(RTRIM(@BackupDate)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [BackupLog].[FilePath] > LTRIM(RTRIM(@FilePath)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [BackupLog].[Remarks] > LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [BackupLog].[BackupLogID]
     ,[BackupLog].[BackupDate]
     ,[BackupLog].[FilePath]
     ,[BackupLog].[Remarks]
FROM
     [BackupLog]
WHERE
      (@BackupLogID IS NULL OR @BackupLogID = '' OR [BackupLog].[BackupLogID] < LTRIM(RTRIM(@BackupLogID)))
AND   (@BackupDate IS NULL OR @BackupDate = '' OR [BackupLog].[BackupDate] < LTRIM(RTRIM(@BackupDate)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [BackupLog].[FilePath] < LTRIM(RTRIM(@FilePath)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [BackupLog].[Remarks] < LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [BackupLog].[BackupLogID]
     ,[BackupLog].[BackupDate]
     ,[BackupLog].[FilePath]
     ,[BackupLog].[Remarks]
FROM
     [BackupLog]
WHERE
      (@BackupLogID IS NULL OR @BackupLogID = '' OR [BackupLog].[BackupLogID] >= LTRIM(RTRIM(@BackupLogID)))
AND   (@BackupDate IS NULL OR @BackupDate = '' OR [BackupLog].[BackupDate] >= LTRIM(RTRIM(@BackupDate)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [BackupLog].[FilePath] >= LTRIM(RTRIM(@FilePath)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [BackupLog].[Remarks] >= LTRIM(RTRIM(@Remarks)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [BackupLog].[BackupLogID]
     ,[BackupLog].[BackupDate]
     ,[BackupLog].[FilePath]
     ,[BackupLog].[Remarks]
FROM
     [BackupLog]
WHERE
      (@BackupLogID IS NULL OR @BackupLogID = '' OR [BackupLog].[BackupLogID] <= LTRIM(RTRIM(@BackupLogID)))
AND   (@BackupDate IS NULL OR @BackupDate = '' OR [BackupLog].[BackupDate] <= LTRIM(RTRIM(@BackupDate)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [BackupLog].[FilePath] <= LTRIM(RTRIM(@FilePath)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [BackupLog].[Remarks] <= LTRIM(RTRIM(@Remarks)))

END

END
GO

GRANT EXECUTE ON [BackupLogSearch] TO [Public]
GO

 
