--***********************************************************
--SEARCH Stored Procedure for ProjectStatus table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectStatusSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectStatusSearch] 
GO

CREATE PROCEDURE [ProjectStatusSearch] 
      @ProjectStatusID int
     ,@ProjectStatusName varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [ProjectStatus].[ProjectStatusID]
     ,[ProjectStatus].[ProjectStatusName]
FROM
     [ProjectStatus]
WHERE
      (@ProjectStatusID IS NULL OR @ProjectStatusID = '' OR [ProjectStatus].[ProjectStatusID] LIKE '%' + LTRIM(RTRIM(@ProjectStatusID)) + '%')
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] LIKE '%' + LTRIM(RTRIM(@ProjectStatusName)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [ProjectStatus].[ProjectStatusID]
     ,[ProjectStatus].[ProjectStatusName]
FROM
     [ProjectStatus]
WHERE
      (@ProjectStatusID IS NULL OR @ProjectStatusID = '' OR [ProjectStatus].[ProjectStatusID] = LTRIM(RTRIM(@ProjectStatusID)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] = LTRIM(RTRIM(@ProjectStatusName)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [ProjectStatus].[ProjectStatusID]
     ,[ProjectStatus].[ProjectStatusName]
FROM
     [ProjectStatus]
WHERE
      (@ProjectStatusID IS NULL OR @ProjectStatusID = '' OR [ProjectStatus].[ProjectStatusID] LIKE LTRIM(RTRIM(@ProjectStatusID)) + '%')
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] LIKE LTRIM(RTRIM(@ProjectStatusName)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [ProjectStatus].[ProjectStatusID]
     ,[ProjectStatus].[ProjectStatusName]
FROM
     [ProjectStatus]
WHERE
      (@ProjectStatusID IS NULL OR @ProjectStatusID = '' OR [ProjectStatus].[ProjectStatusID] > LTRIM(RTRIM(@ProjectStatusID)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] > LTRIM(RTRIM(@ProjectStatusName)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [ProjectStatus].[ProjectStatusID]
     ,[ProjectStatus].[ProjectStatusName]
FROM
     [ProjectStatus]
WHERE
      (@ProjectStatusID IS NULL OR @ProjectStatusID = '' OR [ProjectStatus].[ProjectStatusID] < LTRIM(RTRIM(@ProjectStatusID)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] < LTRIM(RTRIM(@ProjectStatusName)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [ProjectStatus].[ProjectStatusID]
     ,[ProjectStatus].[ProjectStatusName]
FROM
     [ProjectStatus]
WHERE
      (@ProjectStatusID IS NULL OR @ProjectStatusID = '' OR [ProjectStatus].[ProjectStatusID] >= LTRIM(RTRIM(@ProjectStatusID)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] >= LTRIM(RTRIM(@ProjectStatusName)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [ProjectStatus].[ProjectStatusID]
     ,[ProjectStatus].[ProjectStatusName]
FROM
     [ProjectStatus]
WHERE
      (@ProjectStatusID IS NULL OR @ProjectStatusID = '' OR [ProjectStatus].[ProjectStatusID] <= LTRIM(RTRIM(@ProjectStatusID)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] <= LTRIM(RTRIM(@ProjectStatusName)))

END

END
GO

GRANT EXECUTE ON [ProjectStatusSearch] TO [Public]
GO

 
