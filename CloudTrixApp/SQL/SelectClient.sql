--***********************************************************
--SELECT Stored Procedure for Client table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ClientSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ClientSelect] 
GO

CREATE PROCEDURE [ClientSelect] 
      @ClientID int
AS 
BEGIN 
SELECT 
      [ClientID]
     ,[ClientName]
     ,[Address1]
     ,[Address2]
     ,[City]
     ,[District]
     ,[State]
     ,[PinCode]
     ,[ContactNo]
     ,[EMail]
     ,[GSTIN]
     ,[CompanyID]
     ,[AddUserID]
     ,[AddDate]
     ,[ArchiveUserID]
     ,[ArchiveDate]
FROM
     [Client]
WHERE
    [ClientID] = @ClientID
END 
GO

GRANT EXECUTE ON [ClientSelect] TO [Public]
GO

 
