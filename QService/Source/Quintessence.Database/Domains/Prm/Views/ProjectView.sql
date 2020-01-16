CREATE VIEW [dbo].[ProjectView] AS
	SELECT		*
	FROM		[Project]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0
