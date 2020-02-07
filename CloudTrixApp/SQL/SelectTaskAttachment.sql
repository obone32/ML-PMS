--***********************************************************
--SELECT Stored Procedure for TaskAttachment table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAttachmentSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAttachmentSelect] 
GO

CREATE PROCEDURE [TaskAttachmentSelect] 
      @TaskID int
     ,@TaskAttachmentID int
AS 
BEGIN 
SELECT 
      [TaskID]
     ,[TaskAttachmentID]
     ,[AttachmentName]
     ,[Decription]
     ,[FilePath]
FROM
     [TaskAttachment]
WHERE
    [TaskID] = @TaskID
AND [TaskAttachmentID] = @TaskAttachmentID
END 
GO

GRANT EXECUTE ON [TaskAttachmentSelect] TO [Public]
GO

 
