--***********************************************************
--SELECT Stored Procedure for Architect table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectSelectAll] 
GO

CREATE PROCEDURE [ArchitectSelectAll]
AS 
BEGIN 
SELECT 
      [Architect].[ArchitectID]
     ,[Architect].[ArchitectName]
     ,[Architect].[Address1]
     ,[Architect].[Address2]
     ,[Architect].[City]
     ,[Architect].[District]
     ,[Architect].[State]
     ,[Architect].[Country]
     ,[Architect].[Pincode]
     ,[Architect].[EMail]
     ,[Architect].[ContactNo]
     ,[Architect].[CompanyID]
     ,[Architect].[AddUserID]
     ,[Architect].[AddDate]
     ,[Architect].[ArchiveUserID]
     ,[Architect].[ArchiveDate]
FROM
     [Architect]
END 
GO

GRANT EXECUTE ON [ArchitectSelectAll] TO [Public]
GO

 
