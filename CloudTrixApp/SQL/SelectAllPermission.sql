--***********************************************************
--SELECT Stored Procedure for Permission table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'PermissionSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [PermissionSelectAll] 
GO

CREATE PROCEDURE [PermissionSelectAll]
AS 
BEGIN 
SELECT 
      [Permission].[PermissionID]
     ,[Permission].[PermissionName]
FROM
     [Permission]
END 
GO

GRANT EXECUTE ON [PermissionSelectAll] TO [Public]
GO

 
