CREATE VIEW [dbo].[ActivityTypeProfileView] AS
	SELECT		[ActivityType2Profile].*,
				[ActivityTypeView].[Name]	AS	ActivityTypeName,
				[ProfileView].[Name]		AS	ProfileName

	FROM		[ActivityType2Profile]	WITH (NOLOCK)

	INNER JOIN	[ActivityTypeView]	
		ON		[ActivityTypeView].[Id] = [ActivityType2Profile].[ActivityTypeId]

	INNER JOIN	[ProfileView]
		ON		[ProfileView].[Id] = [ActivityType2Profile].[ProfileId]

	WHERE		[ActivityType2Profile].[Audit_IsDeleted] = 0