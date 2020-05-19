CREATE VIEW [SimulationDepartmentView] AS
	SELECT		*
	FROM		[SimulationDepartment]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0