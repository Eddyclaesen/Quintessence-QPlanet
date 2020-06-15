CREATE VIEW [SimulationContextUserView] AS
	SELECT		*				
	FROM		[SimulationContextUser]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0