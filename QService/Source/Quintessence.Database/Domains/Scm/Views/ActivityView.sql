CREATE VIEW [dbo].[ActivityView] AS
	SELECT		[Activity].*,
				[ActivityTypeView].[Name]		AS	ActivityTypeName

	FROM		[Activity]	WITH (NOLOCK)

	INNER JOIN	[ActivityTypeView]
		ON		[ActivityTypeView].[Id] = [Activity].[ActivityTypeId]

	WHERE		[Activity].[Audit_IsDeleted] = 0