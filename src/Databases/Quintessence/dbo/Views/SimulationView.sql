CREATE VIEW [SimulationView] AS
	SELECT		*				
	FROM		[Simulation]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0