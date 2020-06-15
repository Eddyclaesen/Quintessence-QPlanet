CREATE VIEW [dbo].[ActivityDetailTrainingCandidateView] AS
	SELECT		[ActivityDetailTrainingCandidate].*,
				[ActivityView].[ActivityTypeName]			AS	[ActivityTypeName],
				[ActivityDetailView].[Description]			AS	[ActivityDetailDescription],
				[CrmContactView].[name]						AS	[ContactName],
				[CrmContactView].[department]				AS	[ContactDepartment],
				[CandidateView].[FirstName]					AS	[CandidateFirstName],
				[CandidateView].[LastName]					AS	[CandidateLastName],
				[CandidateView].[Email]						AS	[CandidateEmail],
				[CandidateView].[LanguageId]				AS	[CandidateLanguageId],
				[CandidateView].[Gender]					AS	[CandidateGender]

	FROM		[ActivityDetailTrainingCandidate]	WITH (NOLOCK)

	INNER JOIN	[ActivityDetailTrainingView]
		ON		[ActivityDetailTrainingView].[Id] = [ActivityDetailTrainingCandidate].[ActivityDetailTrainingId]

	INNER JOIN	[ActivityDetailView]
		ON		[ActivityDetailView].[Id] = [ActivityDetailTrainingView].[Id]

	INNER JOIN	[ActivityView]
		ON		[ActivityView].[Id] = [ActivityDetailView].[Id]

	INNER JOIN	[CandidateView]
		ON		[CandidateView].[Id] = [ActivityDetailTrainingCandidate].[CandidateId]	

	INNER JOIN	[CrmReplicationAppointmentTraining]
		ON		[CrmReplicationAppointmentTraining].[Id] = [ActivityDetailTrainingCandidate].[CrmAppointmentId]

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ActivityDetailTrainingCandidate].[ContactId]