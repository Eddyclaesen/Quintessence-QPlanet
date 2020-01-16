CREATE VIEW [dbo].[UserProfileView] AS
	SELECT		[UserProfile].*,
				[LanguageView].[Code]		AS	[LanguageCode],
				[LanguageView].[Name]		AS	[LanguageName],
				[UserView].[UserName],
				[UserView].[FirstName],
				[UserView].[LastName]

	FROM		[UserProfile]	WITH (NOLOCK)

	INNER JOIN	[UserView]
		ON		[UserView].[Id] = [UserProfile].[UserId]

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [UserProfile].[LanguageId]

	WHERE		[UserProfile].[Audit_IsDeleted] = 0
