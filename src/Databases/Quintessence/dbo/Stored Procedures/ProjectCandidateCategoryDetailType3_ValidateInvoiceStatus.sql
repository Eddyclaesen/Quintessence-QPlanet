CREATE PROCEDURE [dbo].[ProjectCandidateCategoryDetailType3_ValidateInvoiceStatus]
AS
BEGIN
	--ProjectCandidateCategoryDetailType3 Planned to To Verify
	UPDATE		[ProjectCandidateCategoryDetailType3]
	SET			[InvoiceStatusCode] = 30 --To Verify
	FROM		[ProjectCandidateCategoryDetailType3]
	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType3].[ProjectCandidateId]
	INNER JOIN	[ProjectCandidateDetailView] 
		ON		[ProjectCandidateDetailView].[Id] = [ProjectCandidateView].[Id]
	INNER JOIN	[ProjectCandidateCategoryDetailType3View]
		ON		[ProjectCandidateCategoryDetailType3View].[Id] = [ProjectCandidateCategoryDetailType3].[Id]
		AND		[ProjectCandidateCategoryDetailType3View].[InvoiceStatusCode] = 10 --Planned
		AND		COALESCE([ProjectCandidateCategoryDetailType3View].[Deadline], [ProjectCandidateDetailView].[AssessmentStartDate]) <= GETDATE()
	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1

	--ProjectCandidateCategoryDetailType3 To Verify to Planned 
	UPDATE		[ProjectCandidateCategoryDetailType3]
	SET			[InvoiceStatusCode] = 10 --Planned 
	FROM		[ProjectCandidateCategoryDetailType3]
	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType3].[ProjectCandidateId]
	INNER JOIN	[ProjectCandidateDetailView] 
		ON		[ProjectCandidateDetailView].[Id] = [ProjectCandidateView].[Id]
	INNER JOIN	[ProjectCandidateCategoryDetailType3View]
		ON		[ProjectCandidateCategoryDetailType3View].[Id] = [ProjectCandidateCategoryDetailType3].[Id]
		AND		[ProjectCandidateCategoryDetailType3View].[InvoiceStatusCode] = 30 --To Verify
		AND		COALESCE([ProjectCandidateCategoryDetailType3View].[Deadline], [ProjectCandidateDetailView].[AssessmentStartDate]) > GETDATE()
	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
		AND		[ProjectView].[PricingModelId] = 1
END