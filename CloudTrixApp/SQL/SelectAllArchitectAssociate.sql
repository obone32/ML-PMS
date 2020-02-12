--***********************************************************
--SELECT Stored Procedure for ArchitectAssociate table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectAssociateSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectAssociateSelectAll] 
GO

CREATE PROCEDURE [ArchitectAssociateSelectAll]
AS 
BEGIN 
SELECT 
      [ArchitectAssociate].[ArchitectID]
     ,[ArchitectAssociate].[ArchitectAssociateID]
     ,[ArchitectAssociate].[AssociateName]
     ,[ArchitectAssociate].[ContactNo]
     ,[ArchitectAssociate].[EMail]
FROM
     [ArchitectAssociate]
END 
GO

GRANT EXECUTE ON [ArchitectAssociateSelectAll] TO [Public]
GO

 
