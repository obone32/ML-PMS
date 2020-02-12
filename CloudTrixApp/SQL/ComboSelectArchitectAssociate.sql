--***********************************************************
--SELECT Stored Procedure for ArchitectAssociate Architect table
--***********************************************************
GO

USE PMMS
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ArchitectAssociate_ArchitectSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ArchitectAssociate_ArchitectSelect] 
GO

CREATE PROCEDURE [ArchitectAssociate_ArchitectSelect]
AS 
BEGIN 
SELECT
     [ArchitectID]
    ,[ArchitectName]
FROM 
     [Architect]
END 
GO

GRANT EXECUTE ON [ArchitectAssociate_ArchitectSelect] TO [Public]
GO


 
