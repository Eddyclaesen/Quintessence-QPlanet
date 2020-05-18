CREATE VIEW [dbo].[ProjectDnaCommercialTranslationView] AS
	SELECT		[ProjectDnaCommercialTranslation].*,
				[LanguageView].[Name]					AS	[LanguageName]

	FROM		[ProjectDnaCommercialTranslation]	WITH (NOLOCK)

	LEFT JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [ProjectDnaCommercialTranslation].[LanguageId]

	WHERE		[ProjectDnaCommercialTranslation].[Audit_IsDeleted] = 0