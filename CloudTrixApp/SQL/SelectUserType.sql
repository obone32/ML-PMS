--***********************************************************
--SELECT Stored Procedure for UserType table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'UserTypeSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [UserTypeSelect] 
GO

CREATE PROCEDURE [UserTypeSelect] 
      @UserTypeID int
AS 
BEGIN 
SELECT 
      [UserTypeID]
     ,[UserTypeName]
FROM
     [UserType]
WHERE
    [UserTypeID] = @UserTypeID
END 
GO

GRANT EXECUTE ON [UserTypeSelect] TO [Public]
GO

 
