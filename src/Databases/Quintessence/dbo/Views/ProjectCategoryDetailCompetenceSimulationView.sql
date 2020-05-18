CREATE VIEW [dbo].[ProjectCategoryDetailCompetenceSimulationView] AS
	SELECT		DISTINCT
				[ProjectCategoryDetail2Competence2Combination].[Id]								AS	Id,
				[ProjectCategoryDetail2Competence2Combination].[ProjectCategoryDetailId]		AS	ProjectCategoryDetailId,
				[ProjectCategoryDetail2Competence2Combination].[DictionaryCompetenceId]			AS	DictionaryCompetenceId,
				[ProjectCategoryDetail2Competence2Combination].[SimulationCombinationId]		AS	SimulationCombinationId

	FROM		[ProjectCategoryDetail2Competence2Combination]

	INNER JOIN	[ProjectCategoryDetailDictionaryIndicatorView]
		ON		[ProjectCategoryDetailDictionaryIndicatorView].[ProjectCategoryDetailId] = [ProjectCategoryDetail2Competence2Combination].[ProjectCategoryDetailId]
		AND		[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId] = [ProjectCategoryDetail2Competence2Combination].[DictionaryCompetenceId]

	INNER JOIN	[ProjectCategoryDetailSimulationCombinationView]
		ON		[ProjectCategoryDetailSimulationCombinationView].[ProjectCategoryDetailId] = [ProjectCategoryDetail2Competence2Combination].[ProjectCategoryDetailId]
		AND		[ProjectCategoryDetailSimulationCombinationView].[SimulationCombinationId] = [ProjectCategoryDetail2Competence2Combination].[SimulationCombinationId]