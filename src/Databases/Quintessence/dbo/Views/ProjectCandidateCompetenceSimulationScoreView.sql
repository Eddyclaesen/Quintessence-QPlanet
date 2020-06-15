CREATE VIEW [dbo].[ProjectCandidateCompetenceSimulationScoreView] AS
	SELECT		[ProjectCandidateCompetenceSimulationScore].*,
				COALESCE(NULLIF(CAST([DictionaryCompetenceTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryCompetenceView].[Name])		AS	[DictionaryCompetenceName],
				[DictionaryClusterView].[Id]																	AS	[DictionaryClusterId],
				COALESCE(NULLIF(CAST([DictionaryClusterTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryClusterView].[Name])				AS	[DictionaryClusterName],
				[SimulationSetView].[Id]																		AS	[SimulationSetId],
				[SimulationSetView].[Name]																		AS	[SimulationSetName],
				[SimulationDepartmentView].[Id]																	AS	[SimulationDepartmentId],
				[SimulationDepartmentView].[Name]																AS	[SimulationDepartmentName],
				[SimulationLevelView].[Id]																		AS	[SimulationLevelId],
				[SimulationLevelView].[Name]																	AS	[SimulationLevelName],
				[SimulationView].[Id]																			AS	[SimulationId],
				[SimulationView].[Name]																			AS	[SimulationName]
				
	FROM		[ProjectCandidateCompetenceSimulationScore]	WITH (NOLOCK)

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCompetenceSimulationScore].[ProjectCandidateId]

	INNER JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [ProjectCandidateCompetenceSimulationScore].[DictionaryCompetenceId]

	LEFT JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = [DictionaryCompetenceView].[Id]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]

	LEFT JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [DictionaryClusterView].[Id]
		AND		[DictionaryClusterTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	INNER JOIN	[SimulationCombinationView]
		ON		[SimulationCombinationView].[Id] = [ProjectCandidateCompetenceSimulationScore].[SimulationCombinationId]

	INNER JOIN	[SimulationSetView]
		ON		[SimulationSetView].[Id] = [SimulationCombinationView].[SimulationSetId]

	LEFT JOIN	[SimulationDepartmentView]
		ON		[SimulationDepartmentView].[Id] = [SimulationCombinationView].[SimulationDepartmentId]

	LEFT JOIN	[SimulationLevelView]
		ON		[SimulationLevelView].[Id] = [SimulationCombinationView].[SimulationLevelId]

	INNER JOIN	[SimulationView]
		ON		[SimulationView].[Id] = [SimulationCombinationView].[SimulationId]

	WHERE		[ProjectCandidateCompetenceSimulationScore].[Audit_IsDeleted] = 0