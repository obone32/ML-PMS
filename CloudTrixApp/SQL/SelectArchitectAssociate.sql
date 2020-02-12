--***********************************************************
--SELECT Stored Procedure for ArchitectAssociate table
--***********************************************************
GO

USE PMMS
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectAssociateSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectAssociateSelect] 
GO

CREATE PROCEDURE [ArchitectAssociateSelect] 
      @ArchitectID int
     ,@ArchitectAssociateID int
AS 
BEGIN 
SELECT 
      [ArchitectID]
     ,[ArchitectAssociateID]
     ,[AssociateName]
     ,[ContactNo]
     ,[EMail]
FROM
     [ArchitectAssociate]
WHERE
    [ArchitectID] = @ArchitectID
AND [ArchitectAssociateID] = @ArchitectAssociateID
END 
GO

GRANT EXECUTE ON [ArchitectAssociateSelect] TO [Public]
GO

 
