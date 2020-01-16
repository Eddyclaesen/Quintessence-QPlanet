CREATE PROCEDURE ProjectCandidate_ListSimulationContextLogins
	@ProjectCandidateId			UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[SimulationContextUser].[UserName],
				[SimulationContextUser].[Password],
				[SimulationContextUser].[ValidFrom],
				[SimulationContextUser].[ValidTo]
	FROM		[ProjectCandidateView]
	INNER JOIN	[ProjectCategoryDetailView] 
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectCandidateView].[ProjectId]
	LEFT JOIN	[ProjectCategorySoDetailView]	
		ON		[ProjectCategorySoDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	LEFT JOIN	[ProjectCategoryPsDetailView]
		ON		[ProjectCategoryPsDetailView].[Id] = [ProjectCategoryDetailView].[Id]	
	LEFT JOIN	[ProjectCategoryFdDetailView]	
		ON		[ProjectCategoryFdDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	LEFT JOIN	[ProjectCategoryFaDetailView]	
		ON		[ProjectCategoryFaDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	LEFT JOIN	[ProjectCategoryEaDetailView]
		ON		[ProjectCategoryEaDetailView].[Id] = [ProjectCategoryDetailView].[Id]	
	LEFT JOIN	[ProjectCategoryDcDetailView]
		ON		[ProjectCategoryDcDetailView].[Id] = [ProjectCategoryDetailView].[Id]	
	LEFT JOIN	[ProjectCategoryAcDetailView]	
		ON		[ProjectCategoryAcDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	LEFT JOIN	[ProjectCategoryCaDetailView]	
		ON		[ProjectCategoryCaDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	INNER JOIN	[CrmReplicationAppointment]
		ON		[CrmReplicationAppointment].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]
	INNER JOIN	[SimulationContextUser]
		ON		[SimulationContextUser].[SimulationContextId] = COALESCE(
				[ProjectCategorySoDetailView].[SimulationContextId],
				[ProjectCategoryPsDetailView].[SimulationContextId],
				[ProjectCategoryFdDetailView].[SimulationContextId],
				[ProjectCategoryFaDetailView].[SimulationContextId],
				[ProjectCategoryEaDetailView].[SimulationContextId],
				[ProjectCategoryDcDetailView].[SimulationContextId],
				[ProjectCategoryAcDetailView].[SimulationContextId],
				[ProjectCategoryCaDetailView].[SimulationContextId]
		)
	WHERE	COALESCE(
				[ProjectCategorySoDetailView].[SimulationContextId],
				[ProjectCategoryPsDetailView].[SimulationContextId],
				[ProjectCategoryFdDetailView].[SimulationContextId],
				[ProjectCategoryFaDetailView].[SimulationContextId],
				[ProjectCategoryEaDetailView].[SimulationContextId],
				[ProjectCategoryDcDetailView].[SimulationContextId],
				[ProjectCategoryAcDetailView].[SimulationContextId],
				[ProjectCategoryCaDetailView].[SimulationContextId]
			) IS NOT NULL
			AND [ProjectCandidateView].[Id] = @ProjectCandidateId
			AND [SimulationContextUser].[ValidTo] >= GETDATE()
			AND	[SimulationContextUser].[ValidTo] <= DATEADD(MONTH, 1, [CrmReplicationAppointment].[EndDate])

END
GO