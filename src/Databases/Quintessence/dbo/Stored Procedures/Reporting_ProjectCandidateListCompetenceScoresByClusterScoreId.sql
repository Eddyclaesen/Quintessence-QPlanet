CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListCompetenceScoresByClusterScoreId]
	@ProjectCandidateClusterScoreId	UNIQUEIDENTIFIER
AS
BEGIN
SET NOCOUNT ON
	SELECT		[ProjectCandidateCompetenceScoreView].[Id],
				ISNULL([DictionaryCompetenceTranslationView].[Text],[DictionaryCompetenceView].Name) AS [Text],
				ISNULL([DictionaryCompetenceTranslationView].[Description],[DictionaryCompetenceView].[Description]) AS [Description],
				[ProjectCandidateCompetenceScoreView].[Score],
				ISNULL([DictionaryClusterTranslationView].[Text],[DictionaryClusterView].Name)			AS	[DictionaryClusterName],
				[DictionaryClusterView].[Color]						AS	[DictionaryClusterColor],
				[DictionaryClusterView].[Order]						AS	[DictionaryClusterOrder],
				[DictionaryClusterView].[ImageName]					AS	[DictionaryClusterImageName]

	FROM		[ProjectCandidateCompetenceScoreView]

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCompetenceScoreView].[ProjectCandidateId]

	LEFT JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	LEFT JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId]

	LEFT JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [DictionaryCompetenceView].[DictionaryClusterId]
		AND		[DictionaryClusterTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	LEFT JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryClusterTranslationView].[DictionaryClusterId]

	WHERE		[ProjectCandidateCompetenceScoreView].[ProjectCandidateClusterScoreId] = @ProjectCandidateClusterScoreId

	ORDER BY	[DictionaryCompetenceView].[Order]
END