CREATE PROCEDURE [dbo].[Invoicing_ProjectCandidateCategoryType1]
	@Date								DATETIME,
	@CustomerAssistantId				UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId					UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
		SELECT	[ProjectCandidateCategoryDetailType1View].[Id]										AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				[ProjectCandidateView].[CandidateFirstName]											AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[CrmContactView].[Id]																AS  [ContactId],
				[CrmContactView].[Name]																AS	[ContactName],
				[ProjectCandidateCategoryDetailType1View].[InvoiceAmount]							AS  [InvoiceAmount],				
				[ProjectCandidateCategoryDetailType1View].[InvoiceStatusCode]						AS  [InvoiceStatusCode],				
				[ProjectCandidateCategoryDetailType1View].[InvoicedDate]							AS  [InvoicedDate],				
				[ProjectCandidateCategoryDetailType1View].[PurchaseOrderNumber]						AS  [PurchaseOrderNumber],				
				[ProjectCandidateCategoryDetailType1View].[InvoiceNumber]							AS  [InvoiceNumber],				
				[ProjectCandidateCategoryDetailType1View].[InvoiceRemarks]							AS  [InvoiceRemarks],				
				[ProjectCandidateCategoryDetailType1View].[ProjectCategoryDetailTypeName]			AS	[ProductName],
				[CrmAppointmentView].[AppointmentDate]												AS  [Date],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],		
				NULL																				AS	[ConsultantFirstName],
				NULL																				AS	[ConsultantLastName],
				NULL																				AS	[ConsultantUserName],	
				[ProjectCandidateCategoryDetailType1View].[ProposalId]								AS  [ProposalId],			
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
		ON [ProjectCategoryDetailType1View].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCategoryDetailTypeId] 
 
	INNER JOIN  [ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCandidateId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN  [ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [projectcategorydetailView].[ProjectTypeCategoryId]

	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[CrmAppointmentView]
		ON		[CrmAppointmentView].[Code] = [ProjectCandidateView].[Code]
		AND		[CrmAppointmentView].[TaskId] = [ProjectTypeCategoryView].[CrmTaskId]
		
	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		[ProjectCandidateCategoryDetailType1View].[InvoiceAmount] > 0
				AND
				(
					(
						MONTH([ProjectCandidateCategoryDetailType1View].[InvoicedDate]	) = MONTH(@Date)
						AND
						YEAR([ProjectCandidateCategoryDetailType1View].[InvoicedDate]	) = YEAR(@Date)
					)
					OR
					(
						(
							[ProjectCategoryDetailType1View].[SurveyPlanningId] NOT IN (2,3) --before (2) & after (3)
							OR
							[ProjectCandidateCategoryDetailType1View].[ScheduledDate] <= @Date
						)
						AND
						[ProjectCandidateCategoryDetailType1View].[InvoiceStatusCode] < 100
					)
				)
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] = @ProjectManagerId)
		AND		(
					[ProjectCandidateCategoryDetailType1View].[InvoiceStatusCode] <> 15
					OR
					(
						MONTH([ProjectCandidateCategoryDetailType1View].[ScheduledDate]) = MONTH(@Date)
						AND
						YEAR([ProjectCandidateCategoryDetailType1View].[ScheduledDate]) = YEAR(@Date)
					)
				)

END
GO