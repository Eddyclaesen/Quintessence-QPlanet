CREATE PROCEDURE [dbo].[ProjectCandidateCancelled_AssistantOverview]
	@StartDate				DATETIME		 = NULL,
	@EndDate				DATETIME		 = NULL,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[ProjectCandidateView].[Id]															AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				[ProjectCandidateView].[CandidateFirstName]											AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[ProjectCandidateView].[Code]														AS  [Code],
				[CrmContactView].[name]																AS	[ContactName],
				[ProjectCandidateView].[IsCancelled]												AS  [IsCancelled],				
				[ProjectCandidateView].[InvoiceAmount]												AS  [InvoiceAmount],				
				[ProjectCandidateView].[Remarks]													AS  [Remarks],						
				[ProjectCandidateView].[FollowUpDone]												AS  [FollowUpDone],					
				[ProjectCandidateView].[OrderConfirmationSentDate]									AS  [OrderConfirmationSentDate],		
				[ProjectCandidateView].[OrderConfirmationReceivedDate]								AS  [OrderConfirmationReceivedDate],	
				[ProjectCandidateView].[InvitationSentDate]											AS  [InvitationSentDate],			
				[ProjectCandidateView].[ReportMailSentDate]											AS  [ReportMailSentDate],			
				[ProjectCandidateView].[ReportDeadline]												AS  [ReportDeadline],			
				[AssessmentDevelopmentProjectView].[ReportDeadlineStep]								AS  [ReportDeadlineStep],			
				[ProjectCandidateView].[DossierReadyDate]											AS  [DossierReadyDate],				
				NULL																				AS  [ProjectCandidateCategoryDetailTypeId],									
				[ProjectTypeCategoryView].[Name]													AS	[Type],
				[LanguageView].[Code]																AS	[Language],
				[ProjectCandidateView].[CandidateLanguageId]										AS  [LanguageId],			
				[AssessmentDevelopmentProjectView].[FunctionTitle]									AS	[Function],
				[ProjectCandidateView].[CancelledAppointmentDate]									AS  [Date],
				[dbo].[Office_RetrieveOfficeNameById]([CrmReplicationAppointment].[OfficeId])		AS  [OfficeName],
				LeadAssessor.UserName																AS	[Assessors],
				LeadAssessor.FirstName+' '+LeadAssessor.LastName									AS	[AssessorUserNames],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				NULL																				AS	[CulturalFit],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],
				dbo.ProjectCandidate_ReportRecipientsAsString([ProjectCandidateView].[Id], ',')		AS	[ReportRecipients],
				[ProjectCandidateView].[ReportDeadlineDone]											AS	[ReportDeadlineDone],					
				[ProjectCandidateView].[OrderConfirmationSentDateDone]								AS	[OrderConfirmationSentDateDone],
				[ProjectCandidateView].[OrderConfirmationReceivedDateDone]							AS	[OrderConfirmationReceivedDateDone],	
				[ProjectCandidateView].[InvitationSentDateDone]										AS	[InvitationSentDateDone],				
				[ProjectCandidateView].[LeafletSentDateDone]										AS	[LeafletSentDateDone],				
				[ProjectCandidateView].[ReportMailSentDateDone]										AS	[ReportMailSentDateDone],				
				[ProjectCandidateView].[DossierReadyDateDone]										AS	[DossierReadyDateDone],	
				[ProjectTypeCategoryView].[Color]													AS	[ProjectTypeCategoryColor],	
				[ProjectCandidateView].[Extra1]														AS	[Extra1],	
				[ProjectCandidateView].[Extra2]														AS	[Extra2],		
				[ProjectCandidateView].[Extra1Done]													AS	[Extra1Done],	
				[ProjectCandidateView].[Extra2Done]													AS	[Extra2Done],	
				CAST(0 AS BIT)																		AS	[IsReserved],	
				[ProjectCandidateView].[Audit_CreatedBy]											AS  [AuditCreatedBy],				
				[ProjectCandidateView].[Audit_CreatedOn]											AS  [AuditCreatedOn],				
				[ProjectCandidateView].[Audit_ModifiedBy]											AS  [AuditModifiedBy],				
				[ProjectCandidateView].[Audit_ModifiedOn]											AS  [AuditModifiedOn],				
				[ProjectCandidateView].[Audit_DeletedBy]											AS  [AuditDeletedBy],				
				[ProjectCandidateView].[Audit_DeletedOn]											AS  [AuditDeletedOn],				
				[ProjectCandidateView].[Audit_IsDeleted]											AS  [AuditIsDeleted],				
				[ProjectCandidateView].[Audit_VersionId]											AS  [AuditVersionId]	

	FROM		[ProjectCandidateView]

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [ProjectCandidateView].[CandidateLanguageId]
	
	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [AssessmentDevelopmentProjectView].[Id]

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN  [ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectView].[Id]

	INNER JOIN  [ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
		AND		[ProjectTypeCategoryView].[SubCategoryType] IS NULL		

	LEFT JOIN	[CrmReplicationAppointment]
		ON		[CrmReplicationAppointment].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]
		
	LEFT JOIN	[UserView] [LeadAssessor]
		ON		[LeadAssessor].[AssociateId] = [CrmReplicationAppointment].[AssociateId]
		
	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		(@StartDate IS NULL OR [ProjectCandidateView].[CancelledAppointmentDate] >= @StartDate)
		AND     (@EndDate IS NULL OR [ProjectCandidateView].[CancelledAppointmentDate] <= @EndDate)
		AND		[ProjectCandidateView].[IsCancelled] = 1
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)

END
GO