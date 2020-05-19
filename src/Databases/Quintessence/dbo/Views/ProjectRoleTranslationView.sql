CREATE VIEW [dbo].[ProjectRoleTranslationView] AS
	SELECT		[ProjectRoleTranslation].*,
				[LanguageView].[Name]				AS	[LanguageName]

	FROM		[ProjectRoleTranslation]	WITH	(NOLOCK)

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [ProjectRoleTranslation].[LanguageId]

 	WHERE		[ProjectRoleTranslation].[Audit_IsDeleted] = 0