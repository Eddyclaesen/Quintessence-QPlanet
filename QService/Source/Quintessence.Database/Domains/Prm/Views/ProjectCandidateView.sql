CREATE VIEW [dbo].[ProjectCandidateView] AS
	SELECT		[ProjectCandidate].*,
				[CrmAppointmentView].[Code]			AS [Code],
				[ProjectCandidate].[Id]				AS	[ProjectCandidateDetailId],
				COALESCE([CrmAppointmentView].[OfficeId], 1)	AS	[OfficeId],
				[CandidateView].[FirstName]			AS	[CandidateFirstName],
				[CandidateView].[LastName]			AS	[CandidateLastName],
				[CandidateView].[Email]				AS	[CandidateEmail],
				[CandidateView].[LanguageId]		AS	[CandidateLanguageId],
				[CandidateView].[Gender]			AS	[CandidateGender]
				
	FROM		[ProjectCandidate]	WITH (NOLOCK)

	INNER JOIN	[CandidateView]
		ON		[ProjectCandidate].[CandidateId] = [CandidateView].[Id]

	LEFT JOIN	[CrmAppointmentView]
		ON		[CrmAppointmentView].[Id] = [ProjectCandidate].[CrmCandidateAppointmentId]
	
	WHERE		[ProjectCandidate].[Audit_IsDeleted] = 0