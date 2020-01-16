CREATE VIEW [SimulationLevelView] AS
	SELECT		*
	FROM		[SimulationLevel]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0