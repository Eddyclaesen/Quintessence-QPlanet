
CREATE PROCEDURE [dbo].[AccountantInvoicing_ProjectCandidateCategoryType2]
	@Date				DATETIME = NULL,
	@Id					UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
		SELECT	[ProjectCandidateCategoryDetailType2View].[Id]										AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				[ProjectCandidateView].[CandidateFirstName]											AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[CrmContactView].[Id]																AS  [ContactId],
				[CrmContactView].[Name]																AS	[ContactName],
				[ProjectCandidateCategoryDetailType2View].[InvoiceAmount]							AS  [InvoiceAmount],				
				[ProjectCandidateCategoryDetailType2View].[InvoiceStatusCode]						AS  [InvoiceStatusCode],				
				[ProjectCandidateCategoryDetailType2View].[InvoiceRemarks]							AS  [InvoiceRemarks],				
				[ProjectCandidateCategoryDetailType2View].[InvoicedDate]							AS  [InvoicedDate],				
				[ProjectCandidateCategoryDetailType2View].[PurchaseOrderNumber]						AS  [PurchaseOrderNumber],				
				[ProjectCandidateCategoryDetailType2View].[InvoiceNumber]							AS  [InvoiceNumber],					
				[ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeName]			AS	[ProductName],
				[CrmProjectView].[Name]																AS	[CrmProjectName],
				[ProjectCandidateCategoryDetailType2View].[Deadline]								AS  [Date],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],	
				[ProjectCandidateCategoryDetailType2View].[ProposalId]								AS  [ProposalId],			
				[ProjectCandidateCategoryDetailType2View].[Audit_CreatedBy]							AS  [AuditCreatedBy],				
				[ProjectCandidateCategoryDetailType2View].[Audit_CreatedOn]							AS  [AuditCreatedOn],				
				[ProjectCandidateCategoryDetailType2View].[Audit_ModifiedBy]						AS  [AuditModifiedBy],				
				[ProjectCandidateCategoryDetailType2View].[Audit_ModifiedOn]						AS  [AuditModifiedOn],				
				[ProjectCandidateCategoryDetailType2View].[Audit_DeletedBy]							AS  [AuditDeletedBy],				
				[ProjectCandidateCategoryDetailType2View].[Audit_DeletedOn]							AS  [AuditDeletedOn],				
				[ProjectCandidateCategoryDetailType2View].[Audit_IsDeleted]							AS  [AuditIsDeleted],				
				[ProjectCandidateCategoryDetailType2View].[Audit_VersionId]							AS  [AuditVersionId],
				[ProjectCandidateCategoryDetailType2View].[FinancialEntityId]						AS	[FinancialEntityId]

	FROM		[ProjectCandidateCategoryDetailType2View]

	INNER JOIN	[ProjectCategoryDetailType2View] 
		ON [ProjectCategoryDetailType2View].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeId] 
 
	INNER JOIN  [ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCandidateId]

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
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [projectcategorydetailView].[ProjectTypeCategoryId]

	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		
	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		(
					@Id IS NULL
					AND
					[ProjectCandidateCategoryDetailType2View].[InvoiceAmount] > 0
					AND
					(
						(
							[ProjectCandidateCategoryDetailType2View].[InvoicedDate] >= DATEADD(MONTH, DATEDIFF(MONTH, 0, @Date)-1, 0)
						)
						OR
						(
							(
								[ProjectCategoryDetailType2View].[SurveyPlanningId] NOT IN (2,3) --before (2) & after (3)
								OR
								[ProjectCandidateCategoryDetailType2View].[Deadline] <= @Date
							)							
							AND
							(
								[ProjectCandidateCategoryDetailType2View].[InvoicedDate] IS NULL
								OR
								[ProjectCandidateCategoryDetailType2View].[InvoiceStatusCode] < 100
							)
						)
					)
				)
				AND [ProjectCandidateCategoryDetailType2View].[InvoiceStatusCode] <> 15
				OR [ProjectCandidateCategoryDetailType2View].[Id] = @Id
END
