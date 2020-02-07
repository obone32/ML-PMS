--***********************************************************
--SELECT Stored Procedure for Architect table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectSelect] 
GO

CREATE PROCEDURE [ArchitectSelect] 
      @ArchitectID int
AS 
BEGIN 
SELECT 
      [ArchitectID]
     ,[ArchitectName]
     ,[Address1]
     ,[Address2]
     ,[City]
     ,[District]
     ,[State]
     ,[Country]
     ,[Pincode]
     ,[EMail]
     ,[ContactNo]
     ,[CompanyID]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
FROM
     [Architect]
WHERE
    [ArchitectID] = @ArchitectID
END 
GO

GRANT EXECUTE ON [ArchitectSelect] TO [Public]
GO

 
