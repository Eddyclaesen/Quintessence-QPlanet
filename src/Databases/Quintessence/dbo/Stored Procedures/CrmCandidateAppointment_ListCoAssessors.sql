CREATE PROCEDURE [dbo].[CrmCandidateAppointment_ListCoAssessors]
	@QProjectId UNIQUEIDENTIFIER
AS

SELECT		DISTINCT [CrmAppointmentView].Id,
			[CrmAppointmentView].AppointmentDate,
			[CrmAppointmentView].EndDate,
			[CrmAppointmentView].AssociateId,
			[CrmAppointmentView].IsReserved,
			[CrmAppointmentView].OfficeId,
			[CrmAppointmentView].LanguageId,
			[CrmAppointmentView].Gender,
			[CrmAppointmentView].Code,
			[CrmAppointmentView].FirstName,
			[CrmAppointmentView].LastName,
			[CrmAppointmentView].CrmProjectId,
			[CrmAppointmentView].TaskId,
			CAST([CrmAppointmentView].[Description] AS nvarchar(MAX)) AS [Description],
			[UserView].[Id]					AS	[UserId],
			[UserView].[FirstName]			AS	[AssessorFirstName],
			[UserView].[LastName]			AS	[AssessorLastName],
			'CA'							AS	[AssessorType]

FROM		[ProjectCandidateView]

INNER JOIN	[CrmAppointmentView]
	ON		[CrmAppointmentView].[Code] = [ProjectCandidateView].[Code]
	AND		[CrmAppointmentView].[TaskId] = 190 --CoAssessor

INNER JOIN	[UserView]
	ON		[UserView].[AssociateId] = [CrmAppointmentView].[AssociateId]

WHERE		[ProjectCandidateView].[ProjectId] = @QProjectId