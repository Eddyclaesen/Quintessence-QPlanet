CREATE VIEW [SimulationContextView] AS
	SELECT		*				
	FROM		[SimulationContext]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0