
CREATE PROCEDURE [dbo].[AccountantInvoicing_ProductSheetEntry]
	@Date					DATETIME = NULL,
	@Id				UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[ProductSheetEntryView].[Id]																	AS  [Id],							
				[ProductSheetEntryView].[ProjectId]																AS  [ProjectId],	
				[ProjectView].[Name]																			AS  [ProjectName],
				[CrmContactView].[Id]																			AS  [ContactId],		
				[CrmContactView].[Name]																			AS	[ContactName],
				[ProductSheetEntryView].[InvoiceAmount]															AS  [InvoiceAmount],				
				[ProductSheetEntryView].[InvoiceStatusCode]														AS  [InvoiceStatusCode],					
				[ProductSheetEntryView].[Date]																	AS  [Date],					
				[ProductSheetEntryView].[PurchaseOrderNumber]													AS  [PurchaseOrderNumber],				
				[ProductSheetEntryView].[InvoiceNumber]															AS  [InvoiceNumber],					
				[ProductSheetEntryView].[InvoiceRemarks]														AS  [InvoiceRemarks],					
				[ProductSheetEntryView].[InvoicedDate]															AS  [InvoicedDate],				
				CAST([ProductsheetEntryView].[Quantity] AS NVARCHAR(max))+'x '+[ProductSheetEntryView].[Name]	AS [ProductName],
				[CrmProjectView].[Name]																			AS	[CrmProjectName],
				[CustomerAssistant].[FirstName]																	AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]																	AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]																	AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]																	AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]																		AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]																		AS	[ProjectManagerUserName],
				[ProductSheetEntryView].[ProposalId]																		AS  [ProposalId],
				[ProductSheetEntryView].[Audit_CreatedBy]														AS  [AuditCreatedBy],				
				[ProductSheetEntryView].[Audit_CreatedOn]														AS  [AuditCreatedOn],				
				[ProductSheetEntryView].[Audit_ModifiedBy]														AS  [AuditModifiedBy],				
				[ProductSheetEntryView].[Audit_ModifiedOn]														AS  [AuditModifiedOn],				
				[ProductSheetEntryView].[Audit_DeletedBy]														AS  [AuditDeletedBy],				
				[ProductSheetEntryView].[Audit_DeletedOn]														AS  [AuditDeletedOn],				
				[ProductSheetEntryView].[Audit_IsDeleted]														AS  [AuditIsDeleted],				
				[ProductSheetEntryView].[Audit_VersionId]														AS  [AuditVersionId],
				[ProductSheetEntryView].[FinancialEntityId]														AS	[FinancialEntityId]	

	FROM		[ProductSheetEntryView]

	INNER JOIN  [ConsultancyProjectView]
		ON		[ConsultancyProjectView].[Id] = [ProductSheetEntryView].[ProjectId]
	
	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ConsultancyProjectView].[Id]
		AND		[ProjectView].[PricingModelId] = 1

	INNER JOIN	[Project2CrmProjectView]
		ON		[Project2CrmProjectView].[ProjectId] = [ProjectView].[Id]

	INNER JOIN	[CrmProjectView]
		ON		[CrmProjectView].[Id] = [Project2CrmProjectView].[CrmProjectId]
		AND		(@Date IS NULL OR @Date BETWEEN [CrmProjectView].[BookyearFrom] AND [CrmProjectView].[BookyearTo])

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		(@Id IS NULL OR [ProductSheetEntryView].[Id] = @Id)
		AND		(
					[ProductSheetEntryView].[InvoiceStatusCode] < 100
					OR
					(
						MONTH([ProductSheetEntryView].[InvoicedDate] ) = MONTH(@Date)
						AND
						YEAR([ProductSheetEntryView].[InvoicedDate]	) = YEAR(@Date)
					)	
				)
		AND		[ProductSheetEntryView].[InvoiceStatusCode] <> 15
		AND		[ProductsheetEntryView].[Date] <= @Date
		OR		[ProductSheetEntryView].Id = @Id
END
