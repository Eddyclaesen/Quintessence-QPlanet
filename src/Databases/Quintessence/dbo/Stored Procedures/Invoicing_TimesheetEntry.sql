CREATE PROCEDURE [dbo].[Invoicing_TimesheetEntry]
	@Date					DATETIME,
	@CustomerAssistantId	UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId		UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Proma TABLE (Id UNIQUEIDENTIFIER)
	IF @ProjectManagerId = '0E689EFC-C4B6-4D35-A68C-6E9C29185002' INSERT INTO @Proma select Id from UserView where IsEmployee = 1 And RoleId is not null
	ELSE INSERT INTO @Proma SELECT @ProjectManagerId

	SELECT		[TimesheetEntryView].[Id]															AS  [Id],							
				[TimesheetEntryView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],
				[CrmContactView].[Id]																AS  [ContactId],	
				[CrmContactView].[Name]																AS	[ContactName],
				[TimesheetEntryView].[InvoiceAmount]												AS  [InvoiceAmount],				
				[TimesheetEntryView].[InvoiceStatusCode]											AS  [InvoiceStatusCode],				
				[TimesheetEntryView].[InvoicedDate]													AS  [InvoicedDate],				
				[TimesheetEntryView].[PurchaseOrderNumber]											AS  [PurchaseOrderNumber],				
				[TimesheetEntryView].[InvoiceNumber]												AS  [InvoiceNumber],					
				[TimesheetEntryView].[InvoiceRemarks]												AS  [InvoiceRemarks],				
				[TimesheetEntryView].[ActivityName]													AS	[ProductName],
				[CrmReplicationAppointmentTimesheet].[StartDate]									AS  [Date],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],
				[Consultant].[FirstName]															AS	[ConsultantFirstName],
				[Consultant].[LastName]																AS	[ConsultantLastName],
				[Consultant].[UserName]																AS	[ConsultantUserName],
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

	INNER JOIN	[UserView] [Consultant]
		ON		[Consultant].[Id] = [TimesheetEntryView].[UserId]

	INNER JOIN  [ConsultancyProjectView]
		ON		[ConsultancyProjectView].[Id] = [TimesheetEntryView].[ProjectId]
		
	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ConsultancyProjectView].[Id]
		AND		[ProjectView].[PricingModelId] = 1

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]
	
	LEFT JOIN	[CrmReplicationAppointmentTimesheet]
		ON		[CrmReplicationAppointmentTimesheet].[Id] = [TimesheetEntryView].[AppointmentId]
		
	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		(
					(
						MONTH([TimesheetEntryView].[InvoicedDate]) = MONTH(@Date)
						AND
						YEAR([TimesheetEntryView].[InvoicedDate]) = YEAR(@Date)
					)
					OR
					(
						[CrmReplicationAppointmentTimesheet].[StartDate] <= @Date
						AND
						[TimesheetEntryView].[InvoiceStatusCode] < 100
					)
				)
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] in (select Id from @Proma))
		AND		(
					[TimesheetEntryView].[InvoiceStatusCode] <> 15
					OR
					(
						MONTH([CrmReplicationAppointmentTimesheet].[StartDate]) = MONTH(@Date)
						AND
						YEAR([CrmReplicationAppointmentTimesheet].[StartDate]) = YEAR(@Date)
					)
				)
END
