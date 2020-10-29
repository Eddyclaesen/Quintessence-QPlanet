CREATE PROCEDURE [dbo].[AccountantInvoicing_ProjectCandidateCategoryType3]
	@Date			DATETIME = NULL,
	@Id				UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
		SELECT	[ProjectCandidateCategoryDetailType3View].[Id]										AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				[ProjectCandidateView].[CandidateFirstName]											AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[CrmContactView].[Id]																AS  [ContactId],
				[CrmContactView].[Name]																AS	[ContactName],
				[ProjectCandidateCategoryDetailType3View].[InvoiceAmount]							AS  [InvoiceAmount],				
				[ProjectCandidateCategoryDetailType3View].[InvoiceStatusCode]						AS  [InvoiceStatusCode],				
				[ProjectCandidateCategoryDetailType3View].[InvoiceRemarks]							AS  [InvoiceRemarks],				
				[ProjectCandidateCategoryDetailType3View].[InvoicedDate]							AS  [InvoicedDate],				
				[ProjectCandidateCategoryDetailType3View].[PurchaseOrderNumber]						AS  [PurchaseOrderNumber],				
				[ProjectCandidateCategoryDetailType3View].[InvoiceNumber]							AS  [InvoiceNumber],					
				[ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeName]			AS	[ProductName],
				[CrmProjectView].[Name]																AS	[CrmProjectName],
				[ProjectCandidateCategoryDetailType3View].[Deadline]								AS  [Date],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],
				[ProjectCandidateCategoryDetailType3View].[ProposalId]								AS  [ProposalId],				
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
		ON [ProjectCategoryDetailType3View].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeId] 
 
	INNER JOIN  [ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCandidateId]

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
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeId]

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
					[ProjectCandidateCategoryDetailType3View].[InvoiceAmount] > 0
					AND
					(
						(
							[ProjectCandidateCategoryDetailType3View].[InvoicedDate] >= DATEADD(MONTH, DATEDIFF(MONTH, 0, @Date)-1, 0)
						)
						OR
						(
							(
								[ProjectCategoryDetailType3View].[SurveyPlanningId] IN (2,3) --before (2) & after (3)
								OR
								[ProjectCandidateCategoryDetailType3View].[Deadline] <= @Date
							)
							AND
							(
								[ProjectCandidateCategoryDetailType3View].[InvoicedDate] IS NULL
								OR
								[ProjectCandidateCategoryDetailType3View].[InvoiceStatusCode] < 100
							)
						)
					)
				)
				AND [ProjectCandidateCategoryDetailType3View].[InvoiceStatusCode] <> 15
				OR 	[ProjectCandidateCategoryDetailType3View].[Id] = @Id
END
