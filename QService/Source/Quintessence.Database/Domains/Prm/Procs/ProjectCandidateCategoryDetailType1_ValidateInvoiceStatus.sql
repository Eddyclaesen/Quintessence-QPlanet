CREATE PROCEDURE [dbo].[ProjectCandidateCategoryDetailType1_ValidateInvoiceStatus]
AS
BEGIN
	--ProjectCandidateCategoryDetailType1 Planned to To Verify
	UPDATE		[ProjectCandidateCategoryDetailType1]
	SET			[InvoiceStatusCode] = 30 --To Verify
	FROM		[ProjectCandidateCategoryDetailType1]
	INNER JOIN	[ProjectCandidateCategoryDetailType1View]
		ON		[ProjectCandidateCategoryDetailType1View].[Id] = [ProjectCandidateCategoryDetailType1].[Id]
		AND		[ProjectCandidateCategoryDetailType1View].[InvoiceStatusCode] = 10 --Planned
		AND		[ProjectCandidateCategoryDetailType1View].[ScheduledDate] <= GETDATE()
	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCandidateId] 
	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1

	--ProjectCandidateCategoryDetailType1 To Verify to Planned
	UPDATE		[ProjectCandidateCategoryDetailType1]
	SET			[InvoiceStatusCode] = 10 --Planned
	FROM		[ProjectCandidateCategoryDetailType1]
	INNER JOIN	[ProjectCandidateCategoryDetailType1View]
		ON		[ProjectCandidateCategoryDetailType1View].[Id] = [ProjectCandidateCategoryDetailType1].[Id]
		AND		[ProjectCandidateCategoryDetailType1View].[InvoiceStatusCode] = 30 --To Verify
		AND		[ProjectCandidateCategoryDetailType1View].[ScheduledDate] > GETDATE()
	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCandidateId] 
	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1
	
END
GO

