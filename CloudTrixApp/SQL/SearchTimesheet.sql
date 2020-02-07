--***********************************************************
--SEARCH Stored Procedure for Timesheet table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TimesheetSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [TimesheetSearch] 
GO

CREATE PROCEDURE [TimesheetSearch] 
      @TimesheetID int
     ,@FirstName varchar(-1)
     ,@ProjectName varchar(-1)
     ,@StartTime datetime
     ,@EndTime datetime
     ,@Remarks varchar(8000)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [Timesheet].[TimesheetID]
     ,[Timesheet].[EmployeeID]
     ,[Timesheet].[ProjectID]
     ,[Timesheet].[StartTime]
     ,[Timesheet].[EndTime]
     ,[Timesheet].[Remarks]
FROM
     [Timesheet]
     INNER JOIN [Employee] ON [Timesheet].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [Project] ON [Timesheet].[ProjectID] = [Project].[ProjectID]
WHERE
      (@TimesheetID IS NULL OR @TimesheetID = '' OR [Timesheet].[TimesheetID] LIKE '%' + LTRIM(RTRIM(@TimesheetID)) + '%')
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] LIKE '%' + LTRIM(RTRIM(@FirstName)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE '%' + LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@StartTime IS NULL OR @StartTime = '' OR [Timesheet].[StartTime] LIKE '%' + LTRIM(RTRIM(@StartTime)) + '%')
AND   (@EndTime IS NULL OR @EndTime = '' OR [Timesheet].[EndTime] LIKE '%' + LTRIM(RTRIM(@EndTime)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [Timesheet].[Remarks] LIKE '%' + LTRIM(RTRIM(@Remarks)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Timesheet].[TimesheetID]
     ,[Timesheet].[EmployeeID]
     ,[Timesheet].[ProjectID]
     ,[Timesheet].[StartTime]
     ,[Timesheet].[EndTime]
     ,[Timesheet].[Remarks]
FROM
     [Timesheet]
     INNER JOIN [Employee] ON [Timesheet].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [Project] ON [Timesheet].[ProjectID] = [Project].[ProjectID]
WHERE
      (@TimesheetID IS NULL OR @TimesheetID = '' OR [Timesheet].[TimesheetID] = LTRIM(RTRIM(@TimesheetID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] = LTRIM(RTRIM(@FirstName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] = LTRIM(RTRIM(@ProjectName)))
AND   (@StartTime IS NULL OR @StartTime = '' OR [Timesheet].[StartTime] = LTRIM(RTRIM(@StartTime)))
AND   (@EndTime IS NULL OR @EndTime = '' OR [Timesheet].[EndTime] = LTRIM(RTRIM(@EndTime)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Timesheet].[Remarks] = LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Timesheet].[TimesheetID]
     ,[Timesheet].[EmployeeID]
     ,[Timesheet].[ProjectID]
     ,[Timesheet].[StartTime]
     ,[Timesheet].[EndTime]
     ,[Timesheet].[Remarks]
FROM
     [Timesheet]
     INNER JOIN [Employee] ON [Timesheet].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [Project] ON [Timesheet].[ProjectID] = [Project].[ProjectID]
WHERE
      (@TimesheetID IS NULL OR @TimesheetID = '' OR [Timesheet].[TimesheetID] LIKE LTRIM(RTRIM(@TimesheetID)) + '%')
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] LIKE LTRIM(RTRIM(@FirstName)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@StartTime IS NULL OR @StartTime = '' OR [Timesheet].[StartTime] LIKE LTRIM(RTRIM(@StartTime)) + '%')
AND   (@EndTime IS NULL OR @EndTime = '' OR [Timesheet].[EndTime] LIKE LTRIM(RTRIM(@EndTime)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [Timesheet].[Remarks] LIKE LTRIM(RTRIM(@Remarks)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Timesheet].[TimesheetID]
     ,[Timesheet].[EmployeeID]
     ,[Timesheet].[ProjectID]
     ,[Timesheet].[StartTime]
     ,[Timesheet].[EndTime]
     ,[Timesheet].[Remarks]
FROM
     [Timesheet]
     INNER JOIN [Employee] ON [Timesheet].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [Project] ON [Timesheet].[ProjectID] = [Project].[ProjectID]
WHERE
      (@TimesheetID IS NULL OR @TimesheetID = '' OR [Timesheet].[TimesheetID] > LTRIM(RTRIM(@TimesheetID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] > LTRIM(RTRIM(@FirstName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] > LTRIM(RTRIM(@ProjectName)))
AND   (@StartTime IS NULL OR @StartTime = '' OR [Timesheet].[StartTime] > LTRIM(RTRIM(@StartTime)))
AND   (@EndTime IS NULL OR @EndTime = '' OR [Timesheet].[EndTime] > LTRIM(RTRIM(@EndTime)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Timesheet].[Remarks] > LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Timesheet].[TimesheetID]
     ,[Timesheet].[EmployeeID]
     ,[Timesheet].[ProjectID]
     ,[Timesheet].[StartTime]
     ,[Timesheet].[EndTime]
     ,[Timesheet].[Remarks]
FROM
     [Timesheet]
     INNER JOIN [Employee] ON [Timesheet].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [Project] ON [Timesheet].[ProjectID] = [Project].[ProjectID]
WHERE
      (@TimesheetID IS NULL OR @TimesheetID = '' OR [Timesheet].[TimesheetID] < LTRIM(RTRIM(@TimesheetID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] < LTRIM(RTRIM(@FirstName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] < LTRIM(RTRIM(@ProjectName)))
AND   (@StartTime IS NULL OR @StartTime = '' OR [Timesheet].[StartTime] < LTRIM(RTRIM(@StartTime)))
AND   (@EndTime IS NULL OR @EndTime = '' OR [Timesheet].[EndTime] < LTRIM(RTRIM(@EndTime)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Timesheet].[Remarks] < LTRIM(RTRIM(@Remarks)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Timesheet].[TimesheetID]
     ,[Timesheet].[EmployeeID]
     ,[Timesheet].[ProjectID]
     ,[Timesheet].[StartTime]
     ,[Timesheet].[EndTime]
     ,[Timesheet].[Remarks]
FROM
     [Timesheet]
     INNER JOIN [Employee] ON [Timesheet].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [Project] ON [Timesheet].[ProjectID] = [Project].[ProjectID]
WHERE
      (@TimesheetID IS NULL OR @TimesheetID = '' OR [Timesheet].[TimesheetID] >= LTRIM(RTRIM(@TimesheetID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] >= LTRIM(RTRIM(@FirstName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] >= LTRIM(RTRIM(@ProjectName)))
AND   (@StartTime IS NULL OR @StartTime = '' OR [Timesheet].[StartTime] >= LTRIM(RTRIM(@StartTime)))
AND   (@EndTime IS NULL OR @EndTime = '' OR [Timesheet].[EndTime] >= LTRIM(RTRIM(@EndTime)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Timesheet].[Remarks] >= LTRIM(RTRIM(@Remarks)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Timesheet].[TimesheetID]
     ,[Timesheet].[EmployeeID]
     ,[Timesheet].[ProjectID]
     ,[Timesheet].[StartTime]
     ,[Timesheet].[EndTime]
     ,[Timesheet].[Remarks]
FROM
     [Timesheet]
     INNER JOIN [Employee] ON [Timesheet].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [Project] ON [Timesheet].[ProjectID] = [Project].[ProjectID]
WHERE
      (@TimesheetID IS NULL OR @TimesheetID = '' OR [Timesheet].[TimesheetID] <= LTRIM(RTRIM(@TimesheetID)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] <= LTRIM(RTRIM(@FirstName)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] <= LTRIM(RTRIM(@ProjectName)))
AND   (@StartTime IS NULL OR @StartTime = '' OR [Timesheet].[StartTime] <= LTRIM(RTRIM(@StartTime)))
AND   (@EndTime IS NULL OR @EndTime = '' OR [Timesheet].[EndTime] <= LTRIM(RTRIM(@EndTime)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [Timesheet].[Remarks] <= LTRIM(RTRIM(@Remarks)))

END

END
GO

GRANT EXECUTE ON [TimesheetSearch] TO [Public]
GO

 
