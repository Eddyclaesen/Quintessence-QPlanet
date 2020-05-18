CREATE VIEW [dbo].[ActivityDetailTrainingLanguageView] AS
	SELECT		[ActivityDetailTrainingLanguage].*,
				[LanguageView].[Name]				AS	LanguageName

	FROM		[ActivityDetailTrainingLanguage]	WITH (NOLOCK)

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [ActivityDetailTrainingLanguage].[LanguageId]

	WHERE		[ActivityDetailTrainingLanguage].[Audit_IsDeleted] = 0