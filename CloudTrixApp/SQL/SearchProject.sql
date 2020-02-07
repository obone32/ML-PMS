--***********************************************************
--SEARCH Stored Procedure for Project table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectSearch] 
GO

CREATE PROCEDURE [ProjectSearch] 
      @ProjectID int
     ,@ProjectName varchar(8000)
     ,@BillingName varchar(8000)
     ,@Description varchar(8000)
     ,@Location varchar(8000)
     ,@StartDate datetime
     ,@EndDate datetime
     ,@ProjectStatusName varchar(-1)
     ,@ClientName varchar(-1)
     ,@ArchitectName varchar(-1)
     ,@CompanyName varchar(-1)
     ,@AddUserID int
     ,@AddDate datetime
     ,@ArchiveUserID int
     ,@ArchiveDate datetime
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [Project].[ProjectID]
     ,[Project].[ProjectName]
     ,[Project].[BillingName]
     ,[Project].[Description]
     ,[Project].[Location]
     ,[Project].[StartDate]
     ,[Project].[EndDate]
     ,[Project].[ProjectStatusID]
     ,[Project].[ClientID]
     ,[Project].[ArchitectID]
     ,[Project].[CompanyID]
     ,[Project].[AddUserID]
     ,[Project].[AddDate]
     ,[Project].[ArchiveUserID]
     ,[Project].[ArchiveDate]
FROM
     [Project]
     INNER JOIN [ProjectStatus] ON [Project].[ProjectStatusID] = [ProjectStatus].[ProjectStatusID]
     INNER JOIN [Client] ON [Project].[ClientID] = [Client].[ClientID]
     INNER JOIN [Architect] ON [Project].[ArchitectID] = [Architect].[ArchitectID]
     INNER JOIN [Company] ON [Project].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ProjectID IS NULL OR @ProjectID = '' OR [Project].[ProjectID] LIKE '%' + LTRIM(RTRIM(@ProjectID)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE '%' + LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@BillingName IS NULL OR @BillingName = '' OR [Project].[BillingName] LIKE '%' + LTRIM(RTRIM(@BillingName)) + '%')
AND   (@Description IS NULL OR @Description = '' OR [Project].[Description] LIKE '%' + LTRIM(RTRIM(@Description)) + '%')
AND   (@Location IS NULL OR @Location = '' OR [Project].[Location] LIKE '%' + LTRIM(RTRIM(@Location)) + '%')
AND   (@StartDate IS NULL OR @StartDate = '' OR [Project].[StartDate] LIKE '%' + LTRIM(RTRIM(@StartDate)) + '%')
AND   (@EndDate IS NULL OR @EndDate = '' OR [Project].[EndDate] LIKE '%' + LTRIM(RTRIM(@EndDate)) + '%')
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] LIKE '%' + LTRIM(RTRIM(@ProjectStatusName)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] LIKE '%' + LTRIM(RTRIM(@ClientName)) + '%')
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] LIKE '%' + LTRIM(RTRIM(@ArchitectName)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE '%' + LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Project].[AddUserID] LIKE '%' + LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Project].[AddDate] LIKE '%' + LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Project].[ArchiveUserID] LIKE '%' + LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Project].[ArchiveDate] LIKE '%' + LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Project].[ProjectID]
     ,[Project].[ProjectName]
     ,[Project].[BillingName]
     ,[Project].[Description]
     ,[Project].[Location]
     ,[Project].[StartDate]
     ,[Project].[EndDate]
     ,[Project].[ProjectStatusID]
     ,[Project].[ClientID]
     ,[Project].[ArchitectID]
     ,[Project].[CompanyID]
     ,[Project].[AddUserID]
     ,[Project].[AddDate]
     ,[Project].[ArchiveUserID]
     ,[Project].[ArchiveDate]
FROM
     [Project]
     INNER JOIN [ProjectStatus] ON [Project].[ProjectStatusID] = [ProjectStatus].[ProjectStatusID]
     INNER JOIN [Client] ON [Project].[ClientID] = [Client].[ClientID]
     INNER JOIN [Architect] ON [Project].[ArchitectID] = [Architect].[ArchitectID]
     INNER JOIN [Company] ON [Project].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ProjectID IS NULL OR @ProjectID = '' OR [Project].[ProjectID] = LTRIM(RTRIM(@ProjectID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] = LTRIM(RTRIM(@ProjectName)))
AND   (@BillingName IS NULL OR @BillingName = '' OR [Project].[BillingName] = LTRIM(RTRIM(@BillingName)))
AND   (@Description IS NULL OR @Description = '' OR [Project].[Description] = LTRIM(RTRIM(@Description)))
AND   (@Location IS NULL OR @Location = '' OR [Project].[Location] = LTRIM(RTRIM(@Location)))
AND   (@StartDate IS NULL OR @StartDate = '' OR [Project].[StartDate] = LTRIM(RTRIM(@StartDate)))
AND   (@EndDate IS NULL OR @EndDate = '' OR [Project].[EndDate] = LTRIM(RTRIM(@EndDate)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] = LTRIM(RTRIM(@ProjectStatusName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] = LTRIM(RTRIM(@ClientName)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] = LTRIM(RTRIM(@ArchitectName)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] = LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Project].[AddUserID] = LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Project].[AddDate] = LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Project].[ArchiveUserID] = LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Project].[ArchiveDate] = LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Project].[ProjectID]
     ,[Project].[ProjectName]
     ,[Project].[BillingName]
     ,[Project].[Description]
     ,[Project].[Location]
     ,[Project].[StartDate]
     ,[Project].[EndDate]
     ,[Project].[ProjectStatusID]
     ,[Project].[ClientID]
     ,[Project].[ArchitectID]
     ,[Project].[CompanyID]
     ,[Project].[AddUserID]
     ,[Project].[AddDate]
     ,[Project].[ArchiveUserID]
     ,[Project].[ArchiveDate]
FROM
     [Project]
     INNER JOIN [ProjectStatus] ON [Project].[ProjectStatusID] = [ProjectStatus].[ProjectStatusID]
     INNER JOIN [Client] ON [Project].[ClientID] = [Client].[ClientID]
     INNER JOIN [Architect] ON [Project].[ArchitectID] = [Architect].[ArchitectID]
     INNER JOIN [Company] ON [Project].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ProjectID IS NULL OR @ProjectID = '' OR [Project].[ProjectID] LIKE LTRIM(RTRIM(@ProjectID)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@BillingName IS NULL OR @BillingName = '' OR [Project].[BillingName] LIKE LTRIM(RTRIM(@BillingName)) + '%')
AND   (@Description IS NULL OR @Description = '' OR [Project].[Description] LIKE LTRIM(RTRIM(@Description)) + '%')
AND   (@Location IS NULL OR @Location = '' OR [Project].[Location] LIKE LTRIM(RTRIM(@Location)) + '%')
AND   (@StartDate IS NULL OR @StartDate = '' OR [Project].[StartDate] LIKE LTRIM(RTRIM(@StartDate)) + '%')
AND   (@EndDate IS NULL OR @EndDate = '' OR [Project].[EndDate] LIKE LTRIM(RTRIM(@EndDate)) + '%')
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] LIKE LTRIM(RTRIM(@ProjectStatusName)) + '%')
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] LIKE LTRIM(RTRIM(@ClientName)) + '%')
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] LIKE LTRIM(RTRIM(@ArchitectName)) + '%')
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] LIKE LTRIM(RTRIM(@CompanyName)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Project].[AddUserID] LIKE LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [Project].[AddDate] LIKE LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Project].[ArchiveUserID] LIKE LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Project].[ArchiveDate] LIKE LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Project].[ProjectID]
     ,[Project].[ProjectName]
     ,[Project].[BillingName]
     ,[Project].[Description]
     ,[Project].[Location]
     ,[Project].[StartDate]
     ,[Project].[EndDate]
     ,[Project].[ProjectStatusID]
     ,[Project].[ClientID]
     ,[Project].[ArchitectID]
     ,[Project].[CompanyID]
     ,[Project].[AddUserID]
     ,[Project].[AddDate]
     ,[Project].[ArchiveUserID]
     ,[Project].[ArchiveDate]
