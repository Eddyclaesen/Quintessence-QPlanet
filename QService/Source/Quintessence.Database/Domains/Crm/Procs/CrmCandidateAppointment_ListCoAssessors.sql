CREATE PROCEDURE [dbo].[CrmCandidateAppointment_ListCoAssessors]
	@QProjectId UNIQUEIDENTIFIER
AS

SELECT		[CrmAppointmentView].*,
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