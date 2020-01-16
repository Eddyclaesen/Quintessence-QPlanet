CREATE PROCEDURE Reporting_ProjectDetailCompetenceMatrix
	@ProjectId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		DISTINCT 
				[ProjectCategoryDetailSimulationCombinationView].[SimulationId],
				[ProjectCategoryDetailSimulationCombinationView].[SimulationName],
				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId],
				[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceName],			
				CASE
					WHEN [ProjectCategoryDetailCompetenceSimulationView].[Id] IS NULL THEN 0
					ELSE 1
				END
			
	FROM		[ProjectCategoryDetailSimulationCombinationView]
	INNER JOIN	[ProjectCategoryDetailDictionaryIndicatorView]
		ON		[ProjectCategoryDetailDictionaryIndicatorView].[ProjectCategoryDetailId] = [ProjectCategoryDetailSimulationCombinationView].[ProjectCategoryDetailId]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailSimulationCombinationView].[ProjectCategoryDetailId]
		AND		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailDictionaryIndicatorView].[ProjectCategoryDetailId]

	LEFT JOIN	[ProjectCategoryDetailCompetenceSimulationView]
		ON		[ProjectCategoryDetailCompetenceSimulationView].[DictionaryCompetenceId] = [ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId]
		AND		[ProjectCategoryDetailCompetenceSimulationView].[SimulationCombinationId] = [ProjectCategoryDetailSimulationCombinationView].[SimulationCombinationId]
		AND		[ProjectCategoryDetailCompetenceSimulationView].[ProjectCategoryDetailId] = [ProjectCategoryDetailView].[Id]

	WHERE		[ProjectCategoryDetailView].[ProjectId] = @ProjectId
END
GO
