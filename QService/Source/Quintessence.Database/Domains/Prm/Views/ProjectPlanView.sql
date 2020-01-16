CREATE VIEW [dbo].[ProjectPlanView] AS
	SELECT		*

	FROM		[ProjectPlan]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0
