CREATE VIEW [dbo].[CrmTimesheetEntryView]
AS
	SELECT		[CrmReplicationAppointmentTimesheet].[Id]				AS	[Id],
				[UserView].[Id]											AS	[UserId],
				[Project2CrmProjectView].[ProjectId]					AS	[ProjectId],
				[CrmReplicationAppointmentTimesheet].[StartDate]		AS	[StartDate],
				[CrmReplicationAppointmentTimesheet].[EndDate]			AS	[EndDate],			
				[CrmReplicationAppointmentTimesheet].[Description]		AS	[Description]

	FROM		[CrmReplicationAppointmentTimesheet]

	INNER JOIN	[UserView]
		ON		[UserView].[AssociateId] = [CrmReplicationAppointmentTimesheet].[AssociateId]

	INNER JOIN	[Project2CrmProjectView]
		ON		[Project2CrmProjectView].[CrmProjectId] = [CrmReplicationAppointmentTimesheet].[ProjectId]