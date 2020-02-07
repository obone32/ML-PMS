--***********************************************************
--SEARCH Stored Procedure for ProjectAssignment table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectAssignmentSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectAssignmentSearch] 
GO

CREATE PROCEDURE [ProjectAssignmentSearch] 
      @ProjectAssignmentID int
     ,@ProjectName varchar(-1)
     ,@FirstName varchar(-1)
     ,@AssignmentDate datetime
     ,@Remarks varchar(8000)
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
      [ProjectAssignment].[ProjectAssignmentID]
     ,[ProjectAssignment].[ProjectID]
     ,[ProjectAssignment].[EmployeeID]
     ,[ProjectAssignment].[AssignmentDate]
     ,[ProjectAssignment].[Remarks]
     ,[ProjectAssignment].[AddUserID]
     ,[ProjectAssignment].[AddDate]
     ,[ProjectAssignment].[ArchiveUserID]
     ,[ProjectAssignment].[ArchiveDate]
FROM
     [ProjectAssignment]
     INNER JOIN [Project] ON [ProjectAssignment].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Employee] ON [ProjectAssignment].[EmployeeID] = [Employee].[EmployeeID]
WHERE
      (@ProjectAssignmentID IS NULL OR @ProjectAssignmentID = '' OR [ProjectAssignment].[ProjectAssignmentID] LIKE '%' + LTRIM(RTRIM(@ProjectAssignmentID)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE '%' + LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] LIKE '%' + LTRIM(RTRIM(@FirstName)) + '%')
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [ProjectAssignment].[AssignmentDate] LIKE '%' + LTRIM(RTRIM(@AssignmentDate)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [ProjectAssignment].[Remarks] LIKE '%' + LTRIM(RTRIM(@Remarks)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [ProjectAssignment].[AddUserID] LIKE '%' + LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [ProjectAssignment].[AddDate] LIKE '%' + LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [ProjectAssignment].[ArchiveUserID] LIKE '%' + LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [ProjectAssignment].[ArchiveDate] LIKE '%' + LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [ProjectAssignment].[ProjectAssignmentID]
     ,[ProjectAssignment].[ProjectID]
     ,[ProjectAssignment].[EmployeeID]
     ,[ProjectAssignment].[AssignmentDate]
     ,[ProjectAssignment].[Remarks]
     ,[ProjectAssignment].[AddUserID]
     ,[ProjectAssignment].[AddDate]
     ,[ProjectAssignment].[ArchiveUserID]
     ,[ProjectAssignment].[ArchiveDate]
FROM
     [ProjectAssignment]
     INNER JOIN [Project] ON [ProjectAssignment].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Employee] ON [ProjectAssignment].[EmployeeID] = [Employee].[EmployeeID]
WHERE
      (@ProjectAssignmentID IS NULL OR @ProjectAssignmentID = '' OR [ProjectAssignment].[ProjectAssignmentID] = LTRIM(RTRIM(@ProjectAssignmentID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] = LTRIM(RTRIM(@ProjectName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] = LTRIM(RTRIM(@FirstName)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [ProjectAssignment].[AssignmentDate] = LTRIM(RTRIM(@AssignmentDate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [ProjectAssignment].[Remarks] = LTRIM(RTRIM(@Remarks)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [ProjectAssignment].[AddUserID] = LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [ProjectAssignment].[AddDate] = LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [ProjectAssignment].[ArchiveUserID] = LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [ProjectAssignment].[ArchiveDate] = LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [ProjectAssignment].[ProjectAssignmentID]
     ,[ProjectAssignment].[ProjectID]
     ,[ProjectAssignment].[EmployeeID]
     ,[ProjectAssignment].[AssignmentDate]
     ,[ProjectAssignment].[Remarks]
     ,[ProjectAssignment].[AddUserID]
     ,[ProjectAssignment].[AddDate]
     ,[ProjectAssignment].[ArchiveUserID]
     ,[ProjectAssignment].[ArchiveDate]
FROM
     [ProjectAssignment]
     INNER JOIN [Project] ON [ProjectAssignment].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Employee] ON [ProjectAssignment].[EmployeeID] = [Employee].[EmployeeID]
WHERE
      (@ProjectAssignmentID IS NULL OR @ProjectAssignmentID = '' OR [ProjectAssignment].[ProjectAssignmentID] LIKE LTRIM(RTRIM(@ProjectAssignmentID)) + '%')
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] LIKE LTRIM(RTRIM(@ProjectName)) + '%')
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] LIKE LTRIM(RTRIM(@FirstName)) + '%')
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [ProjectAssignment].[AssignmentDate] LIKE LTRIM(RTRIM(@AssignmentDate)) + '%')
AND   (@Remarks IS NULL OR @Remarks = '' OR [ProjectAssignment].[Remarks] LIKE LTRIM(RTRIM(@Remarks)) + '%')
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [ProjectAssignment].[AddUserID] LIKE LTRIM(RTRIM(@AddUserID)) + '%')
AND   (@AddDate IS NULL OR @AddDate = '' OR [ProjectAssignment].[AddDate] LIKE LTRIM(RTRIM(@AddDate)) + '%')
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [ProjectAssignment].[ArchiveUserID] LIKE LTRIM(RTRIM(@ArchiveUserID)) + '%')
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [ProjectAssignment].[ArchiveDate] LIKE LTRIM(RTRIM(@ArchiveDate)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [ProjectAssignment].[ProjectAssignmentID]
     ,[ProjectAssignment].[ProjectID]
     ,[ProjectAssignment].[EmployeeID]
     ,[ProjectAssignment].[AssignmentDate]
     ,[ProjectAssignment].[Remarks]
     ,[ProjectAssignment].[AddUserID]
     ,[ProjectAssignment].[AddDate]
     ,[ProjectAssignment].[ArchiveUserID]
     ,[ProjectAssignment].[ArchiveDate]
FROM
     [ProjectAssignment]
     INNER JOIN [Project] ON [ProjectAssignment].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Employee] ON [ProjectAssignment].[EmployeeID] = [Employee].[EmployeeID]
