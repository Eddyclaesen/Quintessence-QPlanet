CREATE PROCEDURE [dbo].[Reporting_ListProjectCompetenceIndicators]
	@ProjectId UNIQUEIDENTIFIER,
	@CompetenceId UNIQUEIDENTIFIER,
	@LanguageId INT
AS
	SELECT		[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryIndicatorId]		AS	IndicatorId,
				[DictionaryIndicatorTranslationView].[Text]									AS	IndicatorName

	FROM		[ProjectCategoryDetailView]

	INNER JOIN	[ProjectCategoryDetailDictionaryIndicatorView]
		ON		[ProjectCategoryDetailDictionaryIndicatorView].[ProjectCategoryDetailId] = [ProjectCategoryDetailView].[Id]

	INNER JOIN	[DictionaryIndicatorTranslationView]
		ON		[DictionaryIndicatorTranslationView].[DictionaryIndicatorId] = [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryIndicatorId]
		AND		[DictionaryIndicatorTranslationView].[LanguageId] = @LanguageId

	WHERE		[ProjectCategoryDetailView].[ProjectId] = @ProjectId
		AND		[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId] = @CompetenceId