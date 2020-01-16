CREATE VIEW [dbo].[ProjectCandidateCategoryDetailType2View] AS
	SELECT		[ProjectCandidateCategoryDetailType2].[Id]									AS	[Id],
				[ProjectCandidateCategoryDetailType2].[ProjectCandidateId]					AS	[ProjectCandidateId],
				[ProjectCandidateCategoryDetailType2].[ProjectCategoryDetailType2Id]		AS	[ProjectCategoryDetailTypeId],
				[ProjectCandidateCategoryDetailType2].[Deadline]							AS	[Deadline],
				[ProjectCandidateCategoryDetailType2].[InvoiceAmount]						AS	[InvoiceAmount],
				[ProjectCandidateCategoryDetailType2].[InvoiceStatusCode]					AS	[InvoiceStatusCode],
				[ProjectCandidateCategoryDetailType2].[InvoicedDate]						AS	[InvoicedDate],
				[ProjectCandidateCategoryDetailType2].[InvoiceRemarks]						AS	[InvoiceRemarks],
				[ProjectCandidateCategoryDetailType2].[PurchaseOrderNumber]					AS	[PurchaseOrderNumber],
				[ProjectCandidateCategoryDetailType2].[InvoiceNumber]						AS	[InvoiceNumber],
				[ProjectCandidateCategoryDetailType2].[InvitationSentDate]					AS	[InvitationSentDate],
				[ProjectCandidateCategoryDetailType2].[DossierReadyDate]					AS	[DossierReadyDate],
				[ProjectCandidateCategoryDetailType2].[InvitationSentDateDone]				AS	[InvitationSentDateDone],
				[ProjectCandidateCategoryDetailType2].[DossierReadyDateDone]				AS	[DossierReadyDateDone],
				[ProjectCandidateCategoryDetailType2].[FollowUpDone]						AS	[FollowUpDone],
				[ProjectCandidateCategoryDetailType2].[Extra1]								AS	[Extra1],
				[ProjectCandidateCategoryDetailType2].[Extra2]								AS	[Extra2],
				[ProjectCandidateCategoryDetailType2].[Extra1Done]							AS	[Extra1Done],
				[ProjectCandidateCategoryDetailType2].[Extra2Done]							AS	[Extra2Done],
				[ProjectCandidateCategoryDetailType2].[ProposalId]							AS	[ProposalId],
				[ProjectCategoryDetailType2View].[Name]										AS	[ProjectCategoryDetailTypeName],
				[ProjectCandidateCategoryDetailType2].[Audit_CreatedBy]						AS	[Audit_CreatedBy],
				[ProjectCandidateCategoryDetailType2].[Audit_CreatedOn]						AS	[Audit_CreatedOn],
				[ProjectCandidateCategoryDetailType2].[Audit_ModifiedBy]					AS	[Audit_ModifiedBy],
				[ProjectCandidateCategoryDetailType2].[Audit_ModifiedOn]					AS	[Audit_ModifiedOn],
				[ProjectCandidateCategoryDetailType2].[Audit_DeletedBy]						AS	[Audit_DeletedBy],
				[ProjectCandidateCategoryDetailType2].[Audit_DeletedOn]						AS	[Audit_DeletedOn],
				[ProjectCandidateCategoryDetailType2].[Audit_IsDeleted]						AS	[Audit_IsDeleted],
				[ProjectCandidateCategoryDetailType2].[Audit_VersionId]						AS	[Audit_VersionId]	

	FROM		[ProjectCandidateCategoryDetailType2]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailType2View]
		ON		[ProjectCategoryDetailType2View].[Id] = [ProjectCandidateCategoryDetailType2].[ProjectCategoryDetailType2Id]

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType2].[ProjectCandidateId]
