CREATE PROCEDURE [dbo].[ProjectCandidateReserved_AssistantOverview]
	@StartDate				DATETIME	= NULL,
	@EndDate				DATETIME	= NULL,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		NEWID()																				AS  [Id],							
				NEWID()																				AS  [ProjectId],	
				[CrmProjectView].[name]																AS  [ProjectName],	
				[CrmAppointmentView].[FirstName]													AS	[CandidateFirstName],
				CASE WHEN [CrmAppointmentView].[LastName] is null THEN ' ('+SoTask.Name+')'
				ELSE [CrmAppointmentView].[LastName]+' ('+SoTask.Name+')'
				END
				AS	[CandidateLastName],
				[CrmAppointmentView].[Code]															AS	[Code],
				[CrmContactView].[name]																AS	[ContactName],
				[LanguageView].[Code]																AS	[Language],
				[CrmAppointmentView].[LanguageId]													AS	[LanguageId],
				[CrmAppointmentView].[AppointmentDate]												AS  [Date],
				[OfficeView].[ShortName]															AS  [OfficeName],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				'FFFFFF'																			AS	[ProjectTypeCategoryColor],	
				[CrmAppointmentView].[IsReserved]													AS	[IsReserved],	
				SUSER_SNAME()																		AS  [AuditCreatedBy],				
				GETDATE()																			AS  [AuditCreatedOn],				
				CAST(0 AS BIT)																		AS  [AuditIsDeleted],				
				NEWID()																				AS  [AuditVersionId]	

	FROM		[CrmAppointmentView]
	
	LEFT JOIN	[CrmProjectView]
		ON		[CrmProjectView].[Id] = [CrmAppointmentView].[CrmProjectId]

	LEFT JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [CrmProjectView].[ContactId]

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [CrmAppointmentView].[LanguageId]

	INNER JOIN	[OfficeView]
		ON		[OfficeView].[Id] = [CrmAppointmentView].[OfficeId]

	INNER JOIN	[UserView] [LeadAssessor]
		ON		[LeadAssessor].[AssociateId] = [CrmAppointmentView].[AssociateId]
		
	LEFT JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[AssociateId] = [CrmContactView].[CustomerAssistantId]
	
	LEFT JOIN	Superoffice7server.superoffice7.dbo.task SoTask
		ON		[CrmAppointmentView].TaskId = SoTask.task_id

	WHERE		[CrmAppointmentView].[IsReserved] = 1
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
		AND		((@StartDate IS NULL OR [CrmAppointmentView].[AppointmentDate] >= @StartDate) AND (@EndDate IS NULL OR [CrmAppointmentView].[AppointmentDate] <= @EndDate))
		AND		[CrmAppointmentView].TaskId <> 190
END
GO