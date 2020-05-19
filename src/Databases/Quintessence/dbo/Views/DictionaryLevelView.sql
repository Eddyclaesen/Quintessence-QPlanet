CREATE VIEW [dbo].[DictionaryLevelView] AS
	SELECT		*
	FROM		[dbo].[DictionaryLevel]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0