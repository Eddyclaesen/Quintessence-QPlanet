
CREATE VIEW [dbo].[TimesheetEntryView] AS
	SELECT		[TimesheetEntry].*,				
				[ActivityProfileView].[ActivityName]	AS	ActivityName,
				[ActivityProfileView].[ProfileName]		AS	ProfileName,
				[CrmReplicationAppointmentTimesheet].[TaskDescription]	AS	Category

	FROM		[TimesheetEntry]	WITH (NOLOCK)

	INNER JOIN	[ActivityProfileView]
		ON		[ActivityProfileView].[Id] = [TimesheetEntry].[ActivityProfileId]
	LEFT JOIN   [CrmReplicationAppointmentTimesheet]
		ON		[CrmReplicationAppointmentTimesheet].[Id] = [TimesheetEntry].[AppointmentId]

	WHERE		[TimesheetEntry].[Audit_IsDeleted] = 0
