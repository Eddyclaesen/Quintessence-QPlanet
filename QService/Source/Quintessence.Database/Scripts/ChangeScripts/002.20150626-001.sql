ALTER PROCEDURE [dbo].[AccountantInvoicing_ProductSheetEntry]
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
				[ProductSheetEntryView].[Audit_VersionId]														AS  [AuditVersionId]	

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

GO

ALTER PROCEDURE [dbo].[AccountantInvoicing_ProjectProduct]
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
				[ProjectProductView].[Deadline]													AS  [Deadline],				
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
		AND		[ProjectProductView].[Deadline] <= @Date
		AND		[ProjectProductView].[InvoiceStatusCode] <> 15
		OR		[ProjectProductView].[Id] = @Id
END

GO

ALTER PROCEDURE [dbo].[Invoicing_ProductSheetEntry]
	@Date					DATETIME,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId		UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
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
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] = @ProjectManagerId)
		AND		([ProductSheetEntryView].[InvoiceStatusCode] <> 15)		
		AND		([ProductSheetEntryView].[Date] <= @Date)
		
END

GO

ALTER PROCEDURE [dbo].[Invoicing_ProjectProduct]
	@Date					DATETIME		 = NULL,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId		UNIQUEIDENTIFIER = NULL
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
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] = @ProjectManagerId)
		AND		([ProjectProductView].[InvoiceStatusCode] <> 15)
		AND		([ProjectProductView].[Deadline] <= @Date)
END

GO

ALTER PROCEDURE [dbo].[AccountantInvoicing_TimesheetEntry]
	@Date			DATETIME = NULL,
	@Id				UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[TimesheetEntryView].[Id]															AS  [Id],							
				[TimesheetEntryView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				[CrmContactView].[Id]																AS  [ContactId],
				[CrmContactView].[Name]																AS	[ContactName],
				[TimesheetEntryView].[InvoiceAmount]												AS  [InvoiceAmount],				
				[TimesheetEntryView].[InvoiceStatusCode]											AS  [InvoiceStatusCode],				
				[TimesheetEntryView].[PurchaseOrderNumber]											AS  [PurchaseOrderNumber],				
				[TimesheetEntryView].[InvoiceNumber]												AS  [InvoiceNumber],					
				[TimesheetEntryView].[InvoiceRemarks]												AS  [InvoiceRemarks],					
				[TimesheetEntryView].[InvoicedDate]													AS  [InvoicedDate],				
				[TimesheetEntryView].[ActivityName]													AS	[ProductName],
				[CrmProjectView].[Name]																AS	[CrmProjectName],
				ISNULL([TimesheetEntryView].[Description],'No Description')							AS	[Description],
				[CrmReplicationAppointmentTimesheet].[StartDate]									AS  [Date],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],
				[TimesheetEntryView].[ProposalId]													AS  [ProposalId],
				[TimesheetEntryView].[Audit_CreatedBy]												AS  [AuditCreatedBy],				
				[TimesheetEntryView].[Audit_CreatedOn]												AS  [AuditCreatedOn],				
				[TimesheetEntryView].[Audit_ModifiedBy]												AS  [AuditModifiedBy],				
				[TimesheetEntryView].[Audit_ModifiedOn]												AS  [AuditModifiedOn],				
				[TimesheetEntryView].[Audit_DeletedBy]												AS  [AuditDeletedBy],				
				[TimesheetEntryView].[Audit_DeletedOn]												AS  [AuditDeletedOn],				
				[TimesheetEntryView].[Audit_IsDeleted]												AS  [AuditIsDeleted],				
				[TimesheetEntryView].[Audit_VersionId]												AS  [AuditVersionId]	

	FROM		[TimesheetEntryView]

	INNER JOIN  [ConsultancyProjectView]
		ON		[ConsultancyProjectView].[Id] = [TimesheetEntryView].[ProjectId]
		
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
	
	INNER JOIN	[CrmReplicationAppointmentTimesheet]
		ON		[CrmReplicationAppointmentTimesheet].[Id] = [TimesheetEntryView].[AppointmentId]
		
	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		(
					@Id IS NULL
					AND
					(
						(
							[TimesheetEntryView].[InvoicedDate] >= DATEADD(MONTH, DATEDIFF(MONTH, 0, @Date)-1, 0)
						)
						OR
						(
							[CrmReplicationAppointmentTimesheet].[StartDate] <= @Date
							AND
							[TimesheetEntryView].[InvoiceStatusCode] < 100
						)
					)
				)
				AND [TimesheetEntryView].[InvoiceStatusCode] <> 15
				OR [TimesheetEntryView].[Id] = @Id
END

GO