--***********************************************************
--SELECT Stored Procedure for TaskAttachment Task table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAttachment_TaskSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAttachment_TaskSelect] 
GO

CREATE PROCEDURE [TaskAttachment_TaskSelect]
AS 
BEGIN 
SELECT
     [TaskID]
FROM 
     [Task]
END 
GO

GRANT EXECUTE ON [TaskAttachment_TaskSelect] TO [Public]
GO


 
