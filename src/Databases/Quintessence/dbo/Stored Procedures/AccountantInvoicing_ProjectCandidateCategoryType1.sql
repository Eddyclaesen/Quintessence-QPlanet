
CREATE PROCEDURE [dbo].[AccountantInvoicing_ProjectCandidateCategoryType1]
	@Date			DATETIME = NULL,
	@Id				UNIQUEIDENTIFIER = NULL
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
				[ProjectCandidateCategoryDetailType1View].[InvoiceRemarks]							AS  [InvoiceRemarks],				
				[ProjectCandidateCategoryDetailType1View].[InvoicedDate]							AS  [InvoicedDate],				
				[ProjectCandidateCategoryDetailType1View].[PurchaseOrderNumber]						AS  [PurchaseOrderNumber],				
				[ProjectCandidateCategoryDetailType1View].[InvoiceNumber]							AS  [InvoiceNumber],				
				[ProjectCandidateCategoryDetailType1View].[ProjectCategoryDetailTypeName]			AS	[ProductName],
				[CrmProjectView].[Name]																AS	[CrmProjectName],
				[CrmAppointmentView].[AppointmentDate]												AS  [Date],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],
				[ProjectCandidateCategoryDetailType1View].[ProposalId]								AS  [ProposalId],
				[ProjectCandidateCategoryDetailType1View].[Audit_CreatedBy]							AS  [AuditCreatedBy],				
				[ProjectCandidateCategoryDetailType1View].[Audit_CreatedOn]							AS  [AuditCreatedOn],				
				[ProjectCandidateCategoryDetailType1View].[Audit_ModifiedBy]						AS  [AuditModifiedBy],				
				[ProjectCandidateCategoryDetailType1View].[Audit_ModifiedOn]						AS  [AuditModifiedOn],				
				[ProjectCandidateCategoryDetailType1View].[Audit_DeletedBy]							AS  [AuditDeletedBy],				
				[ProjectCandidateCategoryDetailType1View].[Audit_DeletedOn]							AS  [AuditDeletedOn],				
				[ProjectCandidateCategoryDetailType1View].[Audit_IsDeleted]							AS  [AuditIsDeleted],				
				[ProjectCandidateCategoryDetailType1View].[Audit_VersionId]							AS  [AuditVersionId],
				[ProjectCandidateCategoryDetailType1View].[FinancialEntityId]						AS	[FinancialEntityId]	

	FROM		[ProjectCandidateCategoryDetailType1View]

	INNER JOIN	[ProjectCategoryDetailType1View] 
		ON		[ProjectCategoryDetailType1View].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCategoryDetailTypeId]
 
	INNER JOIN  [ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCandidateId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN	[Project2CrmProjectView]
		ON		[Project2CrmProjectView].[ProjectId] = [ProjectView].[Id]

	INNER JOIN	[CrmProjectView]
		ON		[CrmProjectView].[Id] = [Project2CrmProjectView].[CrmProjectId]
		AND		(@Date IS NULL OR @Date BETWEEN [CrmProjectView].[BookyearFrom] AND [CrmProjectView].[BookyearTo])

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

	WHERE		(
					@Id IS NULL
					AND
					[ProjectCandidateCategoryDetailType1View].[InvoiceAmount] > 0
					AND
					(
						(
							[ProjectCandidateCategoryDetailType1View].[InvoicedDate] >= DATEADD(MONTH, DATEDIFF(MONTH, 0, @Date)-1, 0)
						)
						OR
						(
							(
								[ProjectCategoryDetailType1View].[SurveyPlanningId] NOT IN (2,3) --before (2) & after (3)
								OR
								[ProjectCandidateCategoryDetailType1View].[ScheduledDate] <= @Date
							)
							AND
							(
								[ProjectCandidateCategoryDetailType1View].[InvoicedDate] IS NULL
								OR
								[ProjectCandidateCategoryDetailType1View].[InvoiceStatusCode] < 100
							)
						)
					)
				)
				AND [ProjectCandidateCategoryDetailType1View].[InvoiceStatusCode] <> 15
				OR 	[ProjectCandidateCategoryDetailType1View].[Id] = @Id

END
