CREATE VIEW [dbo].[DictionaryClusterAdminView] AS
	SELECT		[DictionaryCluster].[Id],
				[DictionaryCluster].[DictionaryId]				AS	[DictionaryAdminId],
				[DictionaryCluster].[Name],	
				[DictionaryCluster].[Description],	
				[DictionaryCluster].[Color],
				[DictionaryCluster].[Order],
				[DictionaryCluster].[ImageName],
				[DictionaryCluster].[Audit_CreatedBy],	
				[DictionaryCluster].[Audit_CreatedOn],	
				[DictionaryCluster].[Audit_ModifiedBy],	
				[DictionaryCluster].[Audit_ModifiedOn],	
				[DictionaryCluster].[Audit_DeletedBy],	
				[DictionaryCluster].[Audit_DeletedOn],	
				[DictionaryCluster].[Audit_IsDeleted],	
				[DictionaryCluster].[Audit_VersionId],	
				[DictionaryCluster].[LegacyId],
				[DictionaryCluster].[DictionaryId]				AS	[DictionaryId],
				[DictionaryAdminView].[Name]					AS	[DictionaryName],
				[DictionaryAdminView].[NumberOfUsages]			AS	[DictionaryNumberOfUsages],
				CAST([DictionaryAdminView].[IsLive] AS BIT)		AS	[DictionaryIsLive]

	FROM		[DictionaryCluster]	WITH (NOLOCK)

	INNER JOIN	[DictionaryAdminView]
		ON		[DictionaryAdminView].[Id] = [DictionaryCluster].[DictionaryId]

	WHERE		[DictionaryCluster].[Audit_IsDeleted] = 0