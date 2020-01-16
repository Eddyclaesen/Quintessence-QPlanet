CREATE PROCEDURE [dbo].[ProjectCandidateCategoryType3_AssistantOverview]
	@ProjectCandidateCategoryDetailTypeId	UNIQUEIDENTIFIER = NULL,
	@StartDate								DATETIME = NULL,
	@EndDate								DATETIME = NULL,
	@CustomerAssistantId					UNIQUEIDENTIFIER = NULL

AS
BEGIN
	SET NOCOUNT ON;
		SELECT	[ProjectCandidateCategoryDetailType3View].[Id]									AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				CASE WHEN ([ProjectCandidateCategoryDetailType3View].[Extra1] is not null OR
				[ProjectCandidateCategoryDetailType3View].[Extra2] is not null)
				THEN '* '+[ProjectCandidateView].[CandidateFirstName]
				ELSE [ProjectCandidateView].[CandidateFirstName]											
				END
				AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[CrmContactView].[name]																AS	[ContactName],
				[ProjectCandidateView].[IsCancelled]												AS  [IsCancelled],				
				[ProjectCandidateCategoryDetailType3View].[InvoiceAmount]							AS  [InvoiceAmount],				
				NULL																				AS  [Remarks],						
				1																					AS  [StatusCode],					
				NULL																				AS  [OrderConfirmationSentDate],		
				NULL																				AS  [OrderConfirmationReceivedDate],	
				[ProjectCandidateCategoryDetailType3View].[InvitationSentDate]						AS  [InvitationSentDate],			
				NULL																				AS  [ReportMailSentDate],
				[ProjectCandidateView].[ReportDeadline]												AS  [ReportDeadline],						
				[AssessmentDevelopmentProjectView].[ReportDeadlineStep]								AS  [ReportDeadlineStep],	
				[ProjectCandidateView].[ReportDeadlineDone]											AS	[ReportDeadlineDone],		
				[ProjectCandidateCategoryDetailType3View].[DossierReadyDate]						AS  [DossierReadyDate],				
				[ProjectCandidateCategoryDetailType3View].[Id]										AS  [ProjectCandidateCategoryDetailTypeId],									
				left([ProjectTypeCategoryView].[Code],1)+lower(substring([ProjectTypeCategoryView].[Code],2,len([ProjectTypeCategoryView].[Code])))	AS	[Type],
				[LanguageView].[Code]																AS	[Language],
				[ProjectCandidateView].[CandidateLanguageId]										AS	[LanguageId],
				[AssessmentDevelopmentProjectView].[FunctionTitle]									AS	[Function],
				[ProjectCandidateCategoryDetailType3View].[Deadline]								AS  [Date],
				NULL																				AS  [OfficeName], --Type 3 has no location.
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
				[ProjectCandidateCategoryDetailType3View].[InvitationSentDateDone]					AS	[InvitationSentDateDone],				
				NULL																				AS	[LeafletSentDateDone],				
				NULL																				AS	[ReportMailSentDateDone],				
				[ProjectCandidateCategoryDetailType3View].[DossierReadyDateDone]					AS	[DossierReadyDateDone],	
				[ProjectCandidateCategoryDetailType3View].[FollowUpDone]							AS	[FollowUpDone],	
				[ProjectTypeCategoryView].[Color]													AS  [ProjectTypeCategoryColor],		
				[ProjectCandidateCategoryDetailType3View].[Extra1]									AS	[Extra1],
				[ProjectCandidateCategoryDetailType3View].[Extra2]									AS	[Extra2],
				[ProjectCandidateCategoryDetailType3View].[Extra1Done]								AS	[Extra1Done],
				[ProjectCandidateCategoryDetailType3View].[Extra2Done]								AS	[Extra2Done],		
				CAST(0 AS BIT)																		AS	[IsReserved],	
				[ProjectCandidateCategoryDetailType3View].[Audit_CreatedBy]							AS  [AuditCreatedBy],				
				[ProjectCandidateCategoryDetailType3View].[Audit_CreatedOn]							AS  [AuditCreatedOn],				
				[ProjectCandidateCategoryDetailType3View].[Audit_ModifiedBy]						AS  [AuditModifiedBy],				
				[ProjectCandidateCategoryDetailType3View].[Audit_ModifiedOn]						AS  [AuditModifiedOn],				
				[ProjectCandidateCategoryDetailType3View].[Audit_DeletedBy]							AS  [AuditDeletedBy],				
				[ProjectCandidateCategoryDetailType3View].[Audit_DeletedOn]							AS  [AuditDeletedOn],				
				[ProjectCandidateCategoryDetailType3View].[Audit_IsDeleted]							AS  [AuditIsDeleted],				
				[ProjectCandidateCategoryDetailType3View].[Audit_VersionId]							AS  [AuditVersionId]	

	FROM		[ProjectCandidateCategoryDetailType3View]

	INNER JOIN	[ProjectCategoryDetailType3View] 
		ON		[ProjectCategoryDetailType3View].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeId] 
 
	INNER JOIN  [ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCandidateId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN  [ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeId]

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
		
	WHERE		[ProjectCategoryDetailType3View].[SurveyPlanningId] IN (2,3) --before (2) & after (3)
				AND
				(([ProjectCandidateCategoryDetailType3View].[Deadline] IS NULL OR @StartDate IS NULL OR [ProjectCandidateCategoryDetailType3View].[Deadline] >= @StartDate)
				AND	([ProjectCandidateCategoryDetailType3View].[Deadline] IS NULL OR @EndDate IS NULL OR [ProjectCandidateCategoryDetailType3View].[Deadline] <= @EndDate))
				AND	(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)			
END
GO
