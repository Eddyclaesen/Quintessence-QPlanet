CREATE PROCEDURE Dictionary_ListDictionaryIndicatorMatrixEntries
	@DictionaryId			UNIQUEIDENTIFIER,
	@LanguageId				INT	= NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		[DictionaryIndicatorMatrixEntryView].[Id],
				COALESCE(NULLIF(CAST([DictionaryIndicatorTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryIndicatorMatrixEntryView].[DictionaryIndicatorName])	AS	[DictionaryIndicatorName],
				[DictionaryIndicatorMatrixEntryView].[DictionaryIndicatorOrder],
				[DictionaryIndicatorMatrixEntryView].[DictionaryLevelId],
				COALESCE(NULLIF(CAST([DictionaryLevelTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryIndicatorMatrixEntryView].[DictionaryLevelName])			AS	[DictionaryLevelName],
				[DictionaryIndicatorMatrixEntryView].[DictionaryLevelLevel]																AS	[DictionaryLevelLevel],
				[DictionaryIndicatorMatrixEntryView].[DictionaryCompetenceId],
				COALESCE(NULLIF(CAST([DictionaryCompetenceTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryIndicatorMatrixEntryView].[DictionaryCompetenceName])	AS	[DictionaryCompetenceName],
				[DictionaryIndicatorMatrixEntryView].[DictionaryCompetenceOrder],
				[DictionaryIndicatorMatrixEntryView].[DictionaryClusterId],
				COALESCE(NULLIF(CAST([DictionaryClusterTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryIndicatorMatrixEntryView].[DictionaryClusterName])		AS	[DictionaryClusterName],
				[DictionaryIndicatorMatrixEntryView].[DictionaryClusterOrder],
				[DictionaryIndicatorMatrixEntryView].[DictionaryId],
				[DictionaryIndicatorMatrixEntryView].[DictionaryName]

	FROM		[DictionaryIndicatorMatrixEntryView]

	LEFT JOIN	[DictionaryIndicatorTranslationView]
		ON		[DictionaryIndicatorTranslationView].[DictionaryIndicatorId] = [DictionaryIndicatorMatrixEntryView].[Id]
		AND		[DictionaryIndicatorTranslationView].[LanguageId] = @LanguageId

	LEFT JOIN	[DictionaryLevelTranslationView]
		ON		[DictionaryLevelTranslationView].[DictionaryLevelId] = [DictionaryIndicatorMatrixEntryView].[DictionaryLevelId]
		AND		[DictionaryLevelTranslationView].[LanguageId] = @LanguageId

	LEFT JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = [DictionaryIndicatorMatrixEntryView].[DictionaryCompetenceId]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = @LanguageId

	LEFT JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [DictionaryIndicatorMatrixEntryView].[DictionaryClusterId]
		AND		[DictionaryClusterTranslationView].[LanguageId] = @LanguageId

	WHERE		[DictionaryIndicatorMatrixEntryView].[DictionaryId] = @DictionaryId
END
GO
	