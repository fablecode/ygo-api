/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 All Sql Scripts must be runnable.				
--------------------------------------------------------------------------------------
*/

MERGE dbo.Categories AS Target
USING
(
	SELECT 
		* 
	FROM
	(
		VALUES
			('Monster', GETDATE(), GETDATE()),
			('Spell', GETDATE(), GETDATE()),
			('Trap', GETDATE(), GETDATE())
		
	)As s(Name, Created, Updated)
) As Source
ON 
	Target.Name = Source.Name
WHEN NOT MATCHED THEN
	INSERT (Name, Created, Updated)
	VALUES (Source.Name, Source.Created, Source.Updated);

PRINT (N'[dbo].[Categories]: Insert Batch: 1.....Done!');
GO

MERGE dbo.Formats AS Target
USING
(
	SELECT 
		* 
	FROM
	(
		VALUES
			('Yu-Gi-Oh! Trading Card Game', N'TCG', GETDATE(), GETDATE()),
			('Yu-Gi-Oh! Official Card Game', N'OCG', GETDATE(), GETDATE()),
			('Traditional', N'Traditional', GETDATE(), GETDATE())
		
	)As s(Name, Acronym, Created, Updated)
) As Source
ON 
	Target.Name = Source.Name And 
	Target.Acronym = Source.Acronym
WHEN NOT MATCHED THEN
	INSERT (Name, Acronym, Created, Updated)
	VALUES (Source.Name, Source.Acronym, Source.Created, Source.Updated);

PRINT (N'[dbo].[Formats]: Insert Batch: 1.....Done!');
GO

MERGE dbo.Limits AS Target
USING
(
	SELECT 
		* 
	FROM
	(
		VALUES
			('Forbidden', N'no copies are currently allowed. This status is only used in the TCG Advanced Format and in the OCG; Forbidden cards are changed to Limited in the TCG Traditional Format', GETDATE(), GETDATE()),
			('Limited', N'only one copy is currently allowed.', GETDATE(), GETDATE()),
			('Semi-Limited', N'up to two copies are currently allowed.', GETDATE(), GETDATE())
		
	)As s(Name, Description, Created, Updated)
) As Source
ON Target.Name = Source.Name
WHEN NOT MATCHED THEN
	INSERT (Name, Description, Created, Updated)
	VALUES (Source.Name, Source.Description, Source.Created, Source.Updated);

PRINT (N'[dbo].[Limits]: Insert Batch: 1.....Done!');
