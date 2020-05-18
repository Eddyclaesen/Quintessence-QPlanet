CREATE VIEW [dbo].[ProjectPlanPhaseEntryView] AS
	SELECT		*

	FROM		[ProjectPlanPhaseEntry]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0