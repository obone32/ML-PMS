--***********************************************************
--SEARCH Stored Procedure for TaskAssignment table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAssignmentSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAssignmentSearch] 
GO

CREATE PROCEDURE [TaskAssignmentSearch] 
      @TaskAssignmentID int
     ,@AssignmentDate datetime
     ,@TaskName varchar(-1)
     ,@FirstName varchar(-1)
     ,@TaskStateName varchar(-1)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [TaskAssignment].[TaskAssignmentID]
     ,[TaskAssignment].[AssignmentDate]
     ,[TaskAssignment].[TaskID]
     ,[TaskAssignment].[EmployeeID]
     ,[TaskAssignment].[TaskStateID]
FROM
     [TaskAssignment]
     INNER JOIN [Task] ON [TaskAssignment].[TaskID] = [Task].[TaskID]
     INNER JOIN [Employee] ON [TaskAssignment].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [TaskState] ON [TaskAssignment].[TaskStateID] = [TaskState].[TaskStateID]
WHERE
      (@TaskAssignmentID IS NULL OR @TaskAssignmentID = '' OR [TaskAssignment].[TaskAssignmentID] LIKE '%' + LTRIM(RTRIM(@TaskAssignmentID)) + '%')
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [TaskAssignment].[AssignmentDate] LIKE '%' + LTRIM(RTRIM(@AssignmentDate)) + '%')
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] LIKE '%' + LTRIM(RTRIM(@TaskName)) + '%')
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] LIKE '%' + LTRIM(RTRIM(@FirstName)) + '%')
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] LIKE '%' + LTRIM(RTRIM(@TaskStateName)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [TaskAssignment].[TaskAssignmentID]
     ,[TaskAssignment].[AssignmentDate]
     ,[TaskAssignment].[TaskID]
     ,[TaskAssignment].[EmployeeID]
     ,[TaskAssignment].[TaskStateID]
FROM
     [TaskAssignment]
     INNER JOIN [Task] ON [TaskAssignment].[TaskID] = [Task].[TaskID]
     INNER JOIN [Employee] ON [TaskAssignment].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [TaskState] ON [TaskAssignment].[TaskStateID] = [TaskState].[TaskStateID]
WHERE
      (@TaskAssignmentID IS NULL OR @TaskAssignmentID = '' OR [TaskAssignment].[TaskAssignmentID] = LTRIM(RTRIM(@TaskAssignmentID)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [TaskAssignment].[AssignmentDate] = LTRIM(RTRIM(@AssignmentDate)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] = LTRIM(RTRIM(@TaskName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] = LTRIM(RTRIM(@FirstName)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] = LTRIM(RTRIM(@TaskStateName)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [TaskAssignment].[TaskAssignmentID]
     ,[TaskAssignment].[AssignmentDate]
     ,[TaskAssignment].[TaskID]
     ,[TaskAssignment].[EmployeeID]
     ,[TaskAssignment].[TaskStateID]
FROM
     [TaskAssignment]
     INNER JOIN [Task] ON [TaskAssignment].[TaskID] = [Task].[TaskID]
     INNER JOIN [Employee] ON [TaskAssignment].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [TaskState] ON [TaskAssignment].[TaskStateID] = [TaskState].[TaskStateID]
WHERE
      (@TaskAssignmentID IS NULL OR @TaskAssignmentID = '' OR [TaskAssignment].[TaskAssignmentID] LIKE LTRIM(RTRIM(@TaskAssignmentID)) + '%')
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [TaskAssignment].[AssignmentDate] LIKE LTRIM(RTRIM(@AssignmentDate)) + '%')
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] LIKE LTRIM(RTRIM(@TaskName)) + '%')
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] LIKE LTRIM(RTRIM(@FirstName)) + '%')
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] LIKE LTRIM(RTRIM(@TaskStateName)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [TaskAssignment].[TaskAssignmentID]
     ,[TaskAssignment].[AssignmentDate]
     ,[TaskAssignment].[TaskID]
     ,[TaskAssignment].[EmployeeID]
     ,[TaskAssignment].[TaskStateID]
FROM
     [TaskAssignment]
     INNER JOIN [Task] ON [TaskAssignment].[TaskID] = [Task].[TaskID]
     INNER JOIN [Employee] ON [TaskAssignment].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [TaskState] ON [TaskAssignment].[TaskStateID] = [TaskState].[TaskStateID]
WHERE
      (@TaskAssignmentID IS NULL OR @TaskAssignmentID = '' OR [TaskAssignment].[TaskAssignmentID] > LTRIM(RTRIM(@TaskAssignmentID)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [TaskAssignment].[AssignmentDate] > LTRIM(RTRIM(@AssignmentDate)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] > LTRIM(RTRIM(@TaskName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] > LTRIM(RTRIM(@FirstName)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] > LTRIM(RTRIM(@TaskStateName)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [TaskAssignment].[TaskAssignmentID]
     ,[TaskAssignment].[AssignmentDate]
     ,[TaskAssignment].[TaskID]
     ,[TaskAssignment].[EmployeeID]
     ,[TaskAssignment].[TaskStateID]
FROM
     [TaskAssignment]
     INNER JOIN [Task] ON [TaskAssignment].[TaskID] = [Task].[TaskID]
     INNER JOIN [Employee] ON [TaskAssignment].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [TaskState] ON [TaskAssignment].[TaskStateID] = [TaskState].[TaskStateID]
WHERE
      (@TaskAssignmentID IS NULL OR @TaskAssignmentID = '' OR [TaskAssignment].[TaskAssignmentID] < LTRIM(RTRIM(@TaskAssignmentID)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [TaskAssignment].[AssignmentDate] < LTRIM(RTRIM(@AssignmentDate)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] < LTRIM(RTRIM(@TaskName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] < LTRIM(RTRIM(@FirstName)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] < LTRIM(RTRIM(@TaskStateName)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [TaskAssignment].[TaskAssignmentID]
     ,[TaskAssignment].[AssignmentDate]
     ,[TaskAssignment].[TaskID]
     ,[TaskAssignment].[EmployeeID]
     ,[TaskAssignment].[TaskStateID]
FROM
     [TaskAssignment]
     INNER JOIN [Task] ON [TaskAssignment].[TaskID] = [Task].[TaskID]
     INNER JOIN [Employee] ON [TaskAssignment].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [TaskState] ON [TaskAssignment].[TaskStateID] = [TaskState].[TaskStateID]
WHERE
      (@TaskAssignmentID IS NULL OR @TaskAssignmentID = '' OR [TaskAssignment].[TaskAssignmentID] >= LTRIM(RTRIM(@TaskAssignmentID)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [TaskAssignment].[AssignmentDate] >= LTRIM(RTRIM(@AssignmentDate)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] >= LTRIM(RTRIM(@TaskName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] >= LTRIM(RTRIM(@FirstName)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] >= LTRIM(RTRIM(@TaskStateName)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [TaskAssignment].[TaskAssignmentID]
     ,[TaskAssignment].[AssignmentDate]
     ,[TaskAssignment].[TaskID]
     ,[TaskAssignment].[EmployeeID]
     ,[TaskAssignment].[TaskStateID]
FROM
     [TaskAssignment]
     INNER JOIN [Task] ON [TaskAssignment].[TaskID] = [Task].[TaskID]
     INNER JOIN [Employee] ON [TaskAssignment].[EmployeeID] = [Employee].[EmployeeID]
     INNER JOIN [TaskState] ON [TaskAssignment].[TaskStateID] = [TaskState].[TaskStateID]
WHERE
      (@TaskAssignmentID IS NULL OR @TaskAssignmentID = '' OR [TaskAssignment].[TaskAssignmentID] <= LTRIM(RTRIM(@TaskAssignmentID)))
AND   (@AssignmentDate IS NULL OR @AssignmentDate = '' OR [TaskAssignment].[AssignmentDate] <= LTRIM(RTRIM(@AssignmentDate)))
AND   (@TaskName IS NULL OR @TaskName = '' OR [Task].[TaskName] <= LTRIM(RTRIM(@TaskName)))
AND   (@FirstName IS NULL OR @FirstName = '' OR [Employee].[FirstName] <= LTRIM(RTRIM(@FirstName)))
AND   (@TaskStateName IS NULL OR @TaskStateName = '' OR [TaskState].[TaskStateName] <= LTRIM(RTRIM(@TaskStateName)))

END

END
GO

GRANT EXECUTE ON [TaskAssignmentSearch] TO [Public]
GO

 
