CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListCompetenceScoresByProjectCandidateIdAndDictionaryClusterId]
	@ProjectCandidateId	UNIQUEIDENTIFIER,
	@DictionaryClusterId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[ProjectCandidateCompetenceScoreView].[Id],
				[DictionaryCompetenceTranslationView].[Text],
				[DictionaryCompetenceTranslationView].[Description],
				[ProjectCandidateCompetenceScoreView].[Score],
				[ProjectCandidateCompetenceScoreView].[Statement],
				[DictionaryClusterView].[Color]							AS	[ClusterColor],
				[DictionaryCompetenceView].[Order]							AS	[ClusterOrder],
				[DictionaryClusterView].[ImageName]						AS	[ClusterImageName]

	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCandidateCompetenceScoreView]
		ON		[ProjectCandidateCompetenceScoreView].[ProjectCandidateId] = [ProjectCandidateView].[Id]
	
	INNER JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	INNER JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId]
		AND		[DictionaryCompetenceView].[DictionaryClusterId] = @DictionaryClusterId

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId

	ORDER BY	[DictionaryCompetenceView].[Order]
END