CREATE VIEW [SimulationSetView] AS
	SELECT		*
	FROM		[SimulationSet]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0