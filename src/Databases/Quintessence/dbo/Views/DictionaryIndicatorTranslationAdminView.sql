CREATE VIEW [dbo].[DictionaryIndicatorTranslationAdminView] AS
	SELECT		[DictionaryIndicatorTranslation].[Id],
				[DictionaryIndicatorTranslation].[LanguageId],
				[LanguageView].[Name]										AS	[LanguageName],
				[DictionaryIndicatorTranslation].[DictionaryIndicatorId]	AS	[DictionaryIndicatorAdminId],
				[DictionaryIndicatorTranslation].[Text],
				[DictionaryIndicatorTranslation].[Audit_CreatedBy],
				[DictionaryIndicatorTranslation].[Audit_CreatedOn],
				[DictionaryIndicatorTranslation].[Audit_ModifiedBy],
				[DictionaryIndicatorTranslation].[Audit_ModifiedOn],
				[DictionaryIndicatorTranslation].[Audit_DeletedBy],
				[DictionaryIndicatorTranslation].[Audit_DeletedOn],
				[DictionaryIndicatorTranslation].[Audit_IsDeleted],
				[DictionaryIndicatorTranslation].[Audit_VersionId]

	FROM		[DictionaryIndicatorTranslation]	WITH (NOLOCK)

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [DictionaryIndicatorTranslation].[LanguageId]

	WHERE		[Audit_IsDeleted] = 0