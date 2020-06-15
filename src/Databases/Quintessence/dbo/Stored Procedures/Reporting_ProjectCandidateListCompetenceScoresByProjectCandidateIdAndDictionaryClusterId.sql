CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListCompetenceScoresByProjectCandidateIdAndDictionaryClusterId]
	@ProjectCandidateId	UNIQUEIDENTIFIER,
	@DictionaryClusterId UNIQUEIDENTIFIER
AS
BEGIN
SET NOCOUNT ON
	SELECT		[ProjectCandidateCompetenceScoreView].[Id],
				ISNULL([DictionaryCompetenceTranslationView].[Text],[DictionaryCompetenceView].[Name]) AS [Text],
				ISNULL([DictionaryCompetenceTranslationView].[Description],[DictionaryCompetenceView].[Description]) AS [Description],
				[ProjectCandidateCompetenceScoreView].[Score],
				[ProjectCandidateCompetenceScoreView].[Statement],
				[DictionaryClusterView].[Color]							AS	[ClusterColor],
				[DictionaryCompetenceView].[Order]							AS	[ClusterOrder],
				[DictionaryClusterView].[ImageName]						AS	[ClusterImageName]

	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCandidateCompetenceScoreView]
		ON		[ProjectCandidateCompetenceScoreView].[ProjectCandidateId] = [ProjectCandidateView].[Id]
	
	LEFT JOIN	[DictionaryCompetenceTranslationView]
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