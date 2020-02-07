--***********************************************************
--SELECT Stored Procedure for UserType table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypeSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypeSelectAll] 
GO

CREATE PROCEDURE [UserTypeSelectAll]
AS 
BEGIN 
SELECT 
      [UserType].[UserTypeID]
     ,[UserType].[UserTypeName]
FROM
     [UserType]
END 
GO

GRANT EXECUTE ON [UserTypeSelectAll] TO [Public]
GO

 
