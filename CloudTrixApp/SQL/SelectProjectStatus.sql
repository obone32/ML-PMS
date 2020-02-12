--***********************************************************
--SELECT Stored Procedure for ProjectStatus table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ProjectStatusSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ProjectStatusSelect] 
GO

CREATE PROCEDURE [ProjectStatusSelect] 
      @ProjectStatusID int
AS 
BEGIN 
SELECT 
      [ProjectStatusID]
     ,[ProjectStatusName]
FROM
     [ProjectStatus]
WHERE
    [ProjectStatusID] = @ProjectStatusID
END 
GO

GRANT EXECUTE ON [ProjectStatusSelect] TO [Public]
GO

 
