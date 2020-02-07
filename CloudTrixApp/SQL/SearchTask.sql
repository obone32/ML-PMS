--***********************************************************
--SEARCH Stored Procedure for Task table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskSearch] 
GO

CREATE PROCEDURE [TaskSearch] 
      @TaskID int
     ,@TaskName varchar(8000)
     ,@Description varchar(8000)
     ,@CreationDate datetime
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [Task].[TaskID]
     ,[Task].[TaskName]
     ,[Task].[Description]
     ,[Task].[CreationDate]
FROM
     [Task]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] LIKE '%' + LTRIM(RTRIM(@TaskID)) + '%')
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] LIKE '%' + LTRIM(RTRIM(@TaskName)) + '%')
AND   (@Description IS NULL OR @Description = '' OR [Task].[Description] LIKE '%' + LTRIM(RTRIM(@Description)) + '%')
AND   (@CreationDate IS NULL OR @CreationDate = '' OR [Task].[CreationDate] LIKE '%' + LTRIM(RTRIM(@CreationDate)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Task].[TaskID]
     ,[Task].[TaskName]
     ,[Task].[Description]
     ,[Task].[CreationDate]
FROM
     [Task]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] = LTRIM(RTRIM(@TaskID)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] = LTRIM(RTRIM(@TaskName)))
AND   (@Description IS NULL OR @Description = '' OR [Task].[Description] = LTRIM(RTRIM(@Description)))
AND   (@CreationDate IS NULL OR @CreationDate = '' OR [Task].[CreationDate] = LTRIM(RTRIM(@CreationDate)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Task].[TaskID]
     ,[Task].[TaskName]
     ,[Task].[Description]
     ,[Task].[CreationDate]
FROM
     [Task]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] LIKE LTRIM(RTRIM(@TaskID)) + '%')
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] LIKE LTRIM(RTRIM(@TaskName)) + '%')
AND   (@Description IS NULL OR @Description = '' OR [Task].[Description] LIKE LTRIM(RTRIM(@Description)) + '%')
AND   (@CreationDate IS NULL OR @CreationDate = '' OR [Task].[CreationDate] LIKE LTRIM(RTRIM(@CreationDate)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Task].[TaskID]
     ,[Task].[TaskName]
     ,[Task].[Description]
     ,[Task].[CreationDate]
FROM
     [Task]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] > LTRIM(RTRIM(@TaskID)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] > LTRIM(RTRIM(@TaskName)))
AND   (@Description IS NULL OR @Description = '' OR [Task].[Description] > LTRIM(RTRIM(@Description)))
AND   (@CreationDate IS NULL OR @CreationDate = '' OR [Task].[CreationDate] > LTRIM(RTRIM(@CreationDate)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Task].[TaskID]
     ,[Task].[TaskName]
     ,[Task].[Description]
     ,[Task].[CreationDate]
FROM
     [Task]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] < LTRIM(RTRIM(@TaskID)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] < LTRIM(RTRIM(@TaskName)))
AND   (@Description IS NULL OR @Description = '' OR [Task].[Description] < LTRIM(RTRIM(@Description)))
AND   (@CreationDate IS NULL OR @CreationDate = '' OR [Task].[CreationDate] < LTRIM(RTRIM(@CreationDate)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Task].[TaskID]
     ,[Task].[TaskName]
     ,[Task].[Description]
     ,[Task].[CreationDate]
FROM
     [Task]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] >= LTRIM(RTRIM(@TaskID)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] >= LTRIM(RTRIM(@TaskName)))
AND   (@Description IS NULL OR @Description = '' OR [Task].[Description] >= LTRIM(RTRIM(@Description)))
AND   (@CreationDate IS NULL OR @CreationDate = '' OR [Task].[CreationDate] >= LTRIM(RTRIM(@CreationDate)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Task].[TaskID]
     ,[Task].[TaskName]
     ,[Task].[Description]
     ,[Task].[CreationDate]
FROM
     [Task]
WHERE
      (@TaskID IS NULL OR @TaskID = '' OR [Task].[TaskID] <= LTRIM(RTRIM(@TaskID)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] <= LTRIM(RTRIM(@TaskName)))
AND   (@Description IS NULL OR @Description = '' OR [Task].[Description] <= LTRIM(RTRIM(@Description)))
AND   (@CreationDate IS NULL OR @CreationDate = '' OR [Task].[CreationDate] <= LTRIM(RTRIM(@CreationDate)))

END

END
GO

GRANT EXECUTE ON [TaskSearch] TO [Public]
GO

 
