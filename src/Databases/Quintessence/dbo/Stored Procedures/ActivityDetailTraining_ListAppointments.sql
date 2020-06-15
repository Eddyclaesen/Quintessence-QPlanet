CREATE PROCEDURE [dbo].[ActivityDetailTraining_ListAppointments]
	@ActivityDetailTrainingId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[CrmReplicationAppointmentTraining].*,
				[UserView].[Id]								AS	[UserId],
				[UserView].[FirstName]						AS	[UserFirstName],
				[UserView].[LastName]						AS	[UserLastName],
				[OfficeView].[FullName]						AS	[OfficeFullName],
				[OfficeView].[ShortName]					AS	[OfficeShortName],
				[LanguageView].[Name]						AS	[LanguageName]

	FROM		[ActivityDetailTrainingView]

	INNER JOIN	[ActivityView]
		ON		[ActivityView].[Id] = [ActivityDetailTrainingView].[Id]

	INNER JOIN	[Project2CrmProjectView]
		ON		[Project2CrmProjectView].[ProjectId] = [ActivityView].[ProjectId]

	INNER JOIN	[CrmReplicationAppointmentTraining]
		ON		[CrmReplicationAppointmentTraining].[ProjectId] = [Project2CrmProjectView].[CrmProjectId]
		AND		[CrmReplicationAppointmentTraining].[Code] = [ActivityDetailTrainingView].[Code]
	
	INNER JOIN	[UserView]
		ON		[UserView].[AssociateId] = [CrmReplicationAppointmentTraining].[AssociateId]
	
	INNER JOIN	[OfficeView]
		ON		[OfficeView].[Id] = [CrmReplicationAppointmentTraining].[OfficeId]
	
	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [CrmReplicationAppointmentTraining].[LanguageId]
	
	WHERE		[ActivityDetailTrainingView].[Id] = @ActivityDetailTrainingId
END