
CREATE VIEW [dbo].[UserView] AS 
	SELECT		[User].*,
				[UserProfile].[LanguageId],
				[LanguageView].[Code]		AS	[LanguageCode],
				[LanguageView].[Name]		AS	[LanguageName],
				[RoleView].[Name]			AS	[RoleName],
				[RoleView].[Code]			AS	[RoleCode],
				CAST(CASE 
                  WHEN EXISTS (select * from [Project] WHERE [Project].[ProjectManagerId] = [User].[Id])
                  THEN 1 
                  ELSE 0 
				END AS BIT) AS [IsProjectManager]

	FROM		[User]	WITH (NOLOCK)

	INNER JOIN	[UserProfile]	WITH (NOLOCK) --Use UserProfile in stead of UserProfileView because of recursion UserProfileView uses UserView so, we can't user UserProfileView in UserView
		ON		[UserProfile].[Id] = [User].[Id]
		AND		[UserProfile].[Audit_IsDeleted] = 0

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [UserProfile].[LanguageId]

	LEFT JOIN	[RoleView]
		ON		[RoleView].[Id] = [User].[RoleId]

	WHERE		[User].[Audit_IsDeleted] = 0
		AND		[User].[IsLocked] = 0
