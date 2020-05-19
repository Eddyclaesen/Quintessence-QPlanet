CREATE VIEW [dbo].[DictionaryIndicatorTranslationView] AS
	SELECT		*
	FROM		[dbo].[DictionaryIndicatorTranslation]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0