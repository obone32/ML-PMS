--***********************************************************
--SEARCH Stored Procedure for UserTypePermission table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypePermissionSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypePermissionSearch] 
GO

CREATE PROCEDURE [UserTypePermissionSearch] 
      @UserTypeName varchar(-1)
     ,@UserTypePermissionID int
     ,@PermissionName varchar(-1)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [UserTypePermission].[UserTypeID]
     ,[UserTypePermission].[UserTypePermissionID]
     ,[UserTypePermission].[PermissionID]
FROM
     [UserTypePermission]
     INNER JOIN [UserType] ON [UserTypePermission].[UserTypeID] = [UserType].[UserTypeID]
     INNER JOIN [Permission] ON [UserTypePermission].[PermissionID] = [Permission].[PermissionID]
WHERE
      (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] LIKE '%' + LTRIM(RTRIM(@UserTypeName)) + '%')
AND   (@UserTypePermissionID IS NULL OR @UserTypePermissionID = '' OR [UserTypePermission].[UserTypePermissionID] LIKE '%' + LTRIM(RTRIM(@UserTypePermissionID)) + '%')
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] LIKE '%' + LTRIM(RTRIM(@PermissionName)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [UserTypePermission].[UserTypeID]
     ,[UserTypePermission].[UserTypePermissionID]
     ,[UserTypePermission].[PermissionID]
FROM
     [UserTypePermission]
     INNER JOIN [UserType] ON [UserTypePermission].[UserTypeID] = [UserType].[UserTypeID]
     INNER JOIN [Permission] ON [UserTypePermission].[PermissionID] = [Permission].[PermissionID]
WHERE
      (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] = LTRIM(RTRIM(@UserTypeName)))
AND   (@UserTypePermissionID IS NULL OR @UserTypePermissionID = '' OR [UserTypePermission].[UserTypePermissionID] = LTRIM(RTRIM(@UserTypePermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] = LTRIM(RTRIM(@PermissionName)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [UserTypePermission].[UserTypeID]
     ,[UserTypePermission].[UserTypePermissionID]
     ,[UserTypePermission].[PermissionID]
FROM
     [UserTypePermission]
     INNER JOIN [UserType] ON [UserTypePermission].[UserTypeID] = [UserType].[UserTypeID]
     INNER JOIN [Permission] ON [UserTypePermission].[PermissionID] = [Permission].[PermissionID]
WHERE
      (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] LIKE LTRIM(RTRIM(@UserTypeName)) + '%')
AND   (@UserTypePermissionID IS NULL OR @UserTypePermissionID = '' OR [UserTypePermission].[UserTypePermissionID] LIKE LTRIM(RTRIM(@UserTypePermissionID)) + '%')
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] LIKE LTRIM(RTRIM(@PermissionName)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [UserTypePermission].[UserTypeID]
     ,[UserTypePermission].[UserTypePermissionID]
     ,[UserTypePermission].[PermissionID]
FROM
     [UserTypePermission]
     INNER JOIN [UserType] ON [UserTypePermission].[UserTypeID] = [UserType].[UserTypeID]
     INNER JOIN [Permission] ON [UserTypePermission].[PermissionID] = [Permission].[PermissionID]
WHERE
      (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] > LTRIM(RTRIM(@UserTypeName)))
AND   (@UserTypePermissionID IS NULL OR @UserTypePermissionID = '' OR [UserTypePermission].[UserTypePermissionID] > LTRIM(RTRIM(@UserTypePermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] > LTRIM(RTRIM(@PermissionName)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [UserTypePermission].[UserTypeID]
     ,[UserTypePermission].[UserTypePermissionID]
     ,[UserTypePermission].[PermissionID]
FROM
     [UserTypePermission]
     INNER JOIN [UserType] ON [UserTypePermission].[UserTypeID] = [UserType].[UserTypeID]
     INNER JOIN [Permission] ON [UserTypePermission].[PermissionID] = [Permission].[PermissionID]
WHERE
      (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] < LTRIM(RTRIM(@UserTypeName)))
AND   (@UserTypePermissionID IS NULL OR @UserTypePermissionID = '' OR [UserTypePermission].[UserTypePermissionID] < LTRIM(RTRIM(@UserTypePermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] < LTRIM(RTRIM(@PermissionName)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [UserTypePermission].[UserTypeID]
     ,[UserTypePermission].[UserTypePermissionID]
     ,[UserTypePermission].[PermissionID]
FROM
     [UserTypePermission]
     INNER JOIN [UserType] ON [UserTypePermission].[UserTypeID] = [UserType].[UserTypeID]
     INNER JOIN [Permission] ON [UserTypePermission].[PermissionID] = [Permission].[PermissionID]
WHERE
      (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] >= LTRIM(RTRIM(@UserTypeName)))
AND   (@UserTypePermissionID IS NULL OR @UserTypePermissionID = '' OR [UserTypePermission].[UserTypePermissionID] >= LTRIM(RTRIM(@UserTypePermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] >= LTRIM(RTRIM(@PermissionName)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [UserTypePermission].[UserTypeID]
     ,[UserTypePermission].[UserTypePermissionID]
     ,[UserTypePermission].[PermissionID]
FROM
     [UserTypePermission]
     INNER JOIN [UserType] ON [UserTypePermission].[UserTypeID] = [UserType].[UserTypeID]
     INNER JOIN [Permission] ON [UserTypePermission].[PermissionID] = [Permission].[PermissionID]
WHERE
      (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] <= LTRIM(RTRIM(@UserTypeName)))
AND   (@UserTypePermissionID IS NULL OR @UserTypePermissionID = '' OR [UserTypePermission].[UserTypePermissionID] <= LTRIM(RTRIM(@UserTypePermissionID)))
AND   (@PermissionName IS NULL OR @PermissionName = '' OR [Permission].[PermissionName] <= LTRIM(RTRIM(@PermissionName)))

END

END
GO

GRANT EXECUTE ON [UserTypePermissionSearch] TO [Public]
GO

 
