CREATE VIEW [dbo].[UserProfileContactView] AS
	SELECT		[UserProfile2Contact].[ContactId]		AS	Id,
				[UserProfile2Contact].[UserProfileId]	AS	UserProfileId,
				[CrmContactView].[name]					AS	ContactName,
				[CrmContactView].[department]			AS	ContactDepartment

	FROM		[UserProfile2Contact]	WITH (NOLOCK)

	INNER JOIN	[UserProfileView]
		ON		[UserProfileView].[Id] = [UserProfile2Contact].[UserProfileId]

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [UserProfile2Contact].[ContactId]
