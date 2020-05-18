CREATE VIEW [dbo].[ActivityTypeView] AS
	SELECT		[ActivityType].*
	FROM		[ActivityType]	WITH (NOLOCK)
	WHERE		[ActivityType].[Audit_IsDeleted] = 0