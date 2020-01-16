CREATE PROCEDURE [dbo].[Invoicing_ProjectCandidate]
	@Date					DATETIME,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId		UNIQUEIDENTIFIER = NULL

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[ProjectCandidateView].[Id]															AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				[ProjectCandidateView].[CandidateFirstName]											AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[CrmContactView].[Id]																AS  [ContactId],
				[CrmContactView].[Name]																AS	[ContactName],
				[ProjectCandidateView].[InvoiceAmount]												AS  [InvoiceAmount],				
				[ProjectCandidateView].[InvoiceStatusCode]											AS  [InvoiceStatusCode],				
				[ProjectCandidateView].[InvoicedDate]												AS  [InvoicedDate],				
				[ProjectCandidateView].[PurchaseOrderNumber]										AS  [PurchaseOrderNumber],				
				[ProjectCandidateView].[InvoiceNumber]												AS  [InvoiceNumber],				
				[ProjectCandidateView].[InvoiceRemarks]												AS  [InvoiceRemarks],				
				[ProjectTypeCategoryView].[Name]													AS	[ProductName],
				ISNULL([CrmReplicationAppointment].[AppointmentDate], [ProjectCandidateView].CancelledDate) AS  [Date],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],
				NULL																				AS	[ConsultantFirstName],
				NULL																				AS	[ConsultantLastName],
				NULL																				AS	[ConsultantUserName],	
				[ProjectCandidateView].[ProposalId]													AS  [ProposalId],	
				[ProjectCandidateView].[Audit_CreatedBy]											AS  [AuditCreatedBy],				
				[ProjectCandidateView].[Audit_CreatedOn]											AS  [AuditCreatedOn],				
				[ProjectCandidateView].[Audit_ModifiedBy]											AS  [AuditModifiedBy],				
				[ProjectCandidateView].[Audit_ModifiedOn]											AS  [AuditModifiedOn],				
				[ProjectCandidateView].[Audit_DeletedBy]											AS  [AuditDeletedBy],				
				[ProjectCandidateView].[Audit_DeletedOn]											AS  [AuditDeletedOn],				
				[ProjectCandidateView].[Audit_IsDeleted]											AS  [AuditIsDeleted],				
				[ProjectCandidateView].[Audit_VersionId]											AS  [AuditVersionId]	

	FROM		[ProjectCandidateView]

	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [AssessmentDevelopmentProjectView].[Id]
		AND		[ProjectView].[PricingModelId] = 1

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN  [ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectView].[Id]

	INNER JOIN  [ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
		AND		[ProjectTypeCategoryView].[SubCategoryType] IS NULL		

	LEFT JOIN	[CrmReplicationAppointment]
		ON		[CrmReplicationAppointment].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]
		
	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		
				(
					(
						MONTH([ProjectCandidateView].[InvoicedDate]	) = MONTH(@Date)
						AND
						YEAR([ProjectCandidateView].[InvoicedDate]	) = YEAR(@Date)
					)
					OR
					(
						ISNULL([CrmReplicationAppointment].[AppointmentDate], [ProjectCandidateView].CancelledDate) <= @Date
						AND
						[ProjectCandidateView].[InvoiceStatusCode] < 100
					)
				)
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] = @ProjectManagerId)
		AND		(
					[ProjectCandidateView].[InvoiceStatusCode] <> 15
					OR
					(
						MONTH(ISNULL([CrmReplicationAppointment].[AppointmentDate], [ProjectCandidateView].CancelledDate)) = MONTH(@Date)
						AND
						YEAR(ISNULL([CrmReplicationAppointment].[AppointmentDate], [ProjectCandidateView].CancelledDate)) = YEAR(@Date)
					)
				)
END
GO