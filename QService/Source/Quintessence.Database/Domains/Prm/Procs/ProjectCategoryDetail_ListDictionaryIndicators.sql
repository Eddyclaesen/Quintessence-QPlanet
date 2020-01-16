CREATE PROCEDURE [dbo].[ProjectCategoryDetail_ListDictionaryIndicators]
	@ProjectCategoryDetailId	UNIQUEIDENTIFIER,
	@LanguageId					INT	= NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		[ProjectCategoryDetailDictionaryIndicatorView].[Id],
				[ProjectCategoryDetailDictionaryIndicatorView].[ProjectCategoryDetailId],

				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryIndicatorId],
				COALESCE(NULLIF(CAST([DictionaryIndicatorTranslationView].[Text] AS NVARCHAR(MAX)), ''), [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryIndicatorName])			AS	[DictionaryIndicatorName],
				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryIndicatorOrder],

				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryLevelId],
				COALESCE(NULLIF(CAST([DictionaryLevelTranslationView].[Text] AS NVARCHAR(MAX)), ''), [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryLevelName])					AS	[DictionaryLevelName],
				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryLevelLevel],

				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId],
				COALESCE(NULLIF(CAST([DictionaryCompetenceTranslationView].[Text] AS NVARCHAR(MAX)), ''), [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceName])		AS	[DictionaryCompetenceName],
				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceOrder],

				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryClusterId],
				COALESCE(NULLIF(CAST([DictionaryClusterTranslationView].[Text] AS NVARCHAR(MAX)), ''), [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryClusterName])				AS	[DictionaryClusterName],
				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryClusterOrder],

				[ProjectCategoryDetailDictionaryIndicatorView].[IsDefinedByRole],
				[ProjectCategoryDetailDictionaryIndicatorView].[IsStandard],
				[ProjectCategoryDetailDictionaryIndicatorView].[IsDistinctive]
	
	FROM		[ProjectCategoryDetailDictionaryIndicatorView]

	LEFT JOIN	[DictionaryIndicatorTranslationView]
		ON		[DictionaryIndicatorTranslationView].[DictionaryIndicatorId] = [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryIndicatorId]
		AND		[DictionaryIndicatorTranslationView].[LanguageId] = @LanguageId

	LEFT JOIN	[DictionaryLevelTranslationView]
		ON		[DictionaryLevelTranslationView].[DictionaryLevelId] = [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryLevelId]
		AND		[DictionaryLevelTranslationView].[LanguageId] = @LanguageId

	LEFT JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = @LanguageId

	LEFT JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryClusterId]
		AND		[DictionaryClusterTranslationView].[LanguageId] = @LanguageId

	WHERE		[ProjectCategoryDetailDictionaryIndicatorView].[ProjectCategoryDetailId] = @ProjectCategoryDetailId
END
GO
