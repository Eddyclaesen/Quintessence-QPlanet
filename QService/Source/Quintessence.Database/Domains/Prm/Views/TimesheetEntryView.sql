CREATE VIEW [dbo].[TimesheetEntryView] AS
	SELECT		[TimesheetEntry].*,
				
				[ActivityProfileView].[ActivityName]	AS	ActivityName,
				[ActivityProfileView].[ProfileName]		AS	ProfileName

	FROM		[TimesheetEntry]	WITH (NOLOCK)

	INNER JOIN	[ActivityProfileView]
		ON		[ActivityProfileView].[Id] = [TimesheetEntry].[ActivityProfileId]

	WHERE		[TimesheetEntry].[Audit_IsDeleted] = 0
