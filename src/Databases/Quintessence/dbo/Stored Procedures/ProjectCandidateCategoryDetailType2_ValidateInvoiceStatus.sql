CREATE PROCEDURE [dbo].[ProjectCandidateCategoryDetailType2_ValidateInvoiceStatus]
AS
BEGIN
	--ProjectCandidateCategoryDetailType2 Planned to To Verify
	UPDATE		[ProjectCandidateCategoryDetailType2]
	SET			[InvoiceStatusCode] = 30 --To Verify
	FROM		[ProjectCandidateCategoryDetailType2]
	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType2].[ProjectCandidateId]
	INNER JOIN	[ProjectCandidateDetailView] 
		ON		[ProjectCandidateDetailView].[Id] = [ProjectCandidateView].[Id]
	INNER JOIN	[ProjectCandidateCategoryDetailType2View]
		ON		[ProjectCandidateCategoryDetailType2View].[Id] = [ProjectCandidateCategoryDetailType2].[Id]
		AND		[ProjectCandidateCategoryDetailType2View].[InvoiceStatusCode] = 10 --Planned
		AND		COALESCE([ProjectCandidateCategoryDetailType2View].[Deadline], [ProjectCandidateDetailView].[AssessmentStartDate]) <= GETDATE()
	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1

	--ProjectCandidateCategoryDetailType2 To Verify to Planned
	UPDATE		[ProjectCandidateCategoryDetailType2]
	SET			[InvoiceStatusCode] = 10 --Planned 
	FROM		[ProjectCandidateCategoryDetailType2]
	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType2].[ProjectCandidateId]
	INNER JOIN	[ProjectCandidateDetailView] 
		ON		[ProjectCandidateDetailView].[Id] = [ProjectCandidateView].[Id]
	INNER JOIN	[ProjectCandidateCategoryDetailType2View]
		ON		[ProjectCandidateCategoryDetailType2View].[Id] = [ProjectCandidateCategoryDetailType2].[Id]
		AND		[ProjectCandidateCategoryDetailType2View].[InvoiceStatusCode] = 30 --To Verify
		AND		COALESCE([ProjectCandidateCategoryDetailType2View].[Deadline], [ProjectCandidateDetailView].[AssessmentStartDate]) > GETDATE()
	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1
END