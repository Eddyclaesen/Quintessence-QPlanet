CREATE PROCEDURE CrmCandidateAppointment_ListUnregisteredCrmCandidateAppointments
	@QProjectId			UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[CrmAppointmentView].[Id],
				[CrmAppointmentView].[AppointmentDate],
				[CrmAppointmentView].[AssociateId],
				[UserView].[Id]									AS	[UserId],
				[CrmAppointmentView].[OfficeId],
				[CrmAppointmentView].[LanguageId],
				[CrmAppointmentView].[CrmProjectId],
				[CrmAppointmentView].[FirstName],
				[CrmAppointmentView].[LastName],
				[CrmAppointmentView].[Gender],
				[CrmAppointmentView].[Code],
				'LA'											AS	[AssessorType]

	FROM		[CrmAppointmentView]

	INNER JOIN	[UserView]
		ON		[UserView].[AssociateId] = [CrmAppointmentView].[AssociateId]

	INNER JOIN	[Project2CrmProjectView]
		ON		[Project2CrmProjectView].[CrmProjectId] = [CrmAppointmentView].[CrmProjectId]
		AND		[Project2CrmProjectView].[ProjectId] = @QProjectId

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[ProjectId] = @QProjectId

	INNER JOIN	[ProjectType2ProjectTypeCategory] WITH (NOLOCK)
		ON		[ProjectType2ProjectTypeCategory].[IsMain] = 1
		AND		[ProjectType2ProjectTypeCategory].[ProjectTypeCategoryId] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
		AND		[ProjectTypeCategoryView].[CrmTaskId] = [CrmAppointmentView].[TaskId]
	
	LEFT JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[CrmCandidateAppointmentId] = [CrmAppointmentView].[Id]

	WHERE		[ProjectCandidateView].[CandidateId] IS NULL
		AND		[CrmAppointmentView].[Code] <> ''
END