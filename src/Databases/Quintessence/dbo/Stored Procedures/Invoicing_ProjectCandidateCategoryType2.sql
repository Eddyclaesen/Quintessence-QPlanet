CREATE PROCEDURE [dbo].[Invoicing_ProjectCandidateCategoryType2]
	@Date									DATETIME,
	@CustomerAssistantId					UNIQUEIDENTIFIER = NULL,
	@ProjectManagerId						UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Proma TABLE (Id UNIQUEIDENTIFIER)
	IF @ProjectManagerId = '0E689EFC-C4B6-4D35-A68C-6E9C29185002' INSERT INTO @Proma select Id from UserView where IsEmployee = 1 And RoleId is not null
	ELSE INSERT INTO @Proma SELECT @ProjectManagerId

		SELECT	[ProjectCandidateCategoryDetailType2View].[Id]										AS  [Id],							
				[ProjectCandidateView].[ProjectId]													AS  [ProjectId],	
				[ProjectView].[Name]																AS  [ProjectName],	
				[ProjectCandidateView].[CandidateFirstName]											AS	[CandidateFirstName],
				[ProjectCandidateView].[CandidateLastName]											AS	[CandidateLastName],
				[CrmContactView].[Id]																AS  [ContactId],
				[CrmContactView].[name]																AS	[ContactName],
				[ProjectCandidateCategoryDetailType2View].[InvoiceAmount]							AS  [InvoiceAmount],				
				[ProjectCandidateCategoryDetailType2View].[InvoiceStatusCode]						AS  [InvoiceStatusCode],				
				[ProjectCandidateCategoryDetailType2View].[InvoicedDate]							AS  [InvoicedDate],				
				[ProjectCandidateCategoryDetailType2View].[PurchaseOrderNumber]						AS  [PurchaseOrderNumber],				
				[ProjectCandidateCategoryDetailType2View].[InvoiceNumber]							AS  [InvoiceNumber],					
				[ProjectCandidateCategoryDetailType2View].[InvoiceRemarks]							AS  [InvoiceRemarks],				
				[ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeName]			AS	[ProductName],
				CASE [ProjectCategoryDetailType2View].[SurveyPlanningId]
					WHEN 2 THEN [ProjectCandidateCategoryDetailType2View].[Deadline]
					WHEN 3 THEN [ProjectCandidateCategoryDetailType2View].[Deadline]
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
				[ProjectCandidateCategoryDetailType2View].[ProposalId]								AS  [ProposalId],				
				[ProjectCandidateCategoryDetailType2View].[Audit_CreatedBy]							AS  [AuditCreatedBy],				
				[ProjectCandidateCategoryDetailType2View].[Audit_CreatedOn]							AS  [AuditCreatedOn],				
				[ProjectCandidateCategoryDetailType2View].[Audit_ModifiedBy]						AS  [AuditModifiedBy],				
				[ProjectCandidateCategoryDetailType2View].[Audit_ModifiedOn]						AS  [AuditModifiedOn],				
				[ProjectCandidateCategoryDetailType2View].[Audit_DeletedBy]							AS  [AuditDeletedBy],				
				[ProjectCandidateCategoryDetailType2View].[Audit_DeletedOn]							AS  [AuditDeletedOn],				
				[ProjectCandidateCategoryDetailType2View].[Audit_IsDeleted]							AS  [AuditIsDeleted],				
				[ProjectCandidateCategoryDetailType2View].[Audit_VersionId]							AS  [AuditVersionId]	

	FROM		[ProjectCandidateCategoryDetailType2View]

	INNER JOIN	[ProjectCategoryDetailType2View] 
		ON		[ProjectCategoryDetailType2View].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeId] 
 
	INNER JOIN  [ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCandidateId]

	INNER JOIN	[CrmReplicationAppointment]
		ON		[CrmReplicationAppointment].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]

	INNER JOIN  [ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [ProjectView].[ContactId]

	INNER JOIN  [ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [projectcategorydetailView].[ProjectTypeCategoryId]

	INNER JOIN  [AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		
	INNER JOIN	[UserView] [ProjectManager]
		ON		[ProjectManager].[Id] = [ProjectView].[ProjectManagerId]
		
	INNER JOIN	[UserView] [CustomerAssistant]
		ON		[CustomerAssistant].[Id] = [ProjectView].[CustomerAssistantId]

	WHERE		[ProjectCandidateCategoryDetailType2View].[InvoiceAmount] > 0
				AND
				(
					(
						MONTH([ProjectCandidateCategoryDetailType2View].[InvoicedDate]	) = MONTH(@Date)
						AND
						YEAR([ProjectCandidateCategoryDetailType2View].[InvoicedDate]	) = YEAR(@Date)
					)
					OR
					(
						(
							(
								[ProjectCategoryDetailType2View].[SurveyPlanningId] NOT IN (2,3) --before (2) & after (3)
								AND
								[CrmReplicationAppointment].[AppointmentDate] <= @Date
							)
							OR
							[ProjectCandidateCategoryDetailType2View].[Deadline] <= @Date
						)
						AND
						[ProjectCandidateCategoryDetailType2View].[InvoiceStatusCode] < 100
					)
				)
		AND		(@CustomerAssistantId IS NULL OR [CustomerAssistant].[Id] = @CustomerAssistantId)
		AND		(@ProjectManagerId IS NULL OR [ProjectManager].[Id] in (select Id from @Proma))
		AND		(
					[ProjectCandidateCategoryDetailType2View].[InvoiceStatusCode] <> 15
					OR
					(
						MONTH([ProjectCandidateCategoryDetailType2View].[Deadline]) = MONTH(@Date)
						AND
						YEAR([ProjectCandidateCategoryDetailType2View].[Deadline]) = YEAR(@Date)
					)
				)

END
