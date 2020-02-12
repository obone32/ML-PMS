--***********************************************************
--SELECT Stored Procedure for Permission table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'PermissionSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [PermissionSelect] 
GO

CREATE PROCEDURE [PermissionSelect] 
      @PermissionID int
AS 
BEGIN 
SELECT 
      [PermissionID]
     ,[PermissionName]
FROM
     [Permission]
WHERE
    [PermissionID] = @PermissionID
END 
GO

GRANT EXECUTE ON [PermissionSelect] TO [Public]
GO

 
