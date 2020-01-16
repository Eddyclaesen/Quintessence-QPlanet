CREATE VIEW [dbo].[DictionaryLevelTranslationView] AS
	SELECT		*
	FROM		[dbo].[DictionaryLevelTranslation]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0