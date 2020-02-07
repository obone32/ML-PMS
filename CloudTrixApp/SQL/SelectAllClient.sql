--***********************************************************
--SELECT Stored Procedure for Client table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ClientSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [ClientSelectAll] 
GO

CREATE PROCEDURE [ClientSelectAll]
AS 
BEGIN 
SELECT 
      [Client].[ClientID]
     ,[Client].[ClientName]
     ,[Client].[Address1]
     ,[Client].[Address2]
     ,[Client].[City]
     ,[Client].[District]
     ,[Client].[State]
     ,[Client].[PinCode]
     ,[Client].[ContactNo]
     ,[Client].[EMail]
     ,[Client].[GSTIN]
     ,[Client].[CompanyID]
     ,[Client].[AddUserID]
     ,[Client].[AddDate]
     ,[Client].[ArchiveUserID]
     ,[Client].[ArchiveDate]
FROM
     [Client]
END 
GO

GRANT EXECUTE ON [ClientSelectAll] TO [Public]
GO

 
