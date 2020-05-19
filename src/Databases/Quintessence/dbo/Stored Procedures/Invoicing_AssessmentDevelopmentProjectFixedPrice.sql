
CREATE PROCEDURE [dbo].[Invoicing_AssessmentDevelopmentProjectFixedPrice]
	@Date					DATETIME,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId		UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Proma TABLE (Id UNIQUEIDENTIFIER)
	IF @ProjectManagerId = '0E689EFC-C4B6-4D35-A68C-6E9C29185002' INSERT INTO @Proma select Id from UserView where IsEmployee = 1 And RoleId is not null
	ELSE INSERT INTO @Proma SELECT @ProjectManagerId
	
	SELECT		[ProjectFixedPriceView].[Id]													AS  [Id],							
				[ProjectFixedPriceView].[ProjectId]												AS  [ProjectId],	
				[ProjectView].[Name]															AS  [ProjectName],	
				[CrmContactView].[Id]															AS  [ContactId],
				[CrmContactView].[name]															AS	[ContactName],
				[ProjectFixedPriceView].[Amount]												AS  [InvoiceAmount],				
				[ProjectFixedPriceView].[InvoiceStatusCode]										AS  [InvoiceStatusCode],				
				[ProjectFixedPriceView].[PurchaseOrderNumber]									AS  [PurchaseOrderNumber],				
				[ProjectFixedPriceView].[InvoiceNumber]											AS  [InvoiceNumber],					
				[ProjectFixedPriceView].[InvoiceRemarks]										AS  [InvoiceRemarks],				
				'Partial payment (ACDC)'														AS	[ProductName],
				[ProjectFixedPriceView].[Deadline]												AS  [Date],
				[CustomerAssistant].[FirstName]													AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]													AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]													AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]													AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]														AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]														AS	[ProjectManagerUserName],
				NULL																			AS	[ConsultantFirstName],
				NULL																			AS	[ConsultantLastName],
				NULL																			AS	[ConsultantUserName],	
				[ProjectFixedPriceView].[ProposalId]											AS  [ProposalId],	
				[ProjectFixedPriceView].[Audit_CreatedBy]										AS  [AuditCreatedBy],				
				[ProjectFixedPriceView].[Audit_CreatedOn]										AS  [AuditCreatedOn],				
				[ProjectFixedPriceView].[Audit_ModifiedBy]										AS  [AuditModifiedBy],				
				[ProjectFixedPriceView].[Audit_ModifiedOn]										AS  [AuditModifiedOn],				
				[ProjectFixedPriceView].[Audit_DeletedBy]										AS  [AuditDeletedBy],				
				[ProjectFixedPriceView].[Audit_DeletedOn]										AS  [AuditDeletedOn],				
				[ProjectFixedPriceView].[Audit_IsDeleted]										AS  [AuditIsDeleted],				
				[ProjectFixedPriceView].[Audit_VersionId]										AS  [AuditVersionId],
				[ProjectFixedPriceView].[FinancialEntityId]										AS	[FinancialEntityId]

	FROM		[ProjectFixedPriceView]

	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectFixedPriceView].[ProjectId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [AssessmentDevelopmentProjectView].[Id]
		AND		[ProjectView].[PricingModelId] = 2

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN  [ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectView].[Id]

	INNER JOIN  [ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
		AND		[ProjectTypeCategoryView].[SubCategoryType] IS NULL		

	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		[ProjectFixedPriceView].[Deadline] <= @Date
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] IN (Select Id from @Proma))
		AND		(
					[ProjectFixedPriceView].[InvoiceStatusCode] < 100
					OR
					(
						MONTH([ProjectFixedPriceView].[InvoicedDate]) = MONTH(@Date)
						AND
						YEAR([ProjectFixedPriceView].[InvoicedDate]) = YEAR(@Date)
					)
				)
		AND		(
					[ProjectFixedPriceView].[InvoiceStatusCode] <> 15
					OR
					(
						MONTH([ProjectFixedPriceView].[Deadline]) = MONTH(@Date)
						AND
						YEAR([ProjectFixedPriceView].[Deadline]) = YEAR(@Date)
					)
				)
END
