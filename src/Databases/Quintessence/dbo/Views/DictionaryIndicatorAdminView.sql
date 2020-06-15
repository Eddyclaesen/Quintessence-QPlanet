CREATE VIEW [dbo].[DictionaryIndicatorAdminView] AS
	SELECT		[DictionaryIndicator].[Id],
				[DictionaryIndicator].[DictionaryLevelId]						AS	[DictionaryLevelAdminId],
				[DictionaryIndicator].[Name],
				[DictionaryIndicator].[IsStandard],
				[DictionaryIndicator].[IsDistinctive],
				[DictionaryIndicator].[Order],
				[DictionaryIndicator].[Audit_CreatedBy],
				[DictionaryIndicator].[Audit_CreatedOn],
				[DictionaryIndicator].[Audit_ModifiedBy],
				[DictionaryIndicator].[Audit_ModifiedOn],
				[DictionaryIndicator].[Audit_DeletedBy],
				[DictionaryIndicator].[Audit_DeletedOn],
				[DictionaryIndicator].[Audit_IsDeleted],
				[DictionaryIndicator].[Audit_VersionId],
				[DictionaryIndicator].[LegacyId],
				[DictionaryLevelAdminView].[DictionaryId],
				[DictionaryLevelAdminView].[DictionaryName],
				[DictionaryLevelAdminView].[DictionaryClusterId],
				[DictionaryLevelAdminView].[DictionaryClusterName],
				[DictionaryLevelAdminView].[DictionaryCompetenceId],
				[DictionaryLevelAdminView].[DictionaryCompetenceName],
				[DictionaryIndicator].[DictionaryLevelId],
				[DictionaryLevelAdminView].[Name]								AS	[DictionaryLevelName],
				[DictionaryLevelAdminView].[Level]								AS	[DictionaryLevelLevel],
				[DictionaryLevelAdminView].[DictionaryNumberOfUsages],
				CAST([DictionaryLevelAdminView].[DictionaryIsLive] AS BIT)		AS	[DictionaryIsLive]

	FROM		[DictionaryIndicator]	WITH (NOLOCK)

	INNER JOIN	[DictionaryLevelAdminView]
		ON		[DictionaryLevelAdminView].[Id] = [DictionaryIndicator].[DictionaryLevelId]

	WHERE		[DictionaryIndicator].[Audit_IsDeleted] = 0