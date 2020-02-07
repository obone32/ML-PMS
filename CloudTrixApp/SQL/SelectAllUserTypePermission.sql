--***********************************************************
--SELECT Stored Procedure for UserTypePermission table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypePermissionSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypePermissionSelectAll] 
GO

CREATE PROCEDURE [UserTypePermissionSelectAll]
AS 
BEGIN 
SELECT 
      [UserTypePermission].[UserTypeID]
     ,[UserTypePermission].[UserTypePermissionID]
     ,[UserTypePermission].[PermissionID]
FROM
     [UserTypePermission]
END 
GO

GRANT EXECUTE ON [UserTypePermissionSelectAll] TO [Public]
GO

 
