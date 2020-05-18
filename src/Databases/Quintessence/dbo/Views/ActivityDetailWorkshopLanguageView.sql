CREATE VIEW [dbo].[ActivityDetailWorkshopLanguageView] AS
	SELECT		[ActivityDetailWorkshopLanguage].*,
				[LanguageView].[Name]				AS	LanguageName

	FROM		[ActivityDetailWorkshopLanguage]	WITH (NOLOCK)

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [ActivityDetailWorkshopLanguage].[LanguageId]

	WHERE		[ActivityDetailWorkshopLanguage].[Audit_IsDeleted] = 0