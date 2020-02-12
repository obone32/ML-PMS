--***********************************************************
--SELECT Stored Procedure for TaskAttachment table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'TaskAttachmentSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [TaskAttachmentSelectAll] 
GO

CREATE PROCEDURE [TaskAttachmentSelectAll]
AS 
BEGIN 
SELECT 
      [TaskAttachment].[TaskID]
     ,[TaskAttachment].[TaskAttachmentID]
     ,[TaskAttachment].[AttachmentName]
     ,[TaskAttachment].[Decription]
     ,[TaskAttachment].[FilePath]
FROM
     [TaskAttachment]
END 
GO

GRANT EXECUTE ON [TaskAttachmentSelectAll] TO [Public]
GO

 