WHERE
      (@ProjectAssignmentID IS NULL OR @ProjectAssignmentID = '' OR [ProjectAssignment].[ProjectAssignmentID] > LTRIM(RTRIM(@ProjectAssignmentID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] > LTRIM(RTRIM(@ProjectName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] > LTRIM(RTRIM(@FirstName)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [ProjectAssignment].[AssignmentDate] > LTRIM(RTRIM(@AssignmentDate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [ProjectAssignment].[Remarks] > LTRIM(RTRIM(@Remarks)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [ProjectAssignment].[AddUserID] > LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [ProjectAssignment].[AddDate] > LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [ProjectAssignment].[ArchiveUserID] > LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [ProjectAssignment].[ArchiveDate] > LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [ProjectAssignment].[ProjectAssignmentID]
     ,[ProjectAssignment].[ProjectID]
     ,[ProjectAssignment].[EmployeeID]
     ,[ProjectAssignment].[AssignmentDate]
     ,[ProjectAssignment].[Remarks]
     ,[ProjectAssignment].[AddUserID]
     ,[ProjectAssignment].[AddDate]
     ,[ProjectAssignment].[ArchiveUserID]
     ,[ProjectAssignment].[ArchiveDate]
FROM
     [ProjectAssignment]
     INNER JOIN [Project] ON [ProjectAssignment].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Employee] ON [ProjectAssignment].[EmployeeID] = [Employee].[EmployeeID]
WHERE
      (@ProjectAssignmentID IS NULL OR @ProjectAssignmentID = '' OR [ProjectAssignment].[ProjectAssignmentID] < LTRIM(RTRIM(@ProjectAssignmentID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] < LTRIM(RTRIM(@ProjectName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] < LTRIM(RTRIM(@FirstName)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [ProjectAssignment].[AssignmentDate] < LTRIM(RTRIM(@AssignmentDate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [ProjectAssignment].[Remarks] < LTRIM(RTRIM(@Remarks)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [ProjectAssignment].[AddUserID] < LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [ProjectAssignment].[AddDate] < LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [ProjectAssignment].[ArchiveUserID] < LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [ProjectAssignment].[ArchiveDate] < LTRIM(RTRIM(@ArchiveDate)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [ProjectAssignment].[ProjectAssignmentID]
     ,[ProjectAssignment].[ProjectID]
     ,[ProjectAssignment].[EmployeeID]
     ,[ProjectAssignment].[AssignmentDate]
     ,[ProjectAssignment].[Remarks]
     ,[ProjectAssignment].[AddUserID]
     ,[ProjectAssignment].[AddDate]
     ,[ProjectAssignment].[ArchiveUserID]
     ,[ProjectAssignment].[ArchiveDate]
FROM
     [ProjectAssignment]
     INNER JOIN [Project] ON [ProjectAssignment].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Employee] ON [ProjectAssignment].[EmployeeID] = [Employee].[EmployeeID]
WHERE
      (@ProjectAssignmentID IS NULL OR @ProjectAssignmentID = '' OR [ProjectAssignment].[ProjectAssignmentID] >= LTRIM(RTRIM(@ProjectAssignmentID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] >= LTRIM(RTRIM(@ProjectName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] >= LTRIM(RTRIM(@FirstName)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [ProjectAssignment].[AssignmentDate] >= LTRIM(RTRIM(@AssignmentDate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [ProjectAssignment].[Remarks] >= LTRIM(RTRIM(@Remarks)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [ProjectAssignment].[AddUserID] >= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [ProjectAssignment].[AddDate] >= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [ProjectAssignment].[ArchiveUserID] >= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [ProjectAssignment].[ArchiveDate] >= LTRIM(RTRIM(@ArchiveDate)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [ProjectAssignment].[ProjectAssignmentID]
     ,[ProjectAssignment].[ProjectID]
     ,[ProjectAssignment].[EmployeeID]
     ,[ProjectAssignment].[AssignmentDate]
     ,[ProjectAssignment].[Remarks]
     ,[ProjectAssignment].[AddUserID]
     ,[ProjectAssignment].[AddDate]
     ,[ProjectAssignment].[ArchiveUserID]
     ,[ProjectAssignment].[ArchiveDate]
FROM
     [ProjectAssignment]
     INNER JOIN [Project] ON [ProjectAssignment].[ProjectID] = [Project].[ProjectID]
     INNER JOIN [Employee] ON [ProjectAssignment].[EmployeeID] = [Employee].[EmployeeID]
WHERE
      (@ProjectAssignmentID IS NULL OR @ProjectAssignmentID = '' OR [ProjectAssignment].[ProjectAssignmentID] <= LTRIM(RTRIM(@ProjectAssignmentID)))
AND   (@ProjectName IS NULL OR @ProjectName = '' OR [Project].[ProjectName] <= LTRIM(RTRIM(@ProjectName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] <= LTRIM(RTRIM(@FirstName)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [ProjectAssignment].[AssignmentDate] <= LTRIM(RTRIM(@AssignmentDate)))
AND   (@Remarks IS NULL OR @Remarks = '' OR [ProjectAssignment].[Remarks] <= LTRIM(RTRIM(@Remarks)))
AND   (@AddUserID IS NULL OR @AddUserID = '' OR [ProjectAssignment].[AddUserID] <= LTRIM(RTRIM(@AddUserID)))
AND   (@AddDate IS NULL OR @AddDate = '' OR [ProjectAssignment].[AddDate] <= LTRIM(RTRIM(@AddDate)))
AND   (@ArchiveUserID IS NULL OR @ArchiveUserID = '' OR [ProjectAssignment].[ArchiveUserID] <= LTRIM(RTRIM(@ArchiveUserID)))
AND   (@ArchiveDate IS NULL OR @ArchiveDate = '' OR [ProjectAssignment].[ArchiveDate] <= LTRIM(RTRIM(@ArchiveDate)))

END

END
GO

GRANT EXECUTE ON [ProjectAssignmentSearch] TO [Public]
GO

 
