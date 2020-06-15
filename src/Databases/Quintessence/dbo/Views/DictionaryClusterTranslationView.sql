CREATE VIEW [dbo].[DictionaryClusterTranslationView] AS
	SELECT		*
	FROM		[dbo].[DictionaryClusterTranslation]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0