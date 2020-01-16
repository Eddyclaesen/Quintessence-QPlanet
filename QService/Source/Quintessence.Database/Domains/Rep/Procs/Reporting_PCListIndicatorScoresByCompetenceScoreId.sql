CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListIndicatorScoresByCompetenceScoreId]
	@ProjectCandidateCompetenceScoreId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[ProjectCandidateIndicatorScoreView].[Id],
				ISNULL([DictionaryIndicatorTranslationView].[Text],DictionaryIndicatorView.Name) AS [Text],
				[ProjectCandidateIndicatorScoreView].[Score]

	FROM		[ProjectCandidateIndicatorScoreView]

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateIndicatorScoreView].[ProjectCandidateId]

	INNER JOIN	DictionaryIndicatorView
		ON		[ProjectCandidateIndicatorScoreView].[DictionaryIndicatorId] = DictionaryIndicatorView.Id

	LEFT JOIN	[DictionaryIndicatorTranslationView]
		ON		[DictionaryIndicatorTranslationView].[DictionaryIndicatorId] = [ProjectCandidateIndicatorScoreView].[DictionaryIndicatorId]
		AND		[DictionaryIndicatorTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	WHERE		[ProjectCandidateIndicatorScoreView].[ProjectCandidateCompetenceScoreId] = @ProjectCandidateCompetenceScoreId
END
GO
