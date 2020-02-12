--***********************************************************
--SEARCH Stored Procedure for Permission table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'PermissionSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [PermissionSearch] 
GO

CREATE PROCEDURE [PermissionSearch] 
      @PermissionID int
     ,@PermissionName varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [Permission].[PermissionID]
     ,[Permission].[PermissionName]
FROM
     [Permission]
WHERE
      (@PermissionID IS NULL OR @PermissionID = '' OR [Permission].[PermissionID] LIKE '%' + LTRIM(RTRIM(@PermissionID)) + '%')
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] LIKE '%' + LTRIM(RTRIM(@PermissionName)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Permission].[PermissionID]
     ,[Permission].[PermissionName]
FROM
     [Permission]
WHERE
      (@PermissionID IS NULL OR @PermissionID = '' OR [Permission].[PermissionID] = LTRIM(RTRIM(@PermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] = LTRIM(RTRIM(@PermissionName)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Permission].[PermissionID]
     ,[Permission].[PermissionName]
FROM
     [Permission]
WHERE
      (@PermissionID IS NULL OR @PermissionID = '' OR [Permission].[PermissionID] LIKE LTRIM(RTRIM(@PermissionID)) + '%')
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] LIKE LTRIM(RTRIM(@PermissionName)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Permission].[PermissionID]
     ,[Permission].[PermissionName]
FROM
     [Permission]
WHERE
      (@PermissionID IS NULL OR @PermissionID = '' OR [Permission].[PermissionID] > LTRIM(RTRIM(@PermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] > LTRIM(RTRIM(@PermissionName)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Permission].[PermissionID]
     ,[Permission].[PermissionName]
FROM
     [Permission]
WHERE
      (@PermissionID IS NULL OR @PermissionID = '' OR [Permission].[PermissionID] < LTRIM(RTRIM(@PermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] < LTRIM(RTRIM(@PermissionName)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Permission].[PermissionID]
     ,[Permission].[PermissionName]
FROM
     [Permission]
WHERE
      (@PermissionID IS NULL OR @PermissionID = '' OR [Permission].[PermissionID] >= LTRIM(RTRIM(@PermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] >= LTRIM(RTRIM(@PermissionName)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Permission].[PermissionID]
     ,[Permission].[PermissionName]
FROM
     [Permission]
WHERE
      (@PermissionID IS NULL OR @PermissionID = '' OR [Permission].[PermissionID] <= LTRIM(RTRIM(@PermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] <= LTRIM(RTRIM(@PermissionName)))

END

END
GO

GRANT EXECUTE ON [PermissionSearch] TO [Public]
GO

 
