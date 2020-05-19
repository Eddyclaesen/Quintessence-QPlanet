CREATE VIEW [dbo].[DictionaryCompetenceTranslationView] AS
	SELECT		*
	FROM		[dbo].[DictionaryCompetenceTranslation]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0