CREATE PROCEDURE [dbo].[ProjectCandidate_ReportingOverview]
	@ProjectCandidateId		UNIQUEIDENTIFIER = NULL,
	@StartDate				DATETIME		 = NULL,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT			[ProjectCandidateView].[Id]															AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				[ProjectCandidateView].[CandidateFirstName]											AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]+' ('+[AssessmentDevelopmentProjectView].ProjectTypeCategoryCode+')' AS	[CandidateLastName],
				[CrmContactView].[name]																AS	[ContactName],
				[Language].[Code]																	AS	[ReportLanguage],
				[ProjectCandidateView].[ReportDeadline]												AS  [ReportDeadline],
				[ReportStatus].[Id]																	AS	[ReportStatusId],
				[ReportStatus].[Name]																AS	[ReportStatusName],
				[ProjectCandidateView].[Remarks]													AS  [CandidateRemarks],
				[AssessmentDevelopmentProjectView].[ReportRemarks]									AS	[ReportRemarks],
				[AssessmentDevelopmentProjectView].[IsRevisionByPmRequired]							AS	[IsRevisionByPmRequired],
				[AssessmentDevelopmentProjectView].[SendReportToParticipant]						AS	[SendReportToParticipant],
				[AssessmentDevelopmentProjectView].[SendReportToParticipantRemarks]					AS	[SendReportToParticipantRemarks],
				[dbo].[ProjectCandidate_AssessorsAsString](
						[ProjectCandidateView].[Code], 
						','
				)																					AS	[Assessors],
				[dbo].[ProjectCandidate_AssessorUserNamesAsString](
						[ProjectCandidateView].[Code],
						','
				)																					AS	[AssessorUserNames],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectCandidateView].[ReportReviewerId]											AS	[ReportReviewerId],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],
				dbo.ProjectCandidate_ReportRecipientsAsString([ProjectCandidateView].[Id], ',')		AS	[ReportRecipients],
				[ProjectCandidateView].[Audit_CreatedBy]											AS  [AuditCreatedBy],				
				[ProjectCandidateView].[Audit_CreatedOn]											AS  [AuditCreatedOn],				
				[ProjectCandidateView].[Audit_ModifiedBy]											AS  [AuditModifiedBy],				
				[ProjectCandidateView].[Audit_ModifiedOn]											AS  [AuditModifiedOn],				
				[ProjectCandidateView].[Audit_DeletedBy]											AS  [AuditDeletedBy],				
				[ProjectCandidateView].[Audit_DeletedOn]											AS  [AuditDeletedOn],				
				[ProjectCandidateView].[Audit_IsDeleted]											AS  [AuditIsDeleted],				
				[ProjectCandidateView].[Audit_VersionId]											AS  [AuditVersionId]
	
	FROM [ProjectCandidateView]

	INNER JOIN  [LanguageView] [Language]
		ON [Language].[Id] = [ProjectCandidateView].[ReportLanguageId]
		
	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [AssessmentDevelopmentProjectView].[Id]

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN	[CrmAppointmentView] [Appointment]
		ON		[Appointment].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]
		
	INNER JOIN	[UserView] [LeadAssessor]
		ON		[LeadAssessor].[AssociateId] = [Appointment].[AssociateId]
		
	LEFT JOIN	[UserView] [ReportReviewer]
		ON		[ReportReviewer].[Id] = [ProjectCandidateView].[ReportReviewerId]
		
	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]
		
	INNER JOIN  [ReportStatusView] [ReportStatus]
		ON [ReportStatus].[Id] = [ProjectCandidateView].[ReportStatusId] 

	WHERE		(@ProjectCandidateId IS NULL OR [ProjectCandidateView].[Id] = @ProjectCandidateId)
		AND		(@StartDate IS NULL OR (CONVERT(DATE,[ProjectCandidateView].[ReportDeadline]) <= CONVERT(DATE,@StartDate) AND [ReportStatus].[Id] NOT IN (4,5)))
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
END
GO