CREATE VIEW [dbo].[ProjectPlanPhaseActivityView] AS
	SELECT		[ProjectPlanPhaseActivity].*,
				[ActivityView].[Name]							AS	ActivityName,
				[ProfileView].[Name]							AS	ProfileName,
				[ActivityProfileView].[DayRate]					AS	DayRate,
				[ActivityProfileView].[HalfDayRate]				AS	HalfDayRate,
				[ActivityProfileView].[HourlyRate]				AS	HourlyRate,
				[ActivityProfileView].[IsolatedHourlyRate]		AS	IsolatedHourlyRate

	FROM		[ProjectPlanPhaseActivity]	WITH (NOLOCK)
	INNER JOIN	[ProjectPlanPhaseEntryView]
		ON		[ProjectPlanPhaseEntryView].[Id] = [ProjectPlanPhaseActivity].[Id]

	INNER JOIN	[ActivityView]
		ON		[ActivityView].[Id] = [ProjectPlanPhaseActivity].[ActivityId]

	INNER JOIN	[ProfileView]
		ON		[ProfileView].[Id] = [ProjectPlanPhaseActivity].[ProfileId]

	INNER JOIN	[ActivityProfileView]
		ON		[ActivityProfileView].[ActivityId] = [ProjectPlanPhaseActivity].[ActivityId]
			AND	[ActivityProfileView].[ProfileId] = [ProjectPlanPhaseActivity].[ProfileId]
