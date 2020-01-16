CREATE VIEW [dbo].[DictionaryLevelTranslationAdminView] AS
	SELECT		[DictionaryLevelTranslation].[Id],
				[DictionaryLevelTranslation].[LanguageId],
				[LanguageView].[Name]								AS	[LanguageName],
				[DictionaryLevelTranslation].[DictionaryLevelId]	AS	[DictionaryLevelAdminId],
				[DictionaryLevelTranslation].[Text],
				[DictionaryLevelTranslation].[Audit_CreatedBy],
				[DictionaryLevelTranslation].[Audit_CreatedOn],
				[DictionaryLevelTranslation].[Audit_ModifiedBy],
				[DictionaryLevelTranslation].[Audit_ModifiedOn],
				[DictionaryLevelTranslation].[Audit_DeletedBy],
				[DictionaryLevelTranslation].[Audit_DeletedOn],
				[DictionaryLevelTranslation].[Audit_IsDeleted],
				[DictionaryLevelTranslation].[Audit_VersionId]

	FROM		[DictionaryLevelTranslation]	WITH (NOLOCK)

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [DictionaryLevelTranslation].[LanguageId]

	WHERE		[Audit_IsDeleted] = 0