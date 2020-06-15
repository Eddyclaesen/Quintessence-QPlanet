CREATE VIEW [SimulationCombinationView] AS
	SELECT		*,

				dbo.SimulationCombinationLanguage_ConcatenateLanguageNames([Id])		AS		LanguageNames
	FROM		[SimulationCombination]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0