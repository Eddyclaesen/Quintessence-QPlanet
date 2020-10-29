CREATE PROCEDURE [dbo].[AccountantInvoicing_AssessmentDevelopmentProjectFixedPrice]
	@Date	DATETIME = NULL,
	@Id		UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[ProjectFixedPriceView].[Id]													AS  [Id],							
				[ProjectFixedPriceView].[ProjectId]												AS  [ProjectId],	
				[ProjectView].[Name]															AS  [ProjectName],
				[CrmContactView].[Id]															AS  [ContactId],	
				[CrmContactView].[Name]															AS	[ContactName],
				[ProjectFixedPriceView].[Amount]												AS  [InvoiceAmount],				
				[ProjectFixedPriceView].[InvoiceStatusCode]										AS  [InvoiceStatusCode],				
				[ProjectFixedPriceView].[InvoiceRemarks]										AS  [InvoiceRemarks],				
				[ProjectFixedPriceView].[InvoicedDate]											AS  [InvoicedDate],				
				[ProjectFixedPriceView].[PurchaseOrderNumber]									AS  [PurchaseOrderNumber],				
				[ProjectFixedPriceView].[InvoiceNumber]											AS  [InvoiceNumber],					
				'Partial payment (ACDC)'														AS	[ProductName],
				[CrmProjectView].[Name]															AS	[CrmProjectName],
				[ProjectFixedPriceView].[Deadline]												AS  [Date],
				[CustomerAssistant].[FirstName]													AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]													AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]													AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]													AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]														AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]														AS	[ProjectManagerUserName],
				[ProjectFixedPriceView].[ProposalId]											AS  [ProposalId],
				[ProjectFixedPriceView].[Audit_CreatedBy]										AS  [AuditCreatedBy],				
				[ProjectFixedPriceView].[Audit_CreatedOn]										AS  [AuditCreatedOn],				
				[ProjectFixedPriceView].[Audit_ModifiedBy]										AS  [AuditModifiedBy],				
				[ProjectFixedPriceView].[Audit_ModifiedOn]										AS  [AuditModifiedOn],				
				[ProjectFixedPriceView].[Audit_DeletedBy]										AS  [AuditDeletedBy],				
				[ProjectFixedPriceView].[Audit_DeletedOn]										AS  [AuditDeletedOn],				
				[ProjectFixedPriceView].[Audit_IsDeleted]										AS  [AuditIsDeleted],				
				[ProjectFixedPriceView].[Audit_VersionId]										AS  [AuditVersionId]	

	FROM		[ProjectFixedPriceView]

	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectFixedPriceView].[ProjectId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [AssessmentDevelopmentProjectView].[Id]
		AND		[ProjectView].[PricingModelId] = 2

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN	[Project2CrmProjectView]
		ON		[Project2CrmProjectView].[ProjectId] = [ProjectView].[Id]

	INNER JOIN	[CrmProjectView]
		ON		[CrmProjectView].[Id] = [Project2CrmProjectView].[CrmProjectId]
		AND		(@Date IS NULL OR @Date BETWEEN [CrmProjectView].[BookyearFrom] AND [CrmProjectView].[BookyearTo])

	INNER JOIN  [ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectView].[Id]

	INNER JOIN  [ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
		AND		[ProjectTypeCategoryView].[SubCategoryType] IS NULL		

	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		(@Date IS NULL OR [ProjectFixedPriceView].[Deadline] <= @Date)
		AND		(@Id IS NULL OR [ProjectFixedPriceView].[Id] = @Id)
		AND		(
					[ProjectFixedPriceView].[InvoiceStatusCode] < 100
					OR
					(
						[ProjectFixedPriceView].[InvoicedDate] >= DATEADD(MONTH, DATEDIFF(MONTH, 0, @Date)-1, 0)
					)
				)
		AND		[ProjectFixedPriceView].[InvoiceStatusCode] <> 15
		OR		[ProjectFixedPriceView].Id = @Id
END
