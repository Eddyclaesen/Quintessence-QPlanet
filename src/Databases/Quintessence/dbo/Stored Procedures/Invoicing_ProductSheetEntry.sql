
CREATE PROCEDURE [dbo].[Invoicing_ProductSheetEntry]
	@Date					DATETIME,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId		UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Proma TABLE (Id UNIQUEIDENTIFIER)
	IF @ProjectManagerId = '0E689EFC-C4B6-4D35-A68C-6E9C29185002' INSERT INTO @Proma select Id from UserView where IsEmployee = 1 And RoleId is not null
	ELSE INSERT INTO @Proma SELECT @ProjectManagerId
	
	SELECT		[ProductSheetEntryView].[Id]														AS  [Id],							
				[ProductSheetEntryView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],
				[CrmContactView].[Id]																AS  [ContactId],	
				[CrmContactView].[Name]																AS	[ContactName],
				[ProductSheetEntryView].[InvoiceAmount]												AS  [InvoiceAmount],				
				[ProductSheetEntryView].[InvoiceStatusCode]											AS  [InvoiceStatusCode],					
				[ProductSheetEntryView].[Date]														AS  [Date],				
				[ProductSheetEntryView].[InvoicedDate]												AS  [InvoicedDate],				
				[ProductSheetEntryView].[PurchaseOrderNumber]										AS  [PurchaseOrderNumber],				
				[ProductSheetEntryView].[InvoiceNumber]												AS  [InvoiceNumber],					
				[ProductSheetEntryView].[InvoiceRemarks]											AS  [InvoiceRemarks],				
				CAST([ProductsheetEntryView].[Quantity] AS NVARCHAR(max))+'x '+[ProductSheetEntryView].[Name] AS [ProductName],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],
				NULL																				AS	[ConsultantFirstName],
				NULL																				AS	[ConsultantLastName],
				NULL																				AS	[ConsultantUserName],	
				[ProductSheetEntryView].[ProposalId]												AS  [ProposalId],	
				[ProductSheetEntryView].[Audit_CreatedBy]											AS  [AuditCreatedBy],				
				[ProductSheetEntryView].[Audit_CreatedOn]											AS  [AuditCreatedOn],				
				[ProductSheetEntryView].[Audit_ModifiedBy]											AS  [AuditModifiedBy],				
				[ProductSheetEntryView].[Audit_ModifiedOn]											AS  [AuditModifiedOn],				
				[ProductSheetEntryView].[Audit_DeletedBy]											AS  [AuditDeletedBy],				
				[ProductSheetEntryView].[Audit_DeletedOn]											AS  [AuditDeletedOn],				
				[ProductSheetEntryView].[Audit_IsDeleted]											AS  [AuditIsDeleted],				
				[ProductSheetEntryView].[Audit_VersionId]											AS  [AuditVersionId]	

	FROM		[ProductSheetEntryView]

	INNER JOIN  [ConsultancyProjectView]
		ON		[ConsultancyProjectView].[Id] = [ProductSheetEntryView].[ProjectId]
		
	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ConsultancyProjectView].[Id]
		AND		[ProjectView].[PricingModelId] = 1

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		(
					[ProductSheetEntryView].[InvoiceStatusCode] < 100
					OR
					(
						MONTH([ProductSheetEntryView].[InvoicedDate] ) = MONTH(@Date)
						AND
						YEAR([ProductSheetEntryView].[InvoicedDate]	) = YEAR(@Date)
					)					
				)
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] in (select Id from @Proma))
		AND		([ProductSheetEntryView].[InvoiceStatusCode] <> 15)		
		AND		([ProductSheetEntryView].[Date] <= @Date)
		
END
