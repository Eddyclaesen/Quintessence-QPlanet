CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateListCompetenceScoresByProjectCandidateId]
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
SET NOCOUNT ON
	SELECT		[ProjectCandidateCompetenceScoreView].[Id],
				ISNULL([DictionaryCompetenceTranslationView].[Text],[DictionaryCompetenceView].Name) AS [Text],
				CASE WHEN CAST(ISNULL(DictionaryLevelTranslationView.Text,'') AS nvarchar(max)) = '' THEN ISNULL([DictionaryCompetenceTranslationView].[Description],[DictionaryCompetenceView].[Description])
				ELSE ISNULL(DictionaryLevelTranslationView.[Text],dictionarylevelview.Name)  END AS Description,
				[ProjectCandidateCompetenceScoreView].[Score],
				[ProjectCandidateCompetenceScoreView].[Statement],
				[DictionaryClusterView].[Color]							AS	[ClusterColor],
				[DictionaryClusterView].[Order]							AS	[ClusterOrder],
				[DictionaryClusterView].[ImageName]						AS	[ClusterImageName]

	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCandidateCompetenceScoreView]
		ON		[ProjectCandidateCompetenceScoreView].[ProjectCandidateId] = [ProjectCandidateView].[Id]
	
	LEFT JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	LEFT JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId]

	LEFT JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]

	--ADDED FOR CORRECT LEVEL DEFINITION
	left join dictionarylevelview on [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId] = dictionarylevelview.DictionaryCompetenceId 
		and dictionarylevelview.Level = 
			CASE WHEN dbo.CountLevels(@ProjectCandidateId,[DictionaryCompetenceView].[DictionaryClusterId],[ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId]) > 1 THEN 1
			ELSE (select distinct DictionaryLevelLevel
			from [dbo].[ProjectCandidateIndicatorSimulationScoreView] 
			where projectcandidateid = @ProjectCandidateId
			and DictionaryClusterId = [DictionaryCompetenceView].[DictionaryClusterId]
			and DictionaryCompetenceId = [ProjectCandidateCompetenceScoreView].[DictionaryCompetenceId])
			END

	left join DictionaryLevelTranslationView on dictionarylevelview.Id = DictionaryLevelTranslationView.DictionaryLevelId 
	and DictionaryLevelTranslationView.LanguageId = [ProjectCandidateView].[ReportLanguageId]
	------------------------------------

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId

	ORDER BY	[DictionaryCompetenceView].[Order]
END