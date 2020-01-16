CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateGetAssessors]
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[LeadAssessor].[Id]													AS	[LeadAssessorId],
				[LeadAssessor].[FirstName] + ' ' + [LeadAssessor].[LastName]		AS	[LeadAssessorName],
				[CoAssessor].[Id]													AS	[CoAssessorId],
				[CoAssessor].[FirstName] + ' ' + [CoAssessor].[LastName]			AS	[CoAssessorName]
	
	FROM		[ProjectCandidateView]
	
	INNER JOIN	[CrmAppointmentView]
		ON		[CrmAppointmentView].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]
		
	LEFT JOIN	[UserView] [LeadAssessor]
		ON		[LeadAssessor].[AssociateId] = [CrmAppointmentView].[AssociateId]
		
	LEFT JOIN	[UserView] [CoAssessor]
		ON		[CoAssessor].[Id] = [ProjectCandidateView].[ScoringCoAssessorId]
	
	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId
END