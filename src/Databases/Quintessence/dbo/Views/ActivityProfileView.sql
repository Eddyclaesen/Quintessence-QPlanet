CREATE VIEW [dbo].[ActivityProfileView] AS
	SELECT		[ActivityProfile].*,

				[ProfileView].[Name]		AS	ProfileName,
				[ActivityView].[Name]		AS	ActivityName

	FROM		[ActivityProfile]	WITH (NOLOCK)

	INNER JOIN	[ProfileView]
		ON		[ProfileView].[Id] = [ActivityProfile].[ProfileId]

	INNER JOIN	[ActivityView]
		ON		[ActivityView].[Id] = [ActivityProfile].[ActivityId]

	WHERE		[ActivityProfile].[Audit_IsDeleted] = 0