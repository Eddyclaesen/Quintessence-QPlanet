﻿ALTER TABLE dbo.CrmReplicationAppointmentTimesheet
ADD TaskDescription NVARCHAR(150) NULL

GO

UPDATE [CrmReplicationAppointmentTimesheet]
SET [TaskDescription] = (
SELECT [name] 
FROM [SuperOffice7].[CRM7].[APPOINTMENT]
LEFT JOIN [SuperOffice7].[CRM7].[TASK]
ON [APPOINTMENT].[task_idx] = [TASK].[Task_id]
WHERE [APPOINTMENT].[appointment_id] = [CrmReplicationAppointmentTimesheet].[Id]
)

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CrmReplicationAppointmentTimesheet_Sync]
AS
BEGIN
	DECLARE @CrmProjectTable AS TABLE(Id INT)
	INSERT INTO @CrmProjectTable
		SELECT	DISTINCT CrmProjectId FROM [dbo].[Project2CrmProjectView]
	
	DECLARE	@Appointment	AS	TABLE(
								[Id]					INT,
								[ProjectId]				INT,
								[AssociateId]			INT,
								[StartDate]				DATETIME2,
								[EndDate]				DATETIME2,
								[Description]			NVARCHAR(MAX),
								[TaskDescription] NVARCHAR(150))

	INSERT INTO @Appointment([Id],
							 [ProjectId],
							 [AssociateId],
							 [StartDate],
							 [EndDate],
							 [Description],
							 [TaskDescription])
		SELECT		DISTINCT
					[Appointment].[appointment_id]						AS	[Id],
					[Appointment].[project_id]							AS  [ProjectId],
					[Appointment].[associate_id]						AS  [AssociateId],
					CAST([Appointment].[do_by] AS DATETIME2)			AS  [StartDate],
					CAST([Appointment].[endDate] AS DATETIME2)			AS  [EndDate],
					COALESCE([Text].[text], 'No description')			AS	[Description],
					[TASK].[name]										AS	[TaskDescription]

		FROM		[Superoffice7].[CRM7].[APPOINTMENT] [Appointment]

		LEFT JOIN	[Superoffice7].[CRM7].[TEXT] [Text]
			ON		[Text].[text_id] = [Appointment].[text_id]
		LEFT JOIN   [SuperOffice7].[CRM7].[TASK]
			ON		[TASK].[Task_id] = [Appointment].[task_idx]

		WHERE		[Appointment].[project_id] IN (SELECT Id FROM @CrmProjectTable)
			AND		[Appointment].[Status] <> 13
			--AND		[Appointment].do_by BETWEEN DATEADD(MONTH, -3, GETDATE()) AND GETDATE()
		
	MERGE [CrmReplicationAppointmentTimesheet] AS [TargetDatabase]
	USING @Appointment AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]	
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[ProjectId] =			[SourceDatabase].[ProjectId],
					[TargetDatabase].[AssociateId] =		[SourceDatabase].[AssociateId],
					[TargetDatabase].[StartDate] =			[SourceDatabase].[StartDate],
					[TargetDatabase].[EndDate] =			[SourceDatabase].[EndDate],
					[TargetDatabase].[Description] =		[SourceDatabase].[Description],
					[TargetDatabase].[TaskDescription] =	[SourceDatabase].[TaskDescription]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[ProjectId],
				[AssociateId],
				[StartDate],
				[EndDate],
				[Description],
				[TaskDescription]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[ProjectId],
				[SourceDatabase].[AssociateId],
				[SourceDatabase].[StartDate],
				[SourceDatabase].[EndDate],
				[SourceDatabase].[Description],
				[SourceDatabase].[TaskDescription]);
END

GO

ALTER VIEW [dbo].[TimesheetEntryView] AS
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
GO

ALTER VIEW [dbo].[CrmTimesheetUnregisteredEntryView]
AS
	SELECT		[CrmReplicationAppointmentTimesheet].[Id]					AS	[Id],
				[UserView].[Id]												AS	[UserId],
				[Project2CrmProjectView].[ProjectId]						AS	[ProjectId],
				[CrmReplicationAppointmentTimesheet].[StartDate]			AS	[StartDate],
				[CrmReplicationAppointmentTimesheet].[EndDate]				AS	[EndDate],			
				[CrmReplicationAppointmentTimesheet].[Description]			AS	[Description],
				[CrmReplicationAppointmentTimesheet].[TaskDescription]		AS	[Category]

	FROM		[CrmReplicationAppointmentTimesheet]

	INNER JOIN	[UserView]
		ON		[UserView].[AssociateId] = [CrmReplicationAppointmentTimesheet].[AssociateId]

	INNER JOIN	[Project2CrmProjectView]
		ON		[Project2CrmProjectView].[CrmProjectId] = [CrmReplicationAppointmentTimesheet].[ProjectId]

	LEFT JOIN	[TimesheetEntryView]
		ON		[TimesheetEntryView].[AppointmentId] = [CrmReplicationAppointmentTimesheet].[Id]

	WHERE		[TimesheetEntryView].[Id] IS NULL
GO

ALTER TABLE dbo.ProjectCategoryDetailType1
ADD WithReport BIT NOT NULL DEFAULT 0

GO
