CREATE PROCEDURE [dbo].[CrmReplicationAppointmentTraining_Sync]
AS
BEGIN
	DECLARE @JobHistoryId AS UNIQUEIDENTIFIER
	DECLARE @JobName AS NVARCHAR(MAX)

	SELECT	@JobName = OBJECT_NAME(@@PROCID)

	EXEC [dbo].[CrmReplicationJob_StartProcess] @JobName, @JobHistoryId OUTPUT

	DECLARE	@Appointment	AS	TABLE(
								[Id]					INT,
								[ProjectId]				INT,
								[AssociateId]			INT,
								[OfficeId]				INT,
								[LanguageId]			INT,
								[StartDate]				DATETIME,
								[EndDate]				DATETIME,
								[Code]					VARCHAR(12),
								[Description]			NVARCHAR(MAX))

	INSERT INTO @Appointment([Id],
							 [ProjectId],
							 [AssociateId],
							 [OfficeId],
							 [LanguageId],
							 [StartDate],
							 [EndDate],
							 [Code],
							 [Description])
		SELECT		[Appointment].[appointment_id]																AS	[Id],
					[Appointment].[project_id]																	AS  [ProjectId],
					[Appointment].[Associate_Id]																AS	[AssociateId],
					[dbo].[Office_RetrieveOfficeId](ISNULL([UDAPPNTSMALL].[string05], 'QA'))					AS	[OfficeId],
					[dbo].[Language_RetrieveLanguageId](ISNULL([UDAPPNTSMALL].[string06], 'NL'))				AS	[LanguageId],
					[Appointment].[do_by]																	AS  [StartDate],
					[Appointment].[endDate]																		AS  [EndDate],
					[UDAPPNTSMALL].[string08]																	AS	[Code],
					COALESCE([Text].[text], 'No description')													AS	[Description]

		FROM		[$(SUPEROFFICE7SERVER)].[$(Superoffice7)].[dbo].[APPOINTMENT] [Appointment]
  
		INNER JOIN	[$(SuperOffice7Server)].[$(SuperOffice7)].[dbo].[UDAPPNTSMALL] [UDAPPNTSMALL]
			ON		[Appointment].[userdef_id] = [UDAPPNTSMALL].[udappntsmall_id]
			AND		[UDAPPNTSMALL].[string08] IS NOT NULL
			AND		[UDAPPNTSMALL].[string08] <> ''
			AND		[UDAPPNTSMALL].[string08] <> '0'
	
		INNER JOIN	[$(SuperOffice7Server)].[$(SuperOffice7)].[dbo].[UDAPPNTLARGE] [UDAPPNTLARGE]
			ON		[Appointment].[userdef2_id] = [UDAPPNTLARGE].[UDAPPNTLARGE_id]

		LEFT JOIN	[$(SUPEROFFICE7SERVER)].[$(Superoffice7)].[dbo].[TEXT] [Text]
			ON		[Text].[text_id] = [Appointment].[text_id]

		WHERE		[Appointment].[task_idx] IN (49) --49: Vto/Workshop
			AND		[Appointment].[contact_id] <> 3 --Quintessence
		
	MERGE [CrmReplicationAppointmentTraining] AS [TargetDatabase]
	USING @Appointment AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]	
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[ProjectId] =			[SourceDatabase].[ProjectId],
					[TargetDatabase].[AssociateId] =		[SourceDatabase].[AssociateId],
					[TargetDatabase].[OfficeId] =			[SourceDatabase].[OfficeId],
					[TargetDatabase].[LanguageId] =			[SourceDatabase].[LanguageId],
					[TargetDatabase].[StartDate] =			[SourceDatabase].[StartDate],
					[TargetDatabase].[EndDate] =			[SourceDatabase].[EndDate],
					[TargetDatabase].[Code] =				[SourceDatabase].[Code],
					[TargetDatabase].[Description] =		[SourceDatabase].[Description]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[ProjectId],
				[AssociateId],
				[OfficeId],
				[LanguageId],
				[StartDate],
				[EndDate],
				[Code],
				[Description]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[ProjectId],
				[SourceDatabase].[AssociateId],
				[SourceDatabase].[OfficeId],
				[SourceDatabase].[LanguageId],
				[SourceDatabase].[StartDate],
				[SourceDatabase].[EndDate],
				[SourceDatabase].[Code],
				[SourceDatabase].[Description]);

	EXEC [dbo].[CrmReplicationJob_StopProcess] @JobHistoryId
END
GO