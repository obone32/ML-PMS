--***********************************************************
--SELECT Stored Procedure for BackupLog table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'BackupLogSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [BackupLogSelectAll] 
GO

CREATE PROCEDURE [BackupLogSelectAll]
AS 
BEGIN 
SELECT 
      [BackupLog].[BackupLogID]
     ,[BackupLog].[BackupDate]
     ,[BackupLog].[FilePath]
     ,[BackupLog].[Remarks]
FROM
     [BackupLog]
END 
GO

GRANT EXECUTE ON [BackupLogSelectAll] TO [Public]
GO

 
