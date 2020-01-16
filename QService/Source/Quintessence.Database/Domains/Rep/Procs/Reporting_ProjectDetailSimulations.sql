CREATE PROCEDURE Reporting_ProjectDetailSimulations
	@ProjectId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[ProjectCategoryDetailSimulationCombinationView].[SimulationSetName],
				[ProjectCategoryDetailSimulationCombinationView].[SimulationDepartmentName],
				[ProjectCategoryDetailSimulationCombinationView].[SimulationLevelName],
				[ProjectCategoryDetailSimulationCombinationView].[SimulationName],
				[ProjectCategoryDetailSimulationCombinationView].[Preparation],
				[ProjectCategoryDetailSimulationCombinationView].[Execution]

	FROM		[ProjectCategoryDetailSimulationCombinationView]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailSimulationCombinationView].[ProjectCategoryDetailId]

	WHERE		[ProjectCategoryDetailView].[ProjectId] = @ProjectId
END
GO
