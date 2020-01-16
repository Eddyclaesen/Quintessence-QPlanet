CREATE PROCEDURE [dbo].[Reporting_GetCandidateCoAssessor] 
	@ProjectCandidateId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT      [UserView].[LastName]+' '+[UserView].[FirstName] AS CoAssessor

	FROM        [ProjectCandidateView]

	LEFT JOIN   [CrmAppointmentView]
		ON		[CrmAppointmentView].[Code] = [ProjectCandidateView].[Code]
		AND		[CrmAppointmentView].[TaskId] = '190'

	LEFT JOIN   [UserView]
		ON		[UserView].[AssociateId] = [CrmAppointmentView].[AssociateId]

	WHERE       [ProjectCandidateView].[Id] = @ProjectCandidateId
END
GO