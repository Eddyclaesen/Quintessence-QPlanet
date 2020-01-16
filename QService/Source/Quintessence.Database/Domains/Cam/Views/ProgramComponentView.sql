CREATE VIEW [dbo].[ProgramComponentView] AS
	SELECT		[ProgramComponent].*,				
				[ProjectCandidateView].[CandidateFirstName]									AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]									AS	[CandidateLastName],
				[AssessmentRoomView].[Name]													AS	[AssessmentRoomName],
				[AssessmentRoomView].[OfficeId]												AS	[AssessmentRoomOfficeId],
				[AssessmentRoomView].[OfficeShortName]										AS	[AssessmentRoomOfficeShortName],
				[AssessmentRoomView].[OfficeFullName]										AS	[AssessmentRoomOfficeFullName],
				[SimulationView].[Name]														AS	[SimulationName],
				[ProjectCandidateCategoryDetailTypeView].[ProjectCategoryDetailTypeName]	AS	[ProjectCategoryDetailTypeName],
				LeadAssessorUser.[FirstName]												AS	[LeadAssessorUserFirstName],
				LeadAssessorUser.[LastName]													AS	[LeadAssessorUserLastName],
				CoAssessorUser.[FirstName]													AS	[CoAssessorUserFirstName],
				CoAssessorUser.[LastName]													AS	[CoAssessorUserLastName]


	FROM		[ProgramComponent] WITH (NOLOCK)

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProgramComponent].[ProjectCandidateId]

	INNER JOIN	[AssessmentRoomView]
		ON		[AssessmentRoomView].[Id] = [ProgramComponent].[AssessmentRoomId]

	LEFT JOIN	[SimulationCombinationView]
		ON		[SimulationCombinationView].[Id] = [ProgramComponent].[SimulationCombinationId]	

	LEFT JOIN	[SimulationView]
		ON		[SimulationView].[Id] = [SimulationCombinationView].[SimulationId]

	LEFT JOIN	[ProjectCandidateCategoryDetailTypeView]
		ON		[ProjectCandidateCategoryDetailTypeView].[Id] = [ProgramComponent].[ProjectCandidateCategoryDetailTypeId]

	LEFT JOIN	[UserView]	LeadAssessorUser
		ON		LeadAssessorUser.[Id] = [ProgramComponent].[LeadAssessorUserId]

	LEFT JOIN	[UserView]	CoAssessorUser
		ON		CoAssessorUser.[Id] = [ProgramComponent].[CoAssessorUserId]

	WHERE		[ProgramComponent].[Audit_IsDeleted] = 0
