CREATE VIEW [dbo].[DictionaryClusterTranslationAdminView] AS
	SELECT		[DictionaryClusterTranslation].[Id],
				[DictionaryClusterTranslation].[LanguageId],
				[LanguageView].[Name]									AS	[LanguageName],
				[DictionaryClusterTranslation].[DictionaryClusterId]	AS	[DictionaryClusterAdminId],
				[DictionaryClusterTranslation].[Text],
				[DictionaryClusterTranslation].[Description],
				[DictionaryClusterTranslation].[Audit_CreatedBy],
				[DictionaryClusterTranslation].[Audit_CreatedOn],
				[DictionaryClusterTranslation].[Audit_ModifiedBy],
				[DictionaryClusterTranslation].[Audit_ModifiedOn],
				[DictionaryClusterTranslation].[Audit_DeletedBy],	
				[DictionaryClusterTranslation].[Audit_DeletedOn],	
				[DictionaryClusterTranslation].[Audit_IsDeleted],	
				[DictionaryClusterTranslation].[Audit_VersionId]

	FROM		[DictionaryClusterTranslation]	WITH (NOLOCK)

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [DictionaryClusterTranslation].[LanguageId]
	
	WHERE		[Audit_IsDeleted] = 0