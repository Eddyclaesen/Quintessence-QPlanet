CREATE VIEW [dbo].[DictionaryCompetenceAdminView] AS
	SELECT		[DictionaryCompetence].[Id],
				[DictionaryCompetence].[DictionaryClusterId]						AS	[DictionaryClusterAdminId],
				[DictionaryCompetence].[Name],
				[DictionaryCompetence].[Description],
				[DictionaryCompetence].[Order],
				[DictionaryCompetence].[Audit_CreatedBy],
				[DictionaryCompetence].[Audit_CreatedOn],
				[DictionaryCompetence].[Audit_ModifiedBy],
				[DictionaryCompetence].[Audit_ModifiedOn],
				[DictionaryCompetence].[Audit_DeletedBy],
				[DictionaryCompetence].[Audit_DeletedOn],
				[DictionaryCompetence].[Audit_IsDeleted],
				[DictionaryCompetence].[Audit_VersionId],
				[DictionaryCompetence].[LegacyId],
				[DictionaryClusterAdminView].[DictionaryId],
				[DictionaryClusterAdminView].[DictionaryName],
				[DictionaryCompetence].[DictionaryClusterId],
				[DictionaryClusterAdminView].[Name]									AS	[DictionaryClusterName],
				[DictionaryClusterAdminView].[DictionaryNumberOfUsages],
				CAST([DictionaryClusterAdminView].[DictionaryIsLive] AS BIT)		AS	[DictionaryIsLive]

	FROM		[DictionaryCompetence]	WITH (NOLOCK)

	INNER JOIN	[DictionaryClusterAdminView]
		ON		[DictionaryClusterAdminView].[Id] = [DictionaryCompetence].[DictionaryClusterId]

	WHERE		[DictionaryCompetence].[Audit_IsDeleted] = 0