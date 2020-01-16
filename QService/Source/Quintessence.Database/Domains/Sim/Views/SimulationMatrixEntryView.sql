CREATE VIEW [SimulationMatrixEntryView] AS
	SELECT		[SimulationCombinationView].*,
				[SimulationSetView].[Name] AS SimulationSetName,
				[SimulationDepartmentView].[Name] AS SimulationDepartmentName,
				[SimulationLevelView].[Name] AS SimulationLevelName,
				[SimulationView].[Name] AS SimulationName

	FROM		[SimulationCombinationView]

	INNER JOIN	[SimulationSetView]
		ON		[SimulationSetView].[Id] = [SimulationCombinationView].[SimulationSetId]

	LEFT JOIN	[SimulationDepartmentView]
		ON		[SimulationDepartmentView].[Id] = [SimulationCombinationView].[SimulationDepartmentId]

	LEFT JOIN	[SimulationLevelView]
		ON		[SimulationLevelView].[Id] = [SimulationCombinationView].[SimulationLevelId]

	INNER JOIN	[SimulationView]
		ON		[SimulationView].[Id] = [SimulationCombinationView].[SimulationId]