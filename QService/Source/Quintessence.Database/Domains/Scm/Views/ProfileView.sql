CREATE VIEW [dbo].[ProfileView] AS
	SELECT		[Profile].*
	FROM		[Profile]	WITH (NOLOCK)
	WHERE		[Profile].[Audit_IsDeleted] = 0