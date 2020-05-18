CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListCompetences]
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
SET NOCOUNT ON
	SELECT		DISTINCT
				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId]		AS	[DictionaryCompetenceId],
				CAST([DictionaryCompetenceTranslationView].[Text] AS NVARCHAR(MAX))			AS	[DictionaryCompetenceName],
				[DictionaryClusterView].[Color]												AS	[Color],
				[DictionaryClusterView].[Order]												AS	[Order],
				CAST([DictionaryClusterTranslationView].[Text] AS NVARCHAR(MAX))			AS	[DictionaryClusterName]

	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[ProjectCategoryDetailDictionaryIndicatorView]
		ON		[ProjectCategoryDetailDictionaryIndicatorView].[ProjectCategoryDetailId] = [ProjectCategoryDetailView].[Id]

	INNER JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId]
		
	INNER JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = [DictionaryCompetenceView].[Id]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]

	INNER JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [DictionaryClusterView].[Id]
		AND		[DictionaryClusterTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId

	ORDER BY	[DictionaryClusterView].[Order], CAST([DictionaryCompetenceTranslationView].[Text] AS NVARCHAR(MAX))
END