CREATE VIEW [dbo].[ProjectPlanPhaseView] AS
	SELECT		*

	FROM		[ProjectPlanPhase]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0
