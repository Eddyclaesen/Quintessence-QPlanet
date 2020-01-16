CREATE PROCEDURE [dbo].[CrmReplicationAppointment_Sync]
AS
BEGIN
	DECLARE @JobHistoryId AS UNIQUEIDENTIFIER
	DECLARE @JobName AS NVARCHAR(MAX)

	SELECT	@JobName = OBJECT_NAME(@@PROCID)

	EXEC [dbo].[CrmReplicationJob_StartProcess] @JobName, @JobHistoryId OUTPUT

	DECLARE	@Appointment	AS	TABLE(
								[Id] INT,
								[AppointmentDate] DATETIME,
								[EndDate] DATETIME,
								[AssociateId] INT,
								[IsReserved] BIT,
								[OfficeId] INT,
								[LanguageId] INT,
								[Gender] VARCHAR(1),
								[Code] VARCHAR(12),
								[FirstName] NVARCHAR(MAX),
								[LastName] NVARCHAR(MAX),
								[CrmProjectId] INT,
								[TaskId] INT,
								[Description] TEXT)

	INSERT INTO @Appointment([Id],
							 [AppointmentDate],
							 [EndDate],
							 [AssociateId],
							 [IsReserved],
							 [OfficeId],
							 [LanguageId],
							 [Gender],
							 [Code],
							 [FirstName],
							 [LastName],
							 [CrmProjectId],
							 [TaskId],
							 [Description])
		SELECT		[Appointment].[appointment_id]																AS	[Id],
					[Appointment].[do_by]																	AS	[AppointmentDate],
					[Appointment].[endDate]																		AS	[EndDate],
					[Appointment].[Associate_Id]																AS	[AssociateId],
					CAST(COALESCE([UDAPPNTSMALL].[long06], 0) AS BIT)											AS	[IsReserved],
					[dbo].[Office_RetrieveOfficeId](ISNULL([UDAPPNTSMALL].[string05], 'QA'))					AS	[OfficeId],
					[dbo].[Language_RetrieveLanguageId](ISNULL([UDAPPNTSMALL].[string06], 'NL'))				AS	[LanguageId],
					[UDAPPNTSMALL].[string07]																	AS	[Gender],
					SUBSTRING([UDAPPNTSMALL].[string08], 1, 12)													AS	[Code],
					[UDAPPNTLARGE].[string45]																	AS	[FirstName],
					[UDAPPNTLARGE].[string46]																	AS	[LastName],
					[Appointment].[project_id]																	AS	[CrmProjectId],
					[Appointment].[task_idx]																	AS	[TaskId],
					[Text].[Text]																				AS	[Description]

		FROM		[SUPEROFFICE7SERVER].[Superoffice7].[dbo].[Appointment] [Appointment]
  
		INNER JOIN	[SUPEROFFICE7SERVER].[Superoffice7].[dbo].[UDAPPNTSMALL] [UDAPPNTSMALL]
			ON		[Appointment].[userdef_id] = [UDAPPNTSMALL].[udappntsmall_id]
			AND		[UDAPPNTSMALL].[string08] IS NOT NULL
			AND		[UDAPPNTSMALL].[string08] <> ''
			AND		[UDAPPNTSMALL].[string08] <> '0'
	
		LEFT JOIN	[SUPEROFFICE7SERVER].[Superoffice7].[dbo].[UDAPPNTLARGE] [UDAPPNTLARGE]
			ON		[Appointment].[userdef2_id] = [UDAPPNTLARGE].[UDAPPNTLARGE_id]

		LEFT JOIN	[SUPEROFFICE7SERVER].[Superoffice7].[dbo].[Text] [Text]
			ON		[Text].[text_id] = [Appointment].[text_id]
		
	MERGE [CrmReplicationAppointment] AS [TargetDatabase]
	USING @Appointment AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]	
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[AppointmentDate] =	[SourceDatabase].[AppointmentDate],
					[TargetDatabase].[EndDate] =			[SourceDatabase].[EndDate],
					[TargetDatabase].[AssociateId] =		[SourceDatabase].[AssociateId],
					[TargetDatabase].[IsReserved] =			[SourceDatabase].[IsReserved],
					[TargetDatabase].[OfficeId] =			[SourceDatabase].[OfficeId],
					[TargetDatabase].[LanguageId] =			[SourceDatabase].[LanguageId],
					[TargetDatabase].[Gender] =				[SourceDatabase].[Gender],
					[TargetDatabase].[Code] =				[SourceDatabase].[Code],
					[TargetDatabase].[FirstName] =			[SourceDatabase].[FirstName],
					[TargetDatabase].[LastName] =			[SourceDatabase].[LastName],
					[TargetDatabase].[CrmProjectId] =		[SourceDatabase].[CrmProjectId],
					[TargetDatabase].[TaskId] =				[SourceDatabase].[TaskId],
					[TargetDatabase].[Description] =		[SourceDatabase].[Description]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[AppointmentDate],
				[EndDate],
				[AssociateId],
				[IsReserved],
				[OfficeId],
				[LanguageId],
				[Gender],
				[Code],
				[FirstName],
				[LastName],
				[CrmProjectId],
				[TaskId],
				[Description]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[AppointmentDate],
				[SourceDatabase].[EndDate],
				[SourceDatabase].[AssociateId],
				[SourceDatabase].[IsReserved],
				[SourceDatabase].[OfficeId],
				[SourceDatabase].[LanguageId],
				[SourceDatabase].[Gender],
				[SourceDatabase].[Code],
				[SourceDatabase].[FirstName],
				[SourceDatabase].[LastName],
				[SourceDatabase].[CrmProjectId],
				[SourceDatabase].[TaskId],
				[SourceDatabase].[Description]);

	EXEC [dbo].[CrmReplicationJob_StopProcess] @JobHistoryId

	--Sync between cultural fit candidate deadlines and SO appointment dates.
	UPDATE		[TheoremListRequest]
	SET			[Deadline] = [CrmReplicationAppointment].[AppointmentDate]
	FROM		[TheoremListRequest]
	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[ProjectId] = [TheoremListRequest].[ProjectId]
		AND		[ProjectCandidateView].[CandidateId] = [TheoremListRequest].[CandidateId]
	INNER JOIN	[CrmReplicationAppointment]
		ON		[CrmReplicationAppointment].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]
END
GO