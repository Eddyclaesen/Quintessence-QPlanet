CREATE VIEW [SimulationCombinationLanguageView] AS
	SELECT		[SimulationCombination2Language].[SimulationCombinationId]		AS	SimulationCombinationId,
				[LanguageView].[Id]												AS	LanguageId,
				[LanguageView].[Name]											AS	LanguageName

	FROM		[SimulationCombination2Language]	WITH (NOLOCK)

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [SimulationCombination2Language].[LanguageId]