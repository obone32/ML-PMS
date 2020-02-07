--***********************************************************
--SELECT Stored Procedure for UserTypePermission table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypePermissionSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypePermissionSelect] 
GO

CREATE PROCEDURE [UserTypePermissionSelect] 
      @UserTypeID int
     ,@UserTypePermissionID int
AS 
BEGIN 
SELECT 
      [UserTypeID]
     ,[UserTypePermissionID]
     ,[PermissionID]
FROM
     [UserTypePermission]
WHERE
    [UserTypeID] = @UserTypeID
AND [UserTypePermissionID] = @UserTypePermissionID
END 
GO

GRANT EXECUTE ON [UserTypePermissionSelect] TO [Public]
GO

 
