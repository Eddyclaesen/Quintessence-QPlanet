
CREATE PROCEDURE [dbo].[Invoicing_ProjectCandidateCategoryType3]
	@Date								DATETIME,
	@CustomerAssistantId				UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId					UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Proma TABLE (Id UNIQUEIDENTIFIER)
	IF @ProjectManagerId = '0E689EFC-C4B6-4D35-A68C-6E9C29185002' INSERT INTO @Proma select Id from UserView where IsEmployee = 1 And RoleId is not null
	ELSE INSERT INTO @Proma SELECT @ProjectManagerId

		SELECT	[ProjectCandidateCategoryDetailType3View].[Id]										AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				[ProjectCandidateView].[CandidateFirstName]											AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[CrmContactView].[Id]																AS  [ContactId],
				[CrmContactView].[name]																AS	[ContactName],
				[ProjectCandidateCategoryDetailType3View].[InvoiceAmount]							AS  [InvoiceAmount],				
				[ProjectCandidateCategoryDetailType3View].[InvoiceStatusCode]						AS  [InvoiceStatusCode],				
				[ProjectCandidateCategoryDetailType3View].[InvoicedDate]							AS  [InvoicedDate],				
				[ProjectCandidateCategoryDetailType3View].[PurchaseOrderNumber]						AS  [PurchaseOrderNumber],				
				[ProjectCandidateCategoryDetailType3View].[InvoiceNumber]							AS  [InvoiceNumber],					
				[ProjectCandidateCategoryDetailType3View].[InvoiceRemarks]							AS  [InvoiceRemarks],				
				[ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeName]			AS	[ProductName],
				CASE [ProjectCategoryDetailType3View].[SurveyPlanningId]
					WHEN 2 THEN [ProjectCandidateCategoryDetailType3View].[Deadline]
					WHEN 3 THEN [ProjectCandidateCategoryDetailType3View].[Deadline]
					ELSE [CrmReplicationAppointment].[AppointmentDate]
				END																					AS  [Date],
				[CustomerAssistant].[FirstName]														AS	[CustomerAssistantFirstName],
				[CustomerAssistant].[LastName]														AS	[CustomerAssistantLastName],
				[CustomerAssistant].[UserName]														AS	[CustomerAssistantUserName],
				[ProjectManager].[FirstName]														AS	[ProjectManagerFirstName],
				[ProjectManager].[LastName]															AS	[ProjectManagerLastName],
				[ProjectManager].[UserName]															AS	[ProjectManagerUserName],
				NULL																				AS	[ConsultantFirstName],
				NULL																				AS	[ConsultantLastName],
				NULL																				AS	[ConsultantUserName],
				[ProjectCandidateCategoryDetailType3View].[ProposalId]								AS  [ProposalId],				
				[ProjectCandidateCategoryDetailType3View].[Audit_CreatedBy]							AS  [AuditCreatedBy],				
				[ProjectCandidateCategoryDetailType3View].[Audit_CreatedOn]							AS  [AuditCreatedOn],				
				[ProjectCandidateCategoryDetailType3View].[Audit_ModifiedBy]						AS  [AuditModifiedBy],				
				[ProjectCandidateCategoryDetailType3View].[Audit_ModifiedOn]						AS  [AuditModifiedOn],				
				[ProjectCandidateCategoryDetailType3View].[Audit_DeletedBy]							AS  [AuditDeletedBy],				
				[ProjectCandidateCategoryDetailType3View].[Audit_DeletedOn]							AS  [AuditDeletedOn],				
				[ProjectCandidateCategoryDetailType3View].[Audit_IsDeleted]							AS  [AuditIsDeleted],				
				[ProjectCandidateCategoryDetailType3View].[Audit_VersionId]							AS  [AuditVersionId],
				[ProjectCandidateCategoryDetailType3View].[FinancialEntityId]						AS	[FinancialEntityId]

	FROM		[ProjectCandidateCategoryDetailType3View]

	INNER JOIN	[ProjectCategoryDetailType3View] 
		ON		[ProjectCategoryDetailType3View].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeId] 
 
	INNER JOIN  [ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCandidateId]

	INNER JOIN	[CrmReplicationAppointment]
		ON		[CrmReplicationAppointment].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

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

	WHERE		[ProjectCandidateCategoryDetailType3View].[InvoiceAmount] > 0
				AND
				(
					(
						MONTH([ProjectCandidateCategoryDetailType3View].[InvoicedDate]	) = MONTH(@Date)
						AND
						YEAR([ProjectCandidateCategoryDetailType3View].[InvoicedDate]	) = YEAR(@Date)
					)
					OR
					(
						(
							(
								[ProjectCategoryDetailType3View].[SurveyPlanningId] NOT IN (2,3) --before (2) & after (3)
								AND
								[CrmReplicationAppointment].[AppointmentDate] <= @Date
							)
							OR
							[ProjectCandidateCategoryDetailType3View].[Deadline] <= @Date
						)
						AND
						[ProjectCandidateCategoryDetailType3View].[InvoiceStatusCode] < 100
					)
				)
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] in (select Id from @Proma))
		AND		(
					[ProjectCandidateCategoryDetailType3View].[InvoiceStatusCode] <> 15
					OR
					(
						MONTH([ProjectCandidateCategoryDetailType3View].[Deadline]) = MONTH(@Date)
						AND
						YEAR([ProjectCandidateCategoryDetailType3View].[Deadline]) = YEAR(@Date)
					)
				)

END
