CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListClusterScoresByProjectCandidateId]
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[ProjectCandidateClusterScoreView].[Id],
				ISNULL([DictionaryClusterTranslationView].[Text],[DictionaryClusterView].[Name]) AS [Text],
				ISNULL([DictionaryClusterTranslationView].[Description],[DictionaryClusterView].[Description]) AS [Description],
				[ProjectCandidateClusterScoreView].[Score],
				[ProjectCandidateClusterScoreView].[Statement],
				[DictionaryClusterView].[Color],
				[DictionaryClusterView].[Order],
				[DictionaryClusterView].[ImageName]

	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCandidateClusterScoreView]
		ON		[ProjectCandidateClusterScoreView].[ProjectCandidateId] = [ProjectCandidateView].[Id]
	
	LEFT JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [ProjectCandidateClusterScoreView].[DictionaryClusterId]
		AND		[DictionaryClusterTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [ProjectCandidateClusterScoreView].[DictionaryClusterId]

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId

	ORDER BY	[DictionaryClusterView].[Order]
END
GO
