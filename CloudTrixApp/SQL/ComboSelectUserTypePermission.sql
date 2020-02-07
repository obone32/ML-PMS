--***********************************************************
--SELECT Stored Procedure for UserTypePermission UserType table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypePermission_UserTypeSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypePermission_UserTypeSelect] 
GO

CREATE PROCEDURE [UserTypePermission_UserTypeSelect]
AS 
BEGIN 
SELECT
     [UserTypeID]
    ,[UserTypeName]
FROM 
     [UserType]
END 
GO

GRANT EXECUTE ON [UserTypePermission_UserTypeSelect] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for UserTypePermission Permission table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypePermission_PermissionSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypePermission_PermissionSelect] 
GO

CREATE PROCEDURE [UserTypePermission_PermissionSelect]
AS 
BEGIN 
SELECT
     [PermissionID]
    ,[PermissionName]
FROM 
     [Permission]
END 
GO

GRANT EXECUTE ON [UserTypePermission_PermissionSelect] TO [Public]
GO


 
