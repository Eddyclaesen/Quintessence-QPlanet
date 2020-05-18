CREATE PROCEDURE [dbo].[ProjectCandidateCategoryType1_AssistantOverview]
	@ProjectCandidateCategoryDetailTypeId	UNIQUEIDENTIFIER = NULL,
	@StartDate								DATETIME = NULL,
	@EndDate								DATETIME = NULL,
	@CustomerAssistantId					UNIQUEIDENTIFIER = NULL

AS
BEGIN
	SET NOCOUNT ON;
		SELECT	DISTINCT [ProjectCandidateCategoryDetailType1View].[Id]										AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				CASE WHEN ([ProjectCandidateCategoryDetailType1View].[Extra1] is not null OR
				[ProjectCandidateCategoryDetailType1View].[Extra2] is not null)
				THEN '* '+[ProjectCandidateView].[CandidateFirstName]
				ELSE [ProjectCandidateView].[CandidateFirstName]											
				END
				AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[CrmContactView].[name]																AS	[ContactName],
				[ProjectCandidateView].[IsCancelled]												AS  [IsCancelled],				
				[ProjectCandidateCategoryDetailType1View].[InvoiceAmount]							AS  [InvoiceAmount],				
				NULL																				AS  [Remarks],						
				1																					AS  [StatusCode],					
				NULL																				AS  [OrderConfirmationSentDate],		
				NULL																				AS  [OrderConfirmationReceivedDate],	
				[ProjectCandidateCategoryDetailType1View].[InvitationSentDate]						AS  [InvitationSentDate],			
				NULL																				AS  [ReportMailSentDate],
				[ProjectCandidateView].[ReportDeadline]												AS  [ReportDeadline],							
				[AssessmentDevelopmentProjectView].[ReportDeadlineStep]								AS  [ReportDeadlineStep],
				[ProjectCandidateView].[ReportDeadlineDone]											AS	[ReportDeadlineDone],			
				[ProjectCandidateCategoryDetailType1View].[DossierReadyDate]						AS  [DossierReadyDate],				
				[ProjectCandidateCategoryDetailType1View].[Id]										AS  [ProjectCandidateCategoryDetailTypeId],									
				left([ProjectTypeCategoryView].[Code],1)+lower(substring([ProjectTypeCategoryView].[Code],2,len([ProjectTypeCategoryView].[Code])))	AS	[Type],
				[LanguageView].[Code]																AS	[Language],
				[ProjectCandidateView].[CandidateLanguageId]										AS	[LanguageId],
				[AssessmentDevelopmentProjectView].[FunctionTitle]									AS	[Function],
				[CrmAppointmentView].[AppointmentDate]												AS  [Date],
				[dbo].[Office_RetrieveOfficeNameById]([ProjectCandidateCategoryDetailType1View].[OfficeId]) 
																									AS  [OfficeName],
				LeadAssessor.FirstName+' '+LeadAssessor.LastName									AS	[Assessors],
				LeadAssessor.UserName																AS	[AssessorUserNames],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				NULL																				AS	[CulturalFit],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],				
				dbo.ProjectCandidate_ReportRecipientsAsString([ProjectCandidateView].[Id], ',')		AS	[ReportRecipients],
				NULL		 																		AS	[ReportDeadlineDone],					
				NULL																				AS	[OrderConfirmationSentDateDone],
				NULL																				AS	[OrderConfirmationReceivedDateDone],	
				[ProjectCandidateCategoryDetailType1View].[InvitationSentDateDone]					AS	[InvitationSentDateDone],				
				NULL																				AS	[LeafletSentDateDone],				
				NULL																				AS	[ReportMailSentDateDone],				
				[ProjectCandidateCategoryDetailType1View].[DossierReadyDateDone]					AS	[DossierReadyDateDone],	
				[ProjectCandidateCategoryDetailType1View].[FollowUpDone]							AS	[FollowUpDone],	
				[ProjectTypeCategoryView].[Color]													AS  [ProjectTypeCategoryColor],		
				[ProjectCandidateCategoryDetailType1View].[Extra1]									AS	[Extra1],
				[ProjectCandidateCategoryDetailType1View].[Extra2]									AS	[Extra2],
				[ProjectCandidateCategoryDetailType1View].[Extra1Done]								AS	[Extra1Done],
				[ProjectCandidateCategoryDetailType1View].[Extra2Done]								AS	[Extra2Done],		
				CAST(0 AS BIT)																		AS	[IsReserved],	
				[ProjectCandidateCategoryDetailType1View].[Audit_CreatedBy]							AS  [AuditCreatedBy],				
				[ProjectCandidateCategoryDetailType1View].[Audit_CreatedOn]							AS  [AuditCreatedOn],				
				[ProjectCandidateCategoryDetailType1View].[Audit_ModifiedBy]						AS  [AuditModifiedBy],				
				[ProjectCandidateCategoryDetailType1View].[Audit_ModifiedOn]						AS  [AuditModifiedOn],				
				[ProjectCandidateCategoryDetailType1View].[Audit_DeletedBy]							AS  [AuditDeletedBy],				
				[ProjectCandidateCategoryDetailType1View].[Audit_DeletedOn]							AS  [AuditDeletedOn],				
				[ProjectCandidateCategoryDetailType1View].[Audit_IsDeleted]							AS  [AuditIsDeleted],				
				[ProjectCandidateCategoryDetailType1View].[Audit_VersionId]							AS  [AuditVersionId]	

	FROM		[ProjectCandidateCategoryDetailType1View]

	INNER JOIN	[ProjectCategoryDetailType1View] 
		ON		[ProjectCategoryDetailType1View].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCategoryDetailTypeId] 
 
	INNER JOIN  [ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCandidateId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN  [ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [projectcategorydetailView].[ProjectTypeCategoryId]

	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [ProjectCandidateView].[CandidateLanguageId]

	INNER JOIN	[CrmAppointmentView]
		ON		[CrmAppointmentView].[Code] = [ProjectCandidateView].[Code]
		AND		[CrmAppointmentView].[TaskId] = [ProjectTypeCategoryView].[CrmTaskId]
		
	INNER JOIN	[UserView] [LeadAssessor]
		ON		[LeadAssessor].[AssociateId] = [CrmAppointmentView].[AssociateId]

	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		[ProjectCategoryDetailType1View].[SurveyPlanningId] IN (2,3) --before (2) & after (3)
				AND
				(([ProjectCandidateCategoryDetailType1View].ScheduledDate IS NULL OR @StartDate IS NULL OR [ProjectCandidateCategoryDetailType1View].ScheduledDate >= @StartDate)
				AND	([ProjectCandidateCategoryDetailType1View].ScheduledDate IS NULL OR @EndDate IS NULL OR [ProjectCandidateCategoryDetailType1View].ScheduledDate <= @EndDate))
				AND	(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)		
				and CrmAppointmentView.IsReserved = 0	
				AND [ProjectCandidateCategoryDetailType1View].[FollowUpDone] = 0
				OR [ProjectCandidateCategoryDetailType1View].Id = @ProjectCandidateCategoryDetailTypeId
END