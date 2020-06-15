
CREATE VIEW [dbo].[ProjectCandidateCategoryDetailType1View] AS
	SELECT		[ProjectCandidateCategoryDetailType1].[Id]										AS	[Id],
				[ProjectCandidateCategoryDetailType1].[ProjectCandidateId]						AS	[ProjectCandidateId],
				[ProjectCandidateCategoryDetailType1].[ProjectCategoryDetailType1Id]			AS	[ProjectCategoryDetailTypeId],
				[CrmAppointmentView].[AppointmentDate]											AS	[ScheduledDate],
				COALESCE([CrmAppointmentView].[OfficeId], [ProjectCandidateView].[OfficeId])	AS	[OfficeId],
				[ProjectCandidateCategoryDetailType1].[InvoiceAmount]							AS	[InvoiceAmount],
				[ProjectCandidateCategoryDetailType1].[InvoiceStatusCode]						AS	[InvoiceStatusCode],
				[ProjectCandidateCategoryDetailType1].[InvoicedDate]							AS	[InvoicedDate],
				[ProjectCandidateCategoryDetailType1].[InvoiceRemarks]							AS	[InvoiceRemarks],
				[ProjectCandidateCategoryDetailType1].[PurchaseOrderNumber]						AS	[PurchaseOrderNumber],
				[ProjectCandidateCategoryDetailType1].[InvoiceNumber]							AS	[InvoiceNumber],
				[ProjectCandidateCategoryDetailType1].[InvitationSentDate]						AS	[InvitationSentDate],
				[ProjectCandidateCategoryDetailType1].[DossierReadyDate]						AS	[DossierReadyDate],
				[ProjectCandidateCategoryDetailType1].[InvitationSentDateDone]					AS	[InvitationSentDateDone],
				[ProjectCandidateCategoryDetailType1].[DossierReadyDateDone]					AS	[DossierReadyDateDone],
				[ProjectCandidateCategoryDetailType1].[FollowUpDone]							AS	[FollowUpDone],
				[ProjectCandidateCategoryDetailType1].[Extra1]									AS	[Extra1],
				[ProjectCandidateCategoryDetailType1].[Extra2]									AS	[Extra2],
				[ProjectCandidateCategoryDetailType1].[Extra1Done]								AS	[Extra1Done],
				[ProjectCandidateCategoryDetailType1].[Extra2Done]								AS	[Extra2Done],
				[ProjectCandidateCategoryDetailType1].[ProposalId]								AS	[ProposalId],
				[ProjectCategoryDetailType1View].[Name]											AS	[ProjectCategoryDetailTypeName],
				[ProjectCandidateCategoryDetailType1].[Audit_CreatedBy]							AS	[Audit_CreatedBy],
				[ProjectCandidateCategoryDetailType1].[Audit_CreatedOn]							AS	[Audit_CreatedOn],
				[ProjectCandidateCategoryDetailType1].[Audit_ModifiedBy]						AS	[Audit_ModifiedBy],
				[ProjectCandidateCategoryDetailType1].[Audit_ModifiedOn]						AS	[Audit_ModifiedOn],
				[ProjectCandidateCategoryDetailType1].[Audit_DeletedBy]							AS	[Audit_DeletedBy],
				[ProjectCandidateCategoryDetailType1].[Audit_DeletedOn]							AS	[Audit_DeletedOn],
				[ProjectCandidateCategoryDetailType1].[Audit_IsDeleted]							AS	[Audit_IsDeleted],
				[ProjectCandidateCategoryDetailType1].[Audit_VersionId]							AS	[Audit_VersionId],
				[ProjectCandidateCategoryDetailType1].[FinancialEntityId]						AS	[FinancialEntityId]

	FROM		[ProjectCandidateCategoryDetailType1]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailType1View]
		ON		[ProjectCategoryDetailType1View].[Id] = [ProjectCandidateCategoryDetailType1].[ProjectCategoryDetailType1Id]

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType1].[ProjectCandidateId]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailType1View].[Id]
		AND		[ProjectCategoryDetailView].[ProjectId] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]

	LEFT JOIN	[CrmAppointmentView]
		ON		[CrmAppointmentView].[Code] = [ProjectCandidateView].[Code]
		AND		[CrmAppointmentView].[TaskId] = [ProjectTypeCategoryView].[CrmTaskId]

	WHERE		(
					(
						[ProjectCategoryDetailType1View].[SurveyPlanningId] IN (2, 3) 
						AND 
						[CrmAppointmentView].[Id] IS NOT NULL
					)
				OR	(
						[ProjectCategoryDetailType1View].[SurveyPlanningId] IN (1, 4)
					)
				)
