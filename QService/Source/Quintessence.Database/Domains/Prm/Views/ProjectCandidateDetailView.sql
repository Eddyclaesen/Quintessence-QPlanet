CREATE VIEW [dbo].[ProjectCandidateDetailView] AS
	SELECT		[ProjectCandidateView].[Id]											AS	[Id],
				CAST(CASE 
					WHEN [CrmReplicationAppointment].[Id] IS NULL THEN 1
						ELSE 0
					END AS BIT)														AS	[IsSuperofficeAppointmentDeleted],
				CAST([CrmReplicationAppointment].[AppointmentDate] AS DATETIME2)	AS	[AssessmentStartDate],
				CAST([CrmReplicationAppointment].[EndDate] AS DATETIME2)			AS	[AssessmentEndDate],
				[CrmReplicationAppointment].[AssociateId]							AS	[AssociateId],
				[UserView].[Id]														AS	[LeadAssessorUserId],
				[UserView].[FirstName]												AS	[LeadAssessorFirstName],
				[UserView].[LastName]												AS	[LeadAssessorLastName]
				
	FROM		[ProjectCandidateView]

	LEFT JOIN	[CrmReplicationAppointment]	WITH (NOLOCK)
		ON		[ProjectCandidateView].[CrmCandidateAppointmentId] = [CrmReplicationAppointment].[Id]

	LEFT JOIN	[UserView]
		ON		[UserView].[AssociateId] = [CrmReplicationAppointment].[AssociateId]