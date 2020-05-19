CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListCompetenceScoreClustersByProjectCandidateId]
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
SET NOCOUNT ON
	SELECT		DISTINCT
				[DictionaryCompetenceView].[DictionaryClusterId],
				CAST(ISNULL([DictionaryClusterTranslationView].[Text],[DictionaryClusterView].[Name]) AS NVARCHAR(MAX))		AS	[Name],
				[DictionaryClusterView].[Order],
				[DictionaryClusterView].[ImageName]

	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCandidateCompetenceScoreView]
		ON		[ProjectCandidateCompetenceScoreView].[ProjectCandidateId] = [ProjectCandidateView].[Id]

	INNER JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId]

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]
	
	LEFT JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [DictionaryCompetenceView].[DictionaryClusterId]
		AND		[DictionaryClusterTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId

	ORDER BY	[DictionaryClusterView].[Order]
END