CREATE VIEW [dbo].[ProjectCandidateCategoryDetailTypeView] AS
	SELECT		[ProjectCandidateCategoryDetailType1].[Id]									AS	[Id],
				[ProjectCandidateCategoryDetailType1].[ProjectCandidateId]					AS	[ProjectCandidateId],
				[ProjectCandidateCategoryDetailType1].[ProjectCategoryDetailType1Id]		AS	[ProjectCategoryDetailTypeId],
				[ProjectCandidateCategoryDetailType1].[InvoiceAmount]						AS	[InvoiceAmount],
				[ProjectCandidateCategoryDetailType1].[InvoiceStatusCode]					AS	[InvoiceStatusCode],
				[ProjectCandidateCategoryDetailType1].[InvoicedDate]						AS	[InvoicedDate],
				[ProjectCandidateCategoryDetailType1].[InvoiceRemarks]						AS	[InvoiceRemarks],
				[ProjectCandidateCategoryDetailType1].[PurchaseOrderNumber]					AS	[PurchaseOrderNumber],
				[ProjectCandidateCategoryDetailType1].[InvoiceNumber]						AS	[InvoiceNumber],
				[ProjectCandidateCategoryDetailType1].[InvitationSentDate]					AS	[InvitationSentDate],
				[ProjectCandidateCategoryDetailType1].[DossierReadyDate]					AS	[DossierReadyDate],
				[ProjectCandidateCategoryDetailType1].[InvitationSentDateDone]				AS	[InvitationSentDateDone],
				[ProjectCandidateCategoryDetailType1].[DossierReadyDateDone]				AS	[DossierReadyDateDone],
				[ProjectCandidateCategoryDetailType1].[FollowUpDone]						AS	[FollowUpDone],
				[ProjectCandidateCategoryDetailType1].[Extra1]								AS	[Extra1],
				[ProjectCandidateCategoryDetailType1].[Extra2]								AS	[Extra2],
				[ProjectCandidateCategoryDetailType1].[Extra1Done]							AS	[Extra1Done],
				[ProjectCandidateCategoryDetailType1].[Extra2Done]							AS	[Extra2Done],
				[ProjectCategoryDetailType1View].[Name]										AS	[ProjectCategoryDetailTypeName],
				[ProjectCategoryDetailType1View].[SurveyPlanningId]							AS	[SurveyPlanningId],
				[ProjectCandidateCategoryDetailType1].[Audit_CreatedBy]						AS	[Audit_CreatedBy],
				[ProjectCandidateCategoryDetailType1].[Audit_CreatedOn]						AS	[Audit_CreatedOn],
				[ProjectCandidateCategoryDetailType1].[Audit_ModifiedBy]					AS	[Audit_ModifiedBy],
				[ProjectCandidateCategoryDetailType1].[Audit_ModifiedOn]					AS	[Audit_ModifiedOn],
				[ProjectCandidateCategoryDetailType1].[Audit_DeletedBy]						AS	[Audit_DeletedBy],
				[ProjectCandidateCategoryDetailType1].[Audit_DeletedOn]						AS	[Audit_DeletedOn],
				[ProjectCandidateCategoryDetailType1].[Audit_IsDeleted]						AS	[Audit_IsDeleted],
				[ProjectCandidateCategoryDetailType1].[Audit_VersionId]						AS	[Audit_VersionId]

	FROM		[ProjectCandidateCategoryDetailType1]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailType1View]
		ON		[ProjectCategoryDetailType1View].[Id] = [ProjectCandidateCategoryDetailType1].[ProjectCategoryDetailType1Id]

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType1].[ProjectCandidateId]

	UNION

	SELECT		[ProjectCandidateCategoryDetailType2].[Id]									AS	[Id],
				[ProjectCandidateCategoryDetailType2].[ProjectCandidateId]					AS	[ProjectCandidateId],
				[ProjectCandidateCategoryDetailType2].[ProjectCategoryDetailType2Id]		AS	[ProjectCategoryDetailTypeId],
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
				[ProjectCategoryDetailType2View].[Name]										AS	[ProjectCategoryDetailTypeName],
				[ProjectCategoryDetailType2View].[SurveyPlanningId]							AS	[SurveyPlanningId],
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

	UNION

	SELECT		[ProjectCandidateCategoryDetailType3].[Id]									AS	[Id],
				[ProjectCandidateCategoryDetailType3].[ProjectCandidateId]					AS	[ProjectCandidateId],
				[ProjectCandidateCategoryDetailType3].[ProjectCategoryDetailType3Id]		AS	[ProjectCategoryDetailTypeId],
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
				[ProjectCategoryDetailType3View].[Name]										AS	[ProjectCategoryDetailTypeName],
				[ProjectCategoryDetailType3View].[SurveyPlanningId]							AS	[SurveyPlanningId],
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
