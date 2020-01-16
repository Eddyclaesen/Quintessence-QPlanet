CREATE VIEW [dbo].[DictionaryLevelAdminView] AS
	SELECT		[DictionaryLevel].[Id],
				[DictionaryLevel].[DictionaryCompetenceId]							AS	[DictionaryCompetenceAdminId],
				[DictionaryLevel].[Name],
				[DictionaryLevel].[Level],
				[DictionaryLevel].[Audit_CreatedBy],
				[DictionaryLevel].[Audit_CreatedOn],
				[DictionaryLevel].[Audit_ModifiedBy],
				[DictionaryLevel].[Audit_ModifiedOn],
				[DictionaryLevel].[Audit_DeletedBy],
				[DictionaryLevel].[Audit_DeletedOn],
				[DictionaryLevel].[Audit_IsDeleted],
				[DictionaryLevel].[Audit_VersionId],
				[DictionaryLevel].[LegacyId],
				[DictionaryCompetenceAdminView].[DictionaryId],
				[DictionaryCompetenceAdminView].[DictionaryName],
				[DictionaryCompetenceAdminView].[DictionaryClusterId],
				[DictionaryCompetenceAdminView].[DictionaryClusterName],
				[DictionaryLevel].[DictionaryCompetenceId],
				[DictionaryCompetenceAdminView].[Name]								AS	[DictionaryCompetenceName],
				[DictionaryCompetenceAdminView].[DictionaryNumberOfUsages],
				CAST([DictionaryCompetenceAdminView].[DictionaryIsLive] AS BIT)		AS	[DictionaryIsLive]

	FROM		[DictionaryLevel]	WITH (NOLOCK)

	INNER JOIN	[DictionaryCompetenceAdminView]
		ON		[DictionaryCompetenceAdminView].[Id] = [DictionaryLevel].[DictionaryCompetenceId]

	WHERE		[DictionaryLevel].[Audit_IsDeleted] = 0