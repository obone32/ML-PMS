--***********************************************************
--SEARCH Stored Procedure for TaskState table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskStateSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskStateSearch] 
GO

CREATE PROCEDURE [TaskStateSearch] 
      @TaskStateID int
     ,@TaskStateName varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [TaskState].[TaskStateID]
     ,[TaskState].[TaskStateName]
FROM
     [TaskState]
WHERE
      (@TaskStateID IS NULL OR @TaskStateID = '' OR [TaskState].[TaskStateID] LIKE '%' + LTRIM(RTRIM(@TaskStateID)) + '%')
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] LIKE '%' + LTRIM(RTRIM(@TaskStateName)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [TaskState].[TaskStateID]
     ,[TaskState].[TaskStateName]
FROM
     [TaskState]
WHERE
      (@TaskStateID IS NULL OR @TaskStateID = '' OR [TaskState].[TaskStateID] = LTRIM(RTRIM(@TaskStateID)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] = LTRIM(RTRIM(@TaskStateName)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [TaskState].[TaskStateID]
     ,[TaskState].[TaskStateName]
FROM
     [TaskState]
WHERE
      (@TaskStateID IS NULL OR @TaskStateID = '' OR [TaskState].[TaskStateID] LIKE LTRIM(RTRIM(@TaskStateID)) + '%')
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] LIKE LTRIM(RTRIM(@TaskStateName)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [TaskState].[TaskStateID]
     ,[TaskState].[TaskStateName]
FROM
     [TaskState]
WHERE
      (@TaskStateID IS NULL OR @TaskStateID = '' OR [TaskState].[TaskStateID] > LTRIM(RTRIM(@TaskStateID)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] > LTRIM(RTRIM(@TaskStateName)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [TaskState].[TaskStateID]
     ,[TaskState].[TaskStateName]
FROM
     [TaskState]
WHERE
      (@TaskStateID IS NULL OR @TaskStateID = '' OR [TaskState].[TaskStateID] < LTRIM(RTRIM(@TaskStateID)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] < LTRIM(RTRIM(@TaskStateName)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [TaskState].[TaskStateID]
     ,[TaskState].[TaskStateName]
FROM
     [TaskState]
WHERE
      (@TaskStateID IS NULL OR @TaskStateID = '' OR [TaskState].[TaskStateID] >= LTRIM(RTRIM(@TaskStateID)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] >= LTRIM(RTRIM(@TaskStateName)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [TaskState].[TaskStateID]
     ,[TaskState].[TaskStateName]
FROM
     [TaskState]
WHERE
      (@TaskStateID IS NULL OR @TaskStateID = '' OR [TaskState].[TaskStateID] <= LTRIM(RTRIM(@TaskStateID)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] <= LTRIM(RTRIM(@TaskStateName)))

END

END
GO

GRANT EXECUTE ON [TaskStateSearch] TO [Public]
GO

 
