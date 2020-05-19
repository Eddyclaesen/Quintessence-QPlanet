CREATE PROCEDURE Simulation_ListSimulationCombinationLanguages
	@SimulationCombinationId			UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[Language].[Id]													AS	LanguageId,
				[Language].[Name]												AS	LanguageName,
				[SimulationCombination2Language].[SimulationCombinationId]		AS	SimulationCombinationId
	
	FROM		[Language]
	
	LEFT JOIN	[SimulationCombination2Language]
		ON		[SimulationCombination2Language].[LanguageId] = [Language].[Id]
		AND		[SimulationCombination2Language].[SimulationCombinationId] = @SimulationCombinationId
END