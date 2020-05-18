
CREATE PROCEDURE [dbo].[AccountantInvoicing_TimesheetEntry]
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
				ISNULL([CrmReplicationAppointmentTimesheet].[StartDate],getdate()-1)				AS  [Date],
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
				[TimesheetEntryView].[Audit_VersionId]												AS  [AuditVersionId],
				[TimesheetEntryView].[FinancialEntityId]											AS	[FinancialEntityId]

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
	
	LEFT JOIN	[CrmReplicationAppointmentTimesheet]
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
							ISNULL([CrmReplicationAppointmentTimesheet].[StartDate],getdate()-1) <= @Date
							AND
							[TimesheetEntryView].[InvoiceStatusCode] < 100
						)
					)
				)
				AND [TimesheetEntryView].[InvoiceStatusCode] <> 15
				OR [TimesheetEntryView].[Id] = @Id
END
