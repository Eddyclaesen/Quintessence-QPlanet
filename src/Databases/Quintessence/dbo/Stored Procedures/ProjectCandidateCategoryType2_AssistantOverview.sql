CREATE PROCEDURE [dbo].[ProjectCandidateCategoryType2_AssistantOverview]
	@ProjectCandidateCategoryDetailTypeId	UNIQUEIDENTIFIER = NULL,
	@StartDate								DATETIME = NULL,
	@EndDate								DATETIME = NULL,
	@CustomerAssistantId					UNIQUEIDENTIFIER = NULL

AS
BEGIN
	SET NOCOUNT ON;
		SELECT	[ProjectCandidateCategoryDetailType2View].[Id]									AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				CASE WHEN ([ProjectCandidateCategoryDetailType2View].[Extra1] is not null OR
				[ProjectCandidateCategoryDetailType2View].[Extra2] is not null)
				THEN '* '+[ProjectCandidateView].[CandidateFirstName]
				ELSE [ProjectCandidateView].[CandidateFirstName]											
				END
				AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[CrmContactView].[name]																AS	[ContactName],
				[ProjectCandidateView].[IsCancelled]												AS  [IsCancelled],				
				[ProjectCandidateCategoryDetailType2View].[InvoiceAmount]							AS  [InvoiceAmount],				
				NULL																				AS  [Remarks],						
				1																					AS  [StatusCode],					
				NULL																				AS  [OrderConfirmationSentDate],		
				NULL																				AS  [OrderConfirmationReceivedDate],	
				[ProjectCandidateCategoryDetailType2View].[InvitationSentDate]						AS  [InvitationSentDate],			
				NULL																				AS  [ReportMailSentDate],
				[ProjectCandidateView].[ReportDeadline]												AS  [ReportDeadline],			
				[AssessmentDevelopmentProjectView].[ReportDeadlineStep]								AS  [ReportDeadlineStep],
				[ProjectCandidateView].[ReportDeadlineDone]											AS	[ReportDeadlineDone],			
				[ProjectCandidateCategoryDetailType2View].[DossierReadyDate]						AS  [DossierReadyDate],				
				[ProjectCandidateCategoryDetailType2View].[Id]										AS  [ProjectCandidateCategoryDetailTypeId],									
				left([ProjectTypeCategoryView].[Code],1)+lower(substring([ProjectTypeCategoryView].[Code],2,len([ProjectTypeCategoryView].[Code])))	AS	[Type],
				[LanguageView].[Code]																AS	[Language],
				[ProjectCandidateView].[CandidateLanguageId]										AS	[LanguageId],
				[AssessmentDevelopmentProjectView].[FunctionTitle]									AS	[Function],
				[ProjectCandidateCategoryDetailType2View].[Deadline]								AS  [Date],
				NULL																				AS  [OfficeName], --Type 2 has no location.
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
				[ProjectCandidateCategoryDetailType2View].[InvitationSentDateDone]					AS	[InvitationSentDateDone],				
				NULL																				AS	[LeafletSentDateDone],				
				NULL																				AS	[ReportMailSentDateDone],				
				[ProjectCandidateCategoryDetailType2View].[DossierReadyDateDone]					AS	[DossierReadyDateDone],	
				[ProjectCandidateCategoryDetailType2View].[FollowUpDone]							AS	[FollowUpDone],	
				[ProjectTypeCategoryView].[Color]													AS  [ProjectTypeCategoryColor],		
				[ProjectCandidateCategoryDetailType2View].[Extra1]									AS	[Extra1],
				[ProjectCandidateCategoryDetailType2View].[Extra2]									AS	[Extra2],
				[ProjectCandidateCategoryDetailType2View].[Extra1Done]								AS	[Extra1Done],
				[ProjectCandidateCategoryDetailType2View].[Extra2Done]								AS	[Extra2Done],		
				CAST(0 AS BIT)																		AS	[IsReserved],	
				[ProjectCandidateCategoryDetailType2View].[Audit_CreatedBy]							AS  [AuditCreatedBy],				
				[ProjectCandidateCategoryDetailType2View].[Audit_CreatedOn]							AS  [AuditCreatedOn],				
				[ProjectCandidateCategoryDetailType2View].[Audit_ModifiedBy]						AS  [AuditModifiedBy],				
				[ProjectCandidateCategoryDetailType2View].[Audit_ModifiedOn]						AS  [AuditModifiedOn],				
				[ProjectCandidateCategoryDetailType2View].[Audit_DeletedBy]							AS  [AuditDeletedBy],				
				[ProjectCandidateCategoryDetailType2View].[Audit_DeletedOn]							AS  [AuditDeletedOn],				
				[ProjectCandidateCategoryDetailType2View].[Audit_IsDeleted]							AS  [AuditIsDeleted],				
				[ProjectCandidateCategoryDetailType2View].[Audit_VersionId]							AS  [AuditVersionId]	

	FROM		[ProjectCandidateCategoryDetailType2View]

	INNER JOIN	[ProjectCategoryDetailType2View] 
		ON [ProjectCategoryDetailType2View].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeId] 
 
	INNER JOIN  [ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCandidateId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN  [ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [projectcategorydetailView].[ProjectTypeCategoryId]

	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [ProjectCandidateView].[CandidateLanguageId]

	INNER JOIN	[CrmAppointmentView]
		ON		[CrmAppointmentView].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]
		
	INNER JOIN	[UserView] [LeadAssessor]
		ON		[LeadAssessor].[AssociateId] = [CrmAppointmentView].[AssociateId]

	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		[ProjectCategoryDetailType2View].[SurveyPlanningId] IN (2,3) --before (2) & after (3)
				AND
				(([ProjectCandidateCategoryDetailType2View].[Deadline] IS NULL OR @StartDate IS NULL OR [ProjectCandidateCategoryDetailType2View].[Deadline] >= @StartDate)
				AND	([ProjectCandidateCategoryDetailType2View].[Deadline] IS NULL OR @EndDate IS NULL OR [ProjectCandidateCategoryDetailType2View].[Deadline] <= @EndDate))
				AND	(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)		
				AND [ProjectCandidateCategoryDetailType2View].[FollowUpDone] = 0	
				OR  [ProjectCandidateCategoryDetailType2View].Id = @ProjectCandidateCategoryDetailTypeId
END