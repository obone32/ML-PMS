--***********************************************************
--SEARCH Stored Procedure for TaskAttachment table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAttachmentSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAttachmentSearch] 
GO

CREATE PROCEDURE [TaskAttachmentSearch] 
      @TaskID int
     ,@TaskAttachmentID int
     ,@AttachmentName varchar(8000)
     ,@Decription varchar(8000)
     ,@FilePath varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [TaskAttachment].[TaskID]
     ,[TaskAttachment].[TaskAttachmentID]
     ,[TaskAttachment].[AttachmentName]
     ,[TaskAttachment].[Decription]
     ,[TaskAttachment].[FilePath]
FROM
     [TaskAttachment]
     INNER JOIN [Task] ON [TaskAttachment].[TaskID] = [Task].[TaskID]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] LIKE '%' + LTRIM(RTRIM(@TaskID)) + '%')
AND   (@TaskAttachmentID IS NULL OR @TaskAttachmentID = '' OR [TaskAttachment].[TaskAttachmentID] LIKE '%' + LTRIM(RTRIM(@TaskAttachmentID)) + '%')
AND   (@AttachmentName IS NULL OR @AttachmentName = '' OR [TaskAttachment].[AttachmentName] LIKE '%' + LTRIM(RTRIM(@AttachmentName)) + '%')
AND   (@Decription IS NULL OR @Decription = '' OR [TaskAttachment].[Decription] LIKE '%' + LTRIM(RTRIM(@Decription)) + '%')
AND   (@FilePath IS NULL OR @FilePath = '' OR [TaskAttachment].[FilePath] LIKE '%' + LTRIM(RTRIM(@FilePath)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [TaskAttachment].[TaskID]
     ,[TaskAttachment].[TaskAttachmentID]
     ,[TaskAttachment].[AttachmentName]
     ,[TaskAttachment].[Decription]
     ,[TaskAttachment].[FilePath]
FROM
     [TaskAttachment]
     INNER JOIN [Task] ON [TaskAttachment].[TaskID] = [Task].[TaskID]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] = LTRIM(RTRIM(@TaskID)))
AND   (@TaskAttachmentID IS NULL OR @TaskAttachmentID = '' OR [TaskAttachment].[TaskAttachmentID] = LTRIM(RTRIM(@TaskAttachmentID)))
AND   (@AttachmentName IS NULL OR @AttachmentName = '' OR [TaskAttachment].[AttachmentName] = LTRIM(RTRIM(@AttachmentName)))
AND   (@Decription IS NULL OR @Decription = '' OR [TaskAttachment].[Decription] = LTRIM(RTRIM(@Decription)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [TaskAttachment].[FilePath] = LTRIM(RTRIM(@FilePath)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [TaskAttachment].[TaskID]
     ,[TaskAttachment].[TaskAttachmentID]
     ,[TaskAttachment].[AttachmentName]
     ,[TaskAttachment].[Decription]
     ,[TaskAttachment].[FilePath]
FROM
     [TaskAttachment]
     INNER JOIN [Task] ON [TaskAttachment].[TaskID] = [Task].[TaskID]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] LIKE LTRIM(RTRIM(@TaskID)) + '%')
AND   (@TaskAttachmentID IS NULL OR @TaskAttachmentID = '' OR [TaskAttachment].[TaskAttachmentID] LIKE LTRIM(RTRIM(@TaskAttachmentID)) + '%')
AND   (@AttachmentName IS NULL OR @AttachmentName = '' OR [TaskAttachment].[AttachmentName] LIKE LTRIM(RTRIM(@AttachmentName)) + '%')
AND   (@Decription IS NULL OR @Decription = '' OR [TaskAttachment].[Decription] LIKE LTRIM(RTRIM(@Decription)) + '%')
AND   (@FilePath IS NULL OR @FilePath = '' OR [TaskAttachment].[FilePath] LIKE LTRIM(RTRIM(@FilePath)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [TaskAttachment].[TaskID]
     ,[TaskAttachment].[TaskAttachmentID]
     ,[TaskAttachment].[AttachmentName]
     ,[TaskAttachment].[Decription]
     ,[TaskAttachment].[FilePath]
FROM
     [TaskAttachment]
     INNER JOIN [Task] ON [TaskAttachment].[TaskID] = [Task].[TaskID]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] > LTRIM(RTRIM(@TaskID)))
AND   (@TaskAttachmentID IS NULL OR @TaskAttachmentID = '' OR [TaskAttachment].[TaskAttachmentID] > LTRIM(RTRIM(@TaskAttachmentID)))
AND   (@AttachmentName IS NULL OR @AttachmentName = '' OR [TaskAttachment].[AttachmentName] > LTRIM(RTRIM(@AttachmentName)))
AND   (@Decription IS NULL OR @Decription = '' OR [TaskAttachment].[Decription] > LTRIM(RTRIM(@Decription)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [TaskAttachment].[FilePath] > LTRIM(RTRIM(@FilePath)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [TaskAttachment].[TaskID]
     ,[TaskAttachment].[TaskAttachmentID]
     ,[TaskAttachment].[AttachmentName]
     ,[TaskAttachment].[Decription]
     ,[TaskAttachment].[FilePath]
FROM
     [TaskAttachment]
     INNER JOIN [Task] ON [TaskAttachment].[TaskID] = [Task].[TaskID]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] < LTRIM(RTRIM(@TaskID)))
AND   (@TaskAttachmentID IS NULL OR @TaskAttachmentID = '' OR [TaskAttachment].[TaskAttachmentID] < LTRIM(RTRIM(@TaskAttachmentID)))
AND   (@AttachmentName IS NULL OR @AttachmentName = '' OR [TaskAttachment].[AttachmentName] < LTRIM(RTRIM(@AttachmentName)))
AND   (@Decription IS NULL OR @Decription = '' OR [TaskAttachment].[Decription] < LTRIM(RTRIM(@Decription)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [TaskAttachment].[FilePath] < LTRIM(RTRIM(@FilePath)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [TaskAttachment].[TaskID]
     ,[TaskAttachment].[TaskAttachmentID]
     ,[TaskAttachment].[AttachmentName]
     ,[TaskAttachment].[Decription]
     ,[TaskAttachment].[FilePath]
FROM
     [TaskAttachment]
     INNER JOIN [Task] ON [TaskAttachment].[TaskID] = [Task].[TaskID]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] >= LTRIM(RTRIM(@TaskID)))
AND   (@TaskAttachmentID IS NULL OR @TaskAttachmentID = '' OR [TaskAttachment].[TaskAttachmentID] >= LTRIM(RTRIM(@TaskAttachmentID)))
AND   (@AttachmentName IS NULL OR @AttachmentName = '' OR [TaskAttachment].[AttachmentName] >= LTRIM(RTRIM(@AttachmentName)))
AND   (@Decription IS NULL OR @Decription = '' OR [TaskAttachment].[Decription] >= LTRIM(RTRIM(@Decription)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [TaskAttachment].[FilePath] >= LTRIM(RTRIM(@FilePath)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [TaskAttachment].[TaskID]
     ,[TaskAttachment].[TaskAttachmentID]
     ,[TaskAttachment].[AttachmentName]
     ,[TaskAttachment].[Decription]
     ,[TaskAttachment].[FilePath]
FROM
     [TaskAttachment]
     INNER JOIN [Task] ON [TaskAttachment].[TaskID] = [Task].[TaskID]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] <= LTRIM(RTRIM(@TaskID)))
AND   (@TaskAttachmentID IS NULL OR @TaskAttachmentID = '' OR [TaskAttachment].[TaskAttachmentID] <= LTRIM(RTRIM(@TaskAttachmentID)))
AND   (@AttachmentName IS NULL OR @AttachmentName = '' OR [TaskAttachment].[AttachmentName] <= LTRIM(RTRIM(@AttachmentName)))
AND   (@Decription IS NULL OR @Decription = '' OR [TaskAttachment].[Decription] <= LTRIM(RTRIM(@Decription)))
AND   (@FilePath IS NULL OR @FilePath = '' OR [TaskAttachment].[FilePath] <= LTRIM(RTRIM(@FilePath)))

END

END
GO

GRANT EXECUTE ON [TaskAttachmentSearch] TO [Public]
GO

 
