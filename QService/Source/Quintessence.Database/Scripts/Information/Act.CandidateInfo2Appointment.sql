--INSERT INTO ACT.dbo.CandidateInfo2Appointment
--	SELECT		CandidateInfo.Kand_Inf_ID, 
--				NULL, 
--				CandidateInfo.Assessor_ID, 
--				CandidateInfo.CoAssessor_ID,
--				CandidateInfo.Voornaam, 
--				CandidateInfo.Naam,
--				CandidateInfo.TaalAC_ID,
--				ProjectToMigrate.ContactId,
--				CASE ProjectToMigrate.ProjectTypeCode
--					WHEN 'AC' THEN 167
--					WHEN 'FA' THEN 168
--					WHEN 'DC' THEN 169
--					WHEN 'FD' THEN 170
--					WHEN 'EA' THEN 171
--					ELSE NULL
--				END,
--				LEFT(CONVERT(nvarchar, DATEADD(DAY, -2, CandidateInfo.Tijd), 120), 11) + N'08:30:00',
--				ProjectToMigrate.CrmProjectId
--	FROM		dbo.Kand_Inf CandidateInfo
--	INNER JOIN	ACT.dbo.ProjectFiche ProjectFiche ON CandidateInfo.ACProject_ID = ProjectFiche.ACProject_ID
--	INNER JOIN	ACT.dbo.ProjectToMigrate ProjectToMigrate ON ProjectToMigrate.ProjectFicheId = ProjectFiche.ACProject_ID
--GO

--SELECT * FROM ACT.dbo.ProjectToMigrate WHERE CrmProjectId = 0

--SELECT * FROM ACT.dbo.CandidateInfo2Appointment

BEGIN TRAN

DECLARE @CandidateInfoId INT
DECLARE @ContactId INT
DECLARE @TaskId INT
DECLARE @FirstName NVARCHAR(MAX)
DECLARE @LastName NVARCHAR(MAX)
DECLARE @LanguageId INT
DECLARE @StartDate DATETIME
DECLARE @ProjectId INT
DECLARE @AppointmentId INT

SET @AppointmentId = 2000000000

DECLARE CandidateCursor CURSOR FOR 
SELECT CandidateInfoId, TaskId, ContactId, FirstName, LastName, LanguageId, StartDate, ProjectId FROM ACT.dbo.CandidateInfo2Appointment
WHERE TaskId IS NOT NULL AND ProjectId IS NOT NULL AND ProjectId <> 0

OPEN CandidateCursor

FETCH NEXT FROM CandidateCursor
INTO @CandidateInfoId, @TaskId, @ContactId, @FirstName, @LastName, @LanguageId, @StartDate, @ProjectId

WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO dbo.APPOINTMENT(
						appointment_id,
						contact_id,
						person_id,
						associate_id,
						group_idx,
						registered,
						registered_associate_id,
						done,
						do_by,
						leadtime,
						task_idx,
						priority_idx,
						type,
						status,
						private,
						alarm,
						text_id,
						project_id,
						mother_id,
						document_id,
						color_index,
						invitedPersonId,
						do_by,
						endDate,
						lagTime,
						source,
						userdef_id,
						userdef2_id,
						updated,
						updated_associate_id,
						updatedCount,
						activeLinks,
						recurrenceRuleId,
						location,
						alldayEvent,
						freeBusy,
						rejectCounter,
						emailId,
						rejectReason,
						hasAlarm,
						assignedBy,
						preferredTZLocation,
						sale_id,
						suggestedAppointmentId,
						suggestedDocumentId
					)
					VALUES(
						@AppointmentId,
						@ContactId, --contact_id
						0, --person_id
						315, --associate_id,
						0, --group_idx
						'1809-02-12 00:00:00.000', --registered
						315, --registered_associate_id QPlanet
						'1809-02-12 00:00:00.000', --done
						@StartDate, --do_by
						0, --leadtime
						@TaskId, --task_idx
						0, --priority_idx
						1, --type
						1, --status
						0, --private
						0, --alarm
						0, --text_id
						@ProjectId,
						0, --mother_id
						0, --document_id
						0, --color_index
						0, --invitedPersonId
						@StartDate, --do_by
						DATEADD(HOUR, 8, @StartDate), --enddate
						0, --lagtime
						0, --source
						0, --userdef_id
						0, --userdef2_id
						'1760-01-01 00:00:00.000', --updated
						0, --updated_associate_id
						0, --updateCount
						0, --activeLinks
						0, --recurrenceRuleId
						'', --location
						0, --allDayEvent
						0, --freeBusy
						0, --rejectCounter
						0, --emailId
						'', --rejectReason
						0, --hasAlarm
						0, --assignedBy
						0, --preferredTZLocation
						0, --sale_id
						0, --suggestedAppointmentId
						0 --suggestedDocumentId
					)
	
	UPDATE ACT.dbo.CandidateInfo2Appointment
	SET AppointmentId = @AppointmentId
	WHERE CandidateInfoId = @CandidateInfoId
	
	INSERT INTO [UDAPPNTSMALL](
						[udappntsmall_id], --id
						[long06], --Reserverd
						[string05], --Office
						[string06], --Language
						[string07], --Gender
						[string08] --Code						
					)
					VALUES (
						@AppointmentId,
						0,
						'QA',
						CASE @LanguageId
							WHEN 1 THEN 'NL'
							WHEN 2 THEN 'FR'
							WHEN 3 THEN 'EN'
							ELSE 'NL'
						END,
						'M',
						CAST(@CandidateInfoId AS NVARCHAR(12))
					)

	UPDATE dbo.APPOINTMENT
	SET [userdef_id] = @AppointmentId
	WHERE appointment_id = @AppointmentId
	
	INSERT INTO [UDAPPNTLARGE](
						[udappntlarge_id], --id
						[string45], --FirstName
						[string46] --LastName					
					)
					VALUES (
						@AppointmentId,
						@FirstName,
						@LastName
					)

	UPDATE dbo.APPOINTMENT
	SET [userdef2_id] = @AppointmentId
	WHERE appointment_id = @AppointmentId
	
	INSERT INTO [Text](
						text_id,
						type,
						owner_id,
						registered,
						registered_associate_id,
						updated,
						updated_associate_id,
						updatedCount,
						text,
						lcid,
						seqno)
					VALUES (
						@AppointmentId, --text_id
						4, --type
						@AppointmentId, --owner_id
						'1809-02-12 00:00:00.000', --registered
						315, --registered_associate_id QPlanet	
						'1760-01-01 00:00:00.000', --updated
						0, --updated_associate_id
						0, --updatedCount
						'',
						2060,
						0
					)

	UPDATE dbo.APPOINTMENT
	SET [text_id] = @AppointmentId
	WHERE appointment_id = @AppointmentId
	
	SET @AppointmentId = @AppointmentId + 1

    FETCH NEXT FROM CandidateCursor
	INTO @CandidateInfoId, @TaskId, @ContactId, @FirstName, @LastName, @LanguageId, @StartDate, @ProjectId
END
CLOSE CandidateCursor;
DEALLOCATE CandidateCursor;

SELECT		[Appointment].[appointment_id]																AS	[Id],
			[Appointment].[do_by]																		AS	[AppointmentDate],
			[Appointment].[endDate]																		AS	[EndDate],
			[Appointment].[Associate_Id]																AS	[AssociateId],
			[UDAPPNTSMALL].[long06]																		AS	[IsReserved],
			[UDAPPNTSMALL].[string05]																	AS	[OfficeId],
			[UDAPPNTSMALL].[string06]																	AS	[LanguageId],
			[UDAPPNTSMALL].[string07]																	AS	[Gender],
			[UDAPPNTSMALL].[string08]																	AS	[Code],
			[UDAPPNTLARGE].[string45]																	AS	[FirstName],
			[UDAPPNTLARGE].[string46]																	AS	[LastName],
			[Appointment].[project_id]																	AS	[CrmProjectId],
			[Appointment].[task_idx]																	AS	[TaskId],
			[Text].[Text]																				AS	[Description]

FROM		[Appointment]

INNER JOIN	[UDAPPNTSMALL]
	ON		[Appointment].[userdef_id] = [UDAPPNTSMALL].[udappntsmall_id]
	AND		[UDAPPNTSMALL].[string08] IS NOT NULL
	AND		[UDAPPNTSMALL].[string08] <> ''
	AND		[UDAPPNTSMALL].[string08] <> '0'

INNER JOIN	[UDAPPNTLARGE]
	ON		[Appointment].[userdef2_id] = [UDAPPNTLARGE].[UDAPPNTLARGE_id]

LEFT JOIN	[Text]
	ON		[Text].[text_id] = [Appointment].[text_id]

COMMIT