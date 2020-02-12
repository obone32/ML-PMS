--***********************************************************
--SEARCH Stored Procedure for ArchitectAssociate table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectAssociateSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectAssociateSearch] 
GO

CREATE PROCEDURE [ArchitectAssociateSearch] 
      @ArchitectName varchar(-1)
     ,@ArchitectAssociateID int
     ,@AssociateName varchar(8000)
     ,@ContactNo varchar(8000)
     ,@EMail varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [ArchitectAssociate].[ArchitectID]
     ,[ArchitectAssociate].[ArchitectAssociateID]
     ,[ArchitectAssociate].[AssociateName]
     ,[ArchitectAssociate].[ContactNo]
     ,[ArchitectAssociate].[EMail]
FROM
     [ArchitectAssociate]
     INNER JOIN [Architect] ON [ArchitectAssociate].[ArchitectID] = [Architect].[ArchitectID]
WHERE
      (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] LIKE '%' + LTRIM(RTRIM(@ArchitectName)) + '%')
AND   (@ArchitectAssociateID IS NULL OR @ArchitectAssociateID = '' OR [ArchitectAssociate].[ArchitectAssociateID] LIKE '%' + LTRIM(RTRIM(@ArchitectAssociateID)) + '%')
AND   (@AssociateName IS NULL OR @AssociateName = '' OR [ArchitectAssociate].[AssociateName] LIKE '%' + LTRIM(RTRIM(@AssociateName)) + '%')
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [ArchitectAssociate].[ContactNo] LIKE '%' + LTRIM(RTRIM(@ContactNo)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [ArchitectAssociate].[EMail] LIKE '%' + LTRIM(RTRIM(@EMail)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [ArchitectAssociate].[ArchitectID]
     ,[ArchitectAssociate].[ArchitectAssociateID]
     ,[ArchitectAssociate].[AssociateName]
     ,[ArchitectAssociate].[ContactNo]
     ,[ArchitectAssociate].[EMail]
FROM
     [ArchitectAssociate]
     INNER JOIN [Architect] ON [ArchitectAssociate].[ArchitectID] = [Architect].[ArchitectID]
WHERE
      (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] = LTRIM(RTRIM(@ArchitectName)))
AND   (@ArchitectAssociateID IS NULL OR @ArchitectAssociateID = '' OR [ArchitectAssociate].[ArchitectAssociateID] = LTRIM(RTRIM(@ArchitectAssociateID)))
AND   (@AssociateName IS NULL OR @AssociateName = '' OR [ArchitectAssociate].[AssociateName] = LTRIM(RTRIM(@AssociateName)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [ArchitectAssociate].[ContactNo] = LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [ArchitectAssociate].[EMail] = LTRIM(RTRIM(@EMail)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [ArchitectAssociate].[ArchitectID]
     ,[ArchitectAssociate].[ArchitectAssociateID]
     ,[ArchitectAssociate].[AssociateName]
     ,[ArchitectAssociate].[ContactNo]
     ,[ArchitectAssociate].[EMail]
FROM
     [ArchitectAssociate]
     INNER JOIN [Architect] ON [ArchitectAssociate].[ArchitectID] = [Architect].[ArchitectID]
WHERE
      (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] LIKE LTRIM(RTRIM(@ArchitectName)) + '%')
AND   (@ArchitectAssociateID IS NULL OR @ArchitectAssociateID = '' OR [ArchitectAssociate].[ArchitectAssociateID] LIKE LTRIM(RTRIM(@ArchitectAssociateID)) + '%')
AND   (@AssociateName IS NULL OR @AssociateName = '' OR [ArchitectAssociate].[AssociateName] LIKE LTRIM(RTRIM(@AssociateName)) + '%')
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [ArchitectAssociate].[ContactNo] LIKE LTRIM(RTRIM(@ContactNo)) + '%')
AND   (@EMail IS NULL OR @EMail = '' OR [ArchitectAssociate].[EMail] LIKE LTRIM(RTRIM(@EMail)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [ArchitectAssociate].[ArchitectID]
     ,[ArchitectAssociate].[ArchitectAssociateID]
     ,[ArchitectAssociate].[AssociateName]
     ,[ArchitectAssociate].[ContactNo]
     ,[ArchitectAssociate].[EMail]
FROM
     [ArchitectAssociate]
     INNER JOIN [Architect] ON [ArchitectAssociate].[ArchitectID] = [Architect].[ArchitectID]
WHERE
      (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] > LTRIM(RTRIM(@ArchitectName)))
AND   (@ArchitectAssociateID IS NULL OR @ArchitectAssociateID = '' OR [ArchitectAssociate].[ArchitectAssociateID] > LTRIM(RTRIM(@ArchitectAssociateID)))
AND   (@AssociateName IS NULL OR @AssociateName = '' OR [ArchitectAssociate].[AssociateName] > LTRIM(RTRIM(@AssociateName)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [ArchitectAssociate].[ContactNo] > LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [ArchitectAssociate].[EMail] > LTRIM(RTRIM(@EMail)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [ArchitectAssociate].[ArchitectID]
     ,[ArchitectAssociate].[ArchitectAssociateID]
     ,[ArchitectAssociate].[AssociateName]
     ,[ArchitectAssociate].[ContactNo]
     ,[ArchitectAssociate].[EMail]
FROM
     [ArchitectAssociate]
     INNER JOIN [Architect] ON [ArchitectAssociate].[ArchitectID] = [Architect].[ArchitectID]
WHERE
      (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] < LTRIM(RTRIM(@ArchitectName)))
AND   (@ArchitectAssociateID IS NULL OR @ArchitectAssociateID = '' OR [ArchitectAssociate].[ArchitectAssociateID] < LTRIM(RTRIM(@ArchitectAssociateID)))
AND   (@AssociateName IS NULL OR @AssociateName = '' OR [ArchitectAssociate].[AssociateName] < LTRIM(RTRIM(@AssociateName)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [ArchitectAssociate].[ContactNo] < LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [ArchitectAssociate].[EMail] < LTRIM(RTRIM(@EMail)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [ArchitectAssociate].[ArchitectID]
     ,[ArchitectAssociate].[ArchitectAssociateID]
     ,[ArchitectAssociate].[AssociateName]
     ,[ArchitectAssociate].[ContactNo]
     ,[ArchitectAssociate].[EMail]
FROM
     [ArchitectAssociate]
     INNER JOIN [Architect] ON [ArchitectAssociate].[ArchitectID] = [Architect].[ArchitectID]
WHERE
      (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] >= LTRIM(RTRIM(@ArchitectName)))
AND   (@ArchitectAssociateID IS NULL OR @ArchitectAssociateID = '' OR [ArchitectAssociate].[ArchitectAssociateID] >= LTRIM(RTRIM(@ArchitectAssociateID)))
AND   (@AssociateName IS NULL OR @AssociateName = '' OR [ArchitectAssociate].[AssociateName] >= LTRIM(RTRIM(@AssociateName)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [ArchitectAssociate].[ContactNo] >= LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [ArchitectAssociate].[EMail] >= LTRIM(RTRIM(@EMail)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [ArchitectAssociate].[ArchitectID]
     ,[ArchitectAssociate].[ArchitectAssociateID]
     ,[ArchitectAssociate].[AssociateName]
     ,[ArchitectAssociate].[ContactNo]
     ,[ArchitectAssociate].[EMail]
FROM
     [ArchitectAssociate]
     INNER JOIN [Architect] ON [ArchitectAssociate].[ArchitectID] = [Architect].[ArchitectID]
WHERE
      (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] <= LTRIM(RTRIM(@ArchitectName)))
AND   (@ArchitectAssociateID IS NULL OR @ArchitectAssociateID = '' OR [ArchitectAssociate].[ArchitectAssociateID] <= LTRIM(RTRIM(@ArchitectAssociateID)))
AND   (@AssociateName IS NULL OR @AssociateName = '' OR [ArchitectAssociate].[AssociateName] <= LTRIM(RTRIM(@AssociateName)))
AND   (@ContactNo IS NULL OR @ContactNo = '' OR [ArchitectAssociate].[ContactNo] <= LTRIM(RTRIM(@ContactNo)))
AND   (@EMail IS NULL OR @EMail = '' OR [ArchitectAssociate].[EMail] <= LTRIM(RTRIM(@EMail)))

END

END
GO

GRANT EXECUTE ON [ArchitectAssociateSearch] TO [Public]
GO

 
