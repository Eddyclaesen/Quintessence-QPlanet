
CREATE PROCEDURE [dbo].[Invoicing_ProjectProduct]
	@Date					DATETIME		 = NULL,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId		UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Proma TABLE (Id UNIQUEIDENTIFIER)
	IF @ProjectManagerId = '0E689EFC-C4B6-4D35-A68C-6E9C29185002' INSERT INTO @Proma select Id from UserView where IsEmployee = 1 And RoleId is not null
	ELSE INSERT INTO @Proma SELECT @ProjectManagerId
	
	SELECT		[ProjectProductView].[Id]														AS  [Id],							
				[ProjectProductView].[ProjectId]												AS  [ProjectId],	
				[ProjectView].[Name]															AS  [ProjectName],
				[CrmContactView].[Id]															AS  [ContactId],	
				[CrmContactView].[Name]															AS	[ContactName],
				[ProjectProductView].[InvoiceAmount]											AS  [InvoiceAmount],				
				[ProjectProductView].[InvoiceStatusCode]										AS  [InvoiceStatusCode],				
				[ProjectProductView].[InvoicedDate]												AS  [InvoicedDate],					
				[ProjectProductView].[Deadline]													AS  [Deadline],					
				[ProjectProductView].[PurchaseOrderNumber]										AS  [PurchaseOrderNumber],				
				[ProjectProductView].[InvoiceNumber]											AS  [InvoiceNumber],					
				[ProjectProductView].[InvoiceRemarks]											AS  [InvoiceRemarks],				
				[ProjectProductView].[ProductTypeName]											AS	[ProductName],
				[CustomerAssistant].[FirstName]													AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]													AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]													AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]													AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]														AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]														AS	[ProjectManagerUserName],
				NULL																			AS	[ConsultantFirstName],
				NULL																			AS	[ConsultantLastName],
				NULL																			AS	[ConsultantUserName],
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
					[ProjectProductView].[InvoiceStatusCode] < 100
					OR
					(
						MONTH([ProjectProductView].[InvoicedDate]) = MONTH(@Date)
						AND
						YEAR([ProjectProductView].[InvoicedDate]) = YEAR(@Date)
					)
				)
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] in (select Id from @Proma))
		AND		([ProjectProductView].[InvoiceStatusCode] <> 15)
		AND		(([ProjectProductView].[Deadline] <= @Date OR [ProjectProductView].[Deadline] IS NULL))
END