FROM
     [Project]
     INNER JOIN [ProjectStatus] ON [Project].[ProjectStatusID] = [ProjectStatus].[ProjectStatusID]
     INNER JOIN [Client] ON [Project].[ClientID] = [Client].[ClientID]
     INNER JOIN [Architect] ON [Project].[ArchitectID] = [Architect].[ArchitectID]
     INNER JOIN [Company] ON [Project].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ProjectID IS NULL OR @ProjectID = '' OR [Project].[ProjectID] > LTRIM(RTRIM(@ProjectID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] > LTRIM(RTRIM(@ProjectName)))
AND   (@BillingName IS NULL OR @BillingName = '' OR [Project].[BillingName] > LTRIM(RTRIM(@BillingName)))
AND   (@Description IS NULL OR @Description = '' OR [Project].[Description] > LTRIM(RTRIM(@Description)))
AND   (@Location IS NULL OR @Location = '' OR [Project].[Location] > LTRIM(RTRIM(@Location)))
AND   (@StartDate IS NULL OR @StartDate = '' OR [Project].[StartDate] > LTRIM(RTRIM(@StartDate)))
AND   (@EndDate IS NULL OR @EndDate = '' OR [Project].[EndDate] > LTRIM(RTRIM(@EndDate)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] > LTRIM(RTRIM(@ProjectStatusName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] > LTRIM(RTRIM(@ClientName)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] > LTRIM(RTRIM(@ArchitectName)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] > LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Project].[AddUserID] > LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Project].[AddDate] > LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Project].[ArchiveUserID] > LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Project].[ArchiveDate] > LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Project].[ProjectID]
     ,[Project].[ProjectName]
     ,[Project].[BillingName]
     ,[Project].[Description]
     ,[Project].[Location]
     ,[Project].[StartDate]
     ,[Project].[EndDate]
     ,[Project].[ProjectStatusID]
     ,[Project].[ClientID]
     ,[Project].[ArchitectID]
     ,[Project].[CompanyID]
     ,[Project].[AddUserID]
     ,[Project].[AddDate]
     ,[Project].[ArchiveUserID]
     ,[Project].[ArchiveDate]
FROM
     [Project]
     INNER JOIN [ProjectStatus] ON [Project].[ProjectStatusID] = [ProjectStatus].[ProjectStatusID]
     INNER JOIN [Client] ON [Project].[ClientID] = [Client].[ClientID]
     INNER JOIN [Architect] ON [Project].[ArchitectID] = [Architect].[ArchitectID]
     INNER JOIN [Company] ON [Project].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ProjectID IS NULL OR @ProjectID = '' OR [Project].[ProjectID] < LTRIM(RTRIM(@ProjectID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] < LTRIM(RTRIM(@ProjectName)))
AND   (@BillingName IS NULL OR @BillingName = '' OR [Project].[BillingName] < LTRIM(RTRIM(@BillingName)))
AND   (@Description IS NULL OR @Description = '' OR [Project].[Description] < LTRIM(RTRIM(@Description)))
AND   (@Location IS NULL OR @Location = '' OR [Project].[Location] < LTRIM(RTRIM(@Location)))
AND   (@StartDate IS NULL OR @StartDate = '' OR [Project].[StartDate] < LTRIM(RTRIM(@StartDate)))
AND   (@EndDate IS NULL OR @EndDate = '' OR [Project].[EndDate] < LTRIM(RTRIM(@EndDate)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] < LTRIM(RTRIM(@ProjectStatusName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] < LTRIM(RTRIM(@ClientName)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] < LTRIM(RTRIM(@ArchitectName)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] < LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Project].[AddUserID] < LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Project].[AddDate] < LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Project].[ArchiveUserID] < LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Project].[ArchiveDate] < LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Project].[ProjectID]
     ,[Project].[ProjectName]
     ,[Project].[BillingName]
     ,[Project].[Description]
     ,[Project].[Location]
     ,[Project].[StartDate]
     ,[Project].[EndDate]
     ,[Project].[ProjectStatusID]
     ,[Project].[ClientID]
     ,[Project].[ArchitectID]
     ,[Project].[CompanyID]
     ,[Project].[AddUserID]
     ,[Project].[AddDate]
     ,[Project].[ArchiveUserID]
     ,[Project].[ArchiveDate]
FROM
     [Project]
     INNER JOIN [ProjectStatus] ON [Project].[ProjectStatusID] = [ProjectStatus].[ProjectStatusID]
     INNER JOIN [Client] ON [Project].[ClientID] = [Client].[ClientID]
     INNER JOIN [Architect] ON [Project].[ArchitectID] = [Architect].[ArchitectID]
     INNER JOIN [Company] ON [Project].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ProjectID IS NULL OR @ProjectID = '' OR [Project].[ProjectID] >= LTRIM(RTRIM(@ProjectID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] >= LTRIM(RTRIM(@ProjectName)))
AND   (@BillingName IS NULL OR @BillingName = '' OR [Project].[BillingName] >= LTRIM(RTRIM(@BillingName)))
AND   (@Description IS NULL OR @Description = '' OR [Project].[Description] >= LTRIM(RTRIM(@Description)))
AND   (@Location IS NULL OR @Location = '' OR [Project].[Location] >= LTRIM(RTRIM(@Location)))
AND   (@StartDate IS NULL OR @StartDate = '' OR [Project].[StartDate] >= LTRIM(RTRIM(@StartDate)))
AND   (@EndDate IS NULL OR @EndDate = '' OR [Project].[EndDate] >= LTRIM(RTRIM(@EndDate)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] >= LTRIM(RTRIM(@ProjectStatusName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] >= LTRIM(RTRIM(@ClientName)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] >= LTRIM(RTRIM(@ArchitectName)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] >= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Project].[AddUserID] >= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Project].[AddDate] >= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Project].[ArchiveUserID] >= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Project].[ArchiveDate] >= LTRIM(RTRIM(@ArchiveDate)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Project].[ProjectID]
     ,[Project].[ProjectName]
     ,[Project].[BillingName]
     ,[Project].[Description]
     ,[Project].[Location]
     ,[Project].[StartDate]
     ,[Project].[EndDate]
     ,[Project].[ProjectStatusID]
     ,[Project].[ClientID]
     ,[Project].[ArchitectID]
     ,[Project].[CompanyID]
     ,[Project].[AddUserID]
     ,[Project].[AddDate]
     ,[Project].[ArchiveUserID]
     ,[Project].[ArchiveDate]
FROM
     [Project]
     INNER JOIN [ProjectStatus] ON [Project].[ProjectStatusID] = [ProjectStatus].[ProjectStatusID]
     INNER JOIN [Client] ON [Project].[ClientID] = [Client].[ClientID]
     INNER JOIN [Architect] ON [Project].[ArchitectID] = [Architect].[ArchitectID]
     INNER JOIN [Company] ON [Project].[CompanyID] = [Company].[CompanyID]
WHERE
      (@ProjectID IS NULL OR @ProjectID = '' OR [Project].[ProjectID] <= LTRIM(RTRIM(@ProjectID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] <= LTRIM(RTRIM(@ProjectName)))
AND   (@BillingName IS NULL OR @BillingName = '' OR [Project].[BillingName] <= LTRIM(RTRIM(@BillingName)))
AND   (@Description IS NULL OR @Description = '' OR [Project].[Description] <= LTRIM(RTRIM(@Description)))
AND   (@Location IS NULL OR @Location = '' OR [Project].[Location] <= LTRIM(RTRIM(@Location)))
AND   (@StartDate IS NULL OR @StartDate = '' OR [Project].[StartDate] <= LTRIM(RTRIM(@StartDate)))
AND   (@EndDate IS NULL OR @EndDate = '' OR [Project].[EndDate] <= LTRIM(RTRIM(@EndDate)))
AND   (@ProjectStatusName IS NULL OR @ProjectStatusName = '' OR [ProjectStatus].[ProjectStatusName] <= LTRIM(RTRIM(@ProjectStatusName)))
AND   (@ClientName IS NULL OR @ClientName = '' OR [Client].[ClientName] <= LTRIM(RTRIM(@ClientName)))
AND   (@ArchitectName IS NULL OR @ArchitectName = '' OR [Architect].[ArchitectName] <= LTRIM(RTRIM(@ArchitectName)))
AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Company].[CompanyName] <= LTRIM(RTRIM(@CompanyName)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [Project].[AddUserID] <= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [Project].[AddDate] <= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [Project].[ArchiveUserID] <= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [Project].[ArchiveDate] <= LTRIM(RTRIM(@ArchiveDate)))

END

END
GO

GRANT EXECUTE ON [ProjectSearch] TO [Public]
GO

 
