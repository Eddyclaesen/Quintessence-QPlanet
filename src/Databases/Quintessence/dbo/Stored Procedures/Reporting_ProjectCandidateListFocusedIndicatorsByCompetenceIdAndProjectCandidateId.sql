CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListFocusedIndicatorsByCompetenceIdAndProjectCandidateId]
	@ProjectCandidateId	UNIQUEIDENTIFIER,
	@DictionaryCompetenceId UNIQUEIDENTIFIER
AS
BEGIN
SET NOCOUNT ON
	SELECT		[DictionaryIndicatorTranslationView].[Text]						AS	[DictionaryIndicatorText],
				[ProjectCategoryDetailDictionaryIndicatorView].[IsStandard]		AS	[IsStandard],
				[ProjectCategoryDetailDictionaryIndicatorView].[IsDistinctive]	AS	[IsDistinctive]

	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[ProjectCategoryDetailDictionaryIndicatorView]
		ON		[ProjectCategoryDetailDictionaryIndicatorView].[ProjectCategoryDetailId] = [ProjectCategoryDetailView].[Id]
		AND		[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId] = @DictionaryCompetenceId

	INNER JOIN	[DictionaryIndicatorTranslationView]
		ON		[DictionaryIndicatorTranslationView].[DictionaryIndicatorId] = [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryIndicatorId]
		AND		[DictionaryIndicatorTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId
END