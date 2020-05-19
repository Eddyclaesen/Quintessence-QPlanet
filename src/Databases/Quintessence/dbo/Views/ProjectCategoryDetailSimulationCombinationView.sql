CREATE VIEW [dbo].[ProjectCategoryDetailSimulationCombinationView] AS
	SELECT		[ProjectCategoryDetail2SimulationCombination].[Id]							AS		Id,
				[ProjectCategoryDetail2SimulationCombination].[ProjectCategoryDetailId]		AS		ProjectCategoryDetailId,
				[ProjectCategoryDetail2SimulationCombination].[SimulationCombinationId]		AS		SimulationCombinationId,
				[SimulationMatrixEntryView].[SimulationSetId]								AS		SimulationSetId,
				[SimulationMatrixEntryView].[SimulationSetName]								AS		SimulationSetName,
				[SimulationMatrixEntryView].[SimulationDepartmentId]						AS		SimulationDepartmentId,
				[SimulationMatrixEntryView].[SimulationDepartmentName]						AS		SimulationDepartmentName,
				[SimulationMatrixEntryView].[SimulationLevelId]							AS		SimulationLevelId,
				[SimulationMatrixEntryView].[SimulationLevelName]							AS		SimulationLevelName,
				[SimulationMatrixEntryView].[SimulationId]									AS		SimulationId,
				[SimulationMatrixEntryView].[SimulationName]								AS		SimulationName,
				[SimulationMatrixEntryView].[Preparation]									AS		Preparation,
				[SimulationMatrixEntryView].[Execution]										AS		Execution,

				dbo.SimulationCombinationLanguage_ConcatenateLanguageNames([ProjectCategoryDetail2SimulationCombination].[SimulationCombinationId])
																							AS		LanguageNames

	FROM		[ProjectCategoryDetail2SimulationCombination]

	INNER JOIN	[SimulationMatrixEntryView]
		ON		[SimulationMatrixEntryView].[Id] = [ProjectCategoryDetail2SimulationCombination].[SimulationCombinationId]