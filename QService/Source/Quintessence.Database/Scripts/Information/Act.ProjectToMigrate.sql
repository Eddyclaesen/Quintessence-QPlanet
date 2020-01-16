UPDATE		[ProjectToMigrate]
SET			[ProjectFicheId] =					[ProjectFiche].[ACProject_id],
			[Name] =							[Projecten].[Omschrijving],
			[ContactId] =						[Projecten].[Contact_Id],
			[ContactName] =						[Contact].[Name] + ' - ' + [Contact].[Department],
			[ProjectManagerAssociateId] =		[ProjectFiche].[Proma_Id],
			[CustomerAssistantAssociateId] =	[CustomerAssistant].[Klantassistente],
			[FunctionTitle] =					[ProjectFiche].[FunctieTitel],
			[FunctionInformation] =				[ProjectFiche].[FunctieInfo],
			[DictionaryLegacyId] =				[ProjectFiche].[Woordenboek_Id],
			[ProjectTypeCode] =					CASE [ProjectFiche].[ProjectType]
													WHEN 1	THEN 'AC'
													WHEN 2	THEN 'EA'
													WHEN 3	THEN 'FA'
													WHEN 4	THEN 'FA'
													WHEN 5	THEN 'AC'
													WHEN 6	THEN 'FA'
													WHEN 7	THEN 'DC'
													WHEN 8	THEN 'FD'
													WHEN 12	THEN 'DC'
													WHEN 13	THEN 'DC'
													WHEN 14	THEN 'FD'
													ELSE CAST([ProjectFiche].[ProjectType] AS VARCHAR(2))
												END,
			[CreatedOn] =						ISNULL([ProjectFiche].[PostDate], '2000-01-01 00:00:00.000'),
			[CrmProjectId] =					[ProjectFiche].[SOProject_Id]
			
FROM		[ProjectToMigrate]
INNER JOIN	[Projecten] 
	ON		[Projecten].[Project_id] = [ProjectToMigrate].[Id]
INNER JOIN	[ProjectFiche] 
	ON		[ProjectFiche].[Project_id] = [ProjectToMigrate].[Id]
LEFT JOIN	[SuperOffice7].[dbo].[vw_Klanten_ptool] [CustomerAssistant]
	ON		[CustomerAssistant].[ID Klant] = [Projecten].[Contact_Id]
LEFT JOIN	[SuperOffice7].[dbo].[Contact] [Contact]
	ON		[Contact].[Contact_Id] = [Projecten].[Contact_Id]
	
UPDATE dbo.ProjectToMigrate SET ContactId = 892, ContactName = 'Universiteit Antwerpen - Campus Middelheim' WHERE id = 1178
UPDATE dbo.ProjectToMigrate SET ProjectTypeCode = 'DC' WHERE id = 1245
UPDATE dbo.ProjectToMigrate SET ProjectTypeCode = 'CA' WHERE id = 2239

SELECT Name, ContactName, FunctionTitle, ProjectTypeCode FROM dbo.ProjectToMigrate
WHERE CrmProjectId IS NULL


