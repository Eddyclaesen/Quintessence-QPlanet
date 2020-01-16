CREATE VIEW [dbo].[ProjectCandidateCategoryDetailType3View] AS
	SELECT		[ProjectCandidateCategoryDetailType3].[Id]									AS	[Id],
				[ProjectCandidateCategoryDetailType3].[ProjectCandidateId]					AS	[ProjectCandidateId],
				[ProjectCandidateCategoryDetailType3].[ProjectCategoryDetailType3Id]		AS	[ProjectCategoryDetailTypeId],
				[ProjectCandidateCategoryDetailType3].[Deadline]							AS	[Deadline],
				[ProjectCandidateCategoryDetailType3].[LoginCode]							AS	[LoginCode],
				[ProjectCandidateCategoryDetailType3].[InvoiceAmount]						AS	[InvoiceAmount],
				[ProjectCandidateCategoryDetailType3].[InvoiceStatusCode]					AS	[InvoiceStatusCode],
				[ProjectCandidateCategoryDetailType3].[InvoicedDate]						AS	[InvoicedDate],
				[ProjectCandidateCategoryDetailType3].[InvoiceRemarks]						AS	[InvoiceRemarks],
				[ProjectCandidateCategoryDetailType3].[PurchaseOrderNumber]					AS	[PurchaseOrderNumber],
				[ProjectCandidateCategoryDetailType3].[InvoiceNumber]						AS	[InvoiceNumber],
				[ProjectCandidateCategoryDetailType3].[InvitationSentDate]					AS	[InvitationSentDate],
				[ProjectCandidateCategoryDetailType3].[DossierReadyDate]					AS	[DossierReadyDate],
				[ProjectCandidateCategoryDetailType3].[InvitationSentDateDone]				AS	[InvitationSentDateDone],
				[ProjectCandidateCategoryDetailType3].[DossierReadyDateDone]				AS	[DossierReadyDateDone],
				[ProjectCandidateCategoryDetailType3].[FollowUpDone]						AS	[FollowUpDone],
				[ProjectCandidateCategoryDetailType3].[Extra1]								AS	[Extra1],
				[ProjectCandidateCategoryDetailType3].[Extra2]								AS	[Extra2],
				[ProjectCandidateCategoryDetailType3].[Extra1Done]							AS	[Extra1Done],
				[ProjectCandidateCategoryDetailType3].[Extra2Done]							AS	[Extra2Done],
				[ProjectCandidateCategoryDetailType3].[ProposalId]							AS	[ProposalId],
				[ProjectCategoryDetailType3View].[Name]										AS	[ProjectCategoryDetailTypeName],
				[ProjectCandidateCategoryDetailType3].[Audit_CreatedBy]						AS	[Audit_CreatedBy],
				[ProjectCandidateCategoryDetailType3].[Audit_CreatedOn]						AS	[Audit_CreatedOn],
				[ProjectCandidateCategoryDetailType3].[Audit_ModifiedBy]					AS	[Audit_ModifiedBy],
				[ProjectCandidateCategoryDetailType3].[Audit_ModifiedOn]					AS	[Audit_ModifiedOn],
				[ProjectCandidateCategoryDetailType3].[Audit_DeletedBy]						AS	[Audit_DeletedBy],
				[ProjectCandidateCategoryDetailType3].[Audit_DeletedOn]						AS	[Audit_DeletedOn],
				[ProjectCandidateCategoryDetailType3].[Audit_IsDeleted]						AS	[Audit_IsDeleted],
				[ProjectCandidateCategoryDetailType3].[Audit_VersionId]						AS	[Audit_VersionId]	

	FROM		[ProjectCandidateCategoryDetailType3]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailType3View]
		ON		[ProjectCategoryDetailType3View].[Id] = [ProjectCandidateCategoryDetailType3].[ProjectCategoryDetailType3Id]

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType3].[ProjectCandidateId]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailType3View].[Id]
		AND		[ProjectCategoryDetailView].[ProjectId] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]