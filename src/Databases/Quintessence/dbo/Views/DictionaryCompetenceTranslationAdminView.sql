CREATE VIEW [dbo].[DictionaryCompetenceTranslationAdminView] AS
	SELECT		[DictionaryCompetenceTranslation].[Id],
				[DictionaryCompetenceTranslation].[LanguageId],
				[LanguageView].[Name]										AS	[LanguageName],
				[DictionaryCompetenceTranslation].[DictionaryCompetenceId]	AS	[DictionaryCompetenceAdminId],
				[DictionaryCompetenceTranslation].[Text],
				[DictionaryCompetenceTranslation].[Description],
				[DictionaryCompetenceTranslation].[Audit_CreatedBy],
				[DictionaryCompetenceTranslation].[Audit_CreatedOn],
				[DictionaryCompetenceTranslation].[Audit_ModifiedBy],
				[DictionaryCompetenceTranslation].[Audit_ModifiedOn],
				[DictionaryCompetenceTranslation].[Audit_DeletedBy],
				[DictionaryCompetenceTranslation].[Audit_DeletedOn],
				[DictionaryCompetenceTranslation].[Audit_IsDeleted],
				[DictionaryCompetenceTranslation].[Audit_VersionId]

	FROM		[DictionaryCompetenceTranslation]	WITH (NOLOCK)

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [DictionaryCompetenceTranslation].[LanguageId]

	WHERE		[Audit_IsDeleted] = 0