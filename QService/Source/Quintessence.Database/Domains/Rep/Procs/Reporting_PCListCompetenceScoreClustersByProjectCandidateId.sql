CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListCompetenceScoreClustersByProjectCandidateId]
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		DISTINCT
				[DictionaryCompetenceView].[DictionaryClusterId],
				CAST([DictionaryClusterTranslationView].[Text] AS NVARCHAR(MAX))		AS	[Name],
				[DictionaryClusterView].[Order],
				[DictionaryClusterView].[ImageName]

	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCandidateCompetenceScoreView]
		ON		[ProjectCandidateCompetenceScoreView].[ProjectCandidateId] = [ProjectCandidateView].[Id]

	INNER JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId]

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]
	
	INNER JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [DictionaryCompetenceView].[DictionaryClusterId]
		AND		[DictionaryClusterTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId

	ORDER BY	[DictionaryClusterView].[Order]
END