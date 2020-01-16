CREATE VIEW [dbo].[DictionaryView] AS
	SELECT		*
	FROM		[dbo].[Dictionary]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0