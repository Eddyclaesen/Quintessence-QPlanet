CREATE VIEW [dbo].[ProjectCandidateIndicatorSimulationScoreView] AS
	SELECT		[ProjectCandidateIndicatorSimulationScore].*,
				COALESCE(NULLIF(CAST([DictionaryIndicatorTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryIndicatorView].[Name])		AS	[DictionaryIndicatorName],
				[DictionaryIndicatorView].[Order]															AS	[DictionaryIndicatorOrder],

				[DictionaryLevelView].[Id]																	AS	[DictionaryLevelId],
				COALESCE(NULLIF(CAST([DictionaryLevelTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryLevelView].[Name])				AS	[DictionaryLevelName],
				[DictionaryLevelView].[Level]																AS	[DictionaryLevelLevel],

				[DictionaryCompetenceView].[Id]																AS	[DictionaryCompetenceId],
				COALESCE(NULLIF(CAST([DictionaryCompetenceTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryCompetenceView].[Name])	AS	[DictionaryCompetenceName],
				[DictionaryCompetenceView].[Order]															AS	[DictionaryCompetenceOrder],

				[DictionaryClusterView].[Id]																AS	[DictionaryClusterId],
				COALESCE(NULLIF(CAST([DictionaryClusterTranslationView].[Text] AS NVARCHAR(MAX)), ''), [DictionaryClusterView].[Name])			AS	[DictionaryClusterName],
				[DictionaryClusterView].[Order]																AS	[DictionaryClusterOrder],

				[SimulationSetView].[Id]																	AS	[SimulationSetId],
				[SimulationSetView].[Name]																	AS	[SimulationSetName],
				[SimulationDepartmentView].[Id]																AS	[SimulationDepartmentId],
				[SimulationDepartmentView].[Name]															AS	[SimulationDepartmentName],
				[SimulationLevelView].[Id]																	AS	[SimulationLevelId],
				[SimulationLevelView].[Name]																AS	[SimulationLevelName],
				[SimulationView].[Id]																		AS	[SimulationId],
				[SimulationView].[Name]																		AS	[SimulationName]
				
	FROM		[ProjectCandidateIndicatorSimulationScore]	WITH (NOLOCK)

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateIndicatorSimulationScore].[ProjectCandidateId]

	INNER JOIN	[DictionaryIndicatorView]
		ON		[DictionaryIndicatorView].[Id] = [ProjectCandidateIndicatorSimulationScore].[DictionaryIndicatorId]

	LEFT JOIN	[DictionaryIndicatorTranslationView]
		ON		[DictionaryIndicatorTranslationView].[DictionaryIndicatorId] = [DictionaryIndicatorView].[Id]
		AND		[DictionaryIndicatorTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	INNER JOIN	[DictionaryLevelView]
		ON		[DictionaryLevelView].[Id] = [DictionaryIndicatorView].[DictionaryLevelId]

	LEFT JOIN	[DictionaryLevelTranslationView]
		ON		[DictionaryLevelTranslationView].[DictionaryLevelId] = [DictionaryLevelView].[Id]
		AND		[DictionaryLevelTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	INNER JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [DictionaryLevelView].[DictionaryCompetenceId]

	LEFT JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = [DictionaryCompetenceView].[Id]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]

	LEFT JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [DictionaryClusterView].[Id]
		AND		[DictionaryClusterTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	INNER JOIN	[SimulationCombinationView]
		ON		[SimulationCombinationView].[Id] = [ProjectCandidateIndicatorSimulationScore].[SimulationCombinationId]

	INNER JOIN	[SimulationSetView]
		ON		[SimulationSetView].[Id] = [SimulationCombinationView].[SimulationSetId]

	LEFT JOIN	[SimulationDepartmentView]
		ON		[SimulationDepartmentView].[Id] = [SimulationCombinationView].[SimulationDepartmentId]

	LEFT JOIN	[SimulationLevelView]
		ON		[SimulationLevelView].[Id] = [SimulationCombinationView].[SimulationLevelId]

	INNER JOIN	[SimulationView]
		ON		[SimulationView].[Id] = [SimulationCombinationView].[SimulationId]

	WHERE		[ProjectCandidateIndicatorSimulationScore].[Audit_IsDeleted] = 0