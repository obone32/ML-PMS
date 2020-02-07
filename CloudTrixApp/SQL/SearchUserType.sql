--***********************************************************
--SEARCH Stored Procedure for UserType table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypeSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypeSearch] 
GO

CREATE PROCEDURE [UserTypeSearch] 
      @UserTypeID int
     ,@UserTypeName varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [UserType].[UserTypeID]
     ,[UserType].[UserTypeName]
FROM
     [UserType]
WHERE
      (@UserTypeID IS NULL OR @UserTypeID = '' OR [UserType].[UserTypeID] LIKE '%' + LTRIM(RTRIM(@UserTypeID)) + '%')
AND   (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] LIKE '%' + LTRIM(RTRIM(@UserTypeName)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [UserType].[UserTypeID]
     ,[UserType].[UserTypeName]
FROM
     [UserType]
WHERE
      (@UserTypeID IS NULL OR @UserTypeID = '' OR [UserType].[UserTypeID] = LTRIM(RTRIM(@UserTypeID)))
AND   (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] = LTRIM(RTRIM(@UserTypeName)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [UserType].[UserTypeID]
     ,[UserType].[UserTypeName]
FROM
     [UserType]
WHERE
      (@UserTypeID IS NULL OR @UserTypeID = '' OR [UserType].[UserTypeID] LIKE LTRIM(RTRIM(@UserTypeID)) + '%')
AND   (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] LIKE LTRIM(RTRIM(@UserTypeName)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [UserType].[UserTypeID]
     ,[UserType].[UserTypeName]
FROM
     [UserType]
WHERE
      (@UserTypeID IS NULL OR @UserTypeID = '' OR [UserType].[UserTypeID] > LTRIM(RTRIM(@UserTypeID)))
AND   (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] > LTRIM(RTRIM(@UserTypeName)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [UserType].[UserTypeID]
     ,[UserType].[UserTypeName]
FROM
     [UserType]
WHERE
      (@UserTypeID IS NULL OR @UserTypeID = '' OR [UserType].[UserTypeID] < LTRIM(RTRIM(@UserTypeID)))
AND   (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] < LTRIM(RTRIM(@UserTypeName)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [UserType].[UserTypeID]
     ,[UserType].[UserTypeName]
FROM
     [UserType]
WHERE
      (@UserTypeID IS NULL OR @UserTypeID = '' OR [UserType].[UserTypeID] >= LTRIM(RTRIM(@UserTypeID)))
AND   (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] >= LTRIM(RTRIM(@UserTypeName)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [UserType].[UserTypeID]
     ,[UserType].[UserTypeName]
FROM
     [UserType]
WHERE
      (@UserTypeID IS NULL OR @UserTypeID = '' OR [UserType].[UserTypeID] <= LTRIM(RTRIM(@UserTypeID)))
AND   (@UserTypeName IS NULL OR @UserTypeName = '' OR [UserType].[UserTypeName] <= LTRIM(RTRIM(@UserTypeName)))

END

END
GO

GRANT EXECUTE ON [UserTypeSearch] TO [Public]
GO

 
