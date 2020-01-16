CREATE PROCEDURE [dbo].[AccountantInvoicing_ProjectProduct]
	@Date				DATETIME = NULL,
	@Id		AS UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[ProjectProductView].[Id]														AS  [Id],							
				[ProjectProductView].[ProjectId]												AS  [ProjectId],	
				[ProjectView].[Name]															AS  [ProjectName],
				[CrmContactView].[Id]															AS  [ContactId],	
				[CrmContactView].[Name]															AS	[ContactName],
				[ProjectProductView].[InvoiceAmount]											AS  [InvoiceAmount],				
				[ProjectProductView].[InvoiceStatusCode]										AS  [InvoiceStatusCode],				
				[ProjectProductView].[InvoiceRemarks]											AS  [InvoiceRemarks],				
				[ProjectProductView].[InvoicedDate]												AS  [InvoicedDate],				
				[ProjectProductView].[PurchaseOrderNumber]										AS  [PurchaseOrderNumber],				
				[ProjectProductView].[InvoiceNumber]											AS  [InvoiceNumber],					
				[ProjectProductView].[ProductTypeName]											AS	[ProductName],
				[CrmProjectView].[Name]															AS	[CrmProjectName],
				[CustomerAssistant].[FirstName]													AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]													AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]													AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]													AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]														AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]														AS	[ProjectManagerUserName],
				[ProjectProductView].[ProposalId]												AS  [ProposalId],
				[ProjectProductView].[Audit_CreatedBy]											AS  [AuditCreatedBy],				
				[ProjectProductView].[Audit_CreatedOn]											AS  [AuditCreatedOn],				
				[ProjectProductView].[Audit_ModifiedBy]											AS  [AuditModifiedBy],				
				[ProjectProductView].[Audit_ModifiedOn]											AS  [AuditModifiedOn],				
				[ProjectProductView].[Audit_DeletedBy]											AS  [AuditDeletedBy],				
				[ProjectProductView].[Audit_DeletedOn]											AS  [AuditDeletedOn],				
				[ProjectProductView].[Audit_IsDeleted]											AS  [AuditIsDeleted],				
				[ProjectProductView].[Audit_VersionId]											AS  [AuditVersionId]	

	FROM		[ProjectProductView]

	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectProductView].[ProjectId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [AssessmentDevelopmentProjectView].[Id]
		AND		[ProjectView].[PricingModelId] = 1

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

	WHERE		(
					@Id IS NULL
					AND
					(
						[ProjectProductView].[InvoiceStatusCode] < 100
						OR
						(
							MONTH([ProjectProductView].[InvoicedDate]) = MONTH(@Date)
							AND
							YEAR([ProjectProductView].[InvoicedDate]) = YEAR(@Date)
						)
					)
				)
		--OR		[ProjectProductView].[Id] = @Id
		AND		[ProjectProductView].[InvoiceStatusCode] <> 15
END
GO