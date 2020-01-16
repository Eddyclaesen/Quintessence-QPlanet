CREATE VIEW [dbo].[TranslationView] AS
	SELECT		*
	FROM		[dbo].[Translation]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0