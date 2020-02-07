--***********************************************************
--SELECT Stored Procedure for BackupLog table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'BackupLogSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [BackupLogSelect] 
GO

CREATE PROCEDURE [BackupLogSelect] 
      @BackupLogID int
AS 
BEGIN 
SELECT 
      [BackupLogID]
     ,[BackupDate]
     ,[FilePath]
     ,[Remarks]
FROM
     [BackupLog]
WHERE
    [BackupLogID] = @BackupLogID
END 
GO

GRANT EXECUTE ON [BackupLogSelect] TO [Public]
GO

 
