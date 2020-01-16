CREATE PROCEDURE Reporting_ProjectCandidateIncludeInReport
	@ProjectCandidateId	UNIQUEIDENTIFIER,
	@Code				NVARCHAR(MAX)
AS
BEGIN
	DECLARE		@IncludeInCandidateReport AS BIT = 0
	
	SELECT		@IncludeInCandidateReport = [ProjectCategoryDetailType3View].[IncludeInCandidateReport]
	
	FROM		[ProjectCandidateCategoryDetailType3View]

	INNER JOIN	[ProjectCategoryDetailType3View]
		ON		[ProjectCategoryDetailType3View].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailType3View].[Id]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
		AND		[ProjectTypeCategoryView].[Code] = @Code

	WHERE		[ProjectCandidateCategoryDetailType3View].[ProjectCandidateId] = @ProjectCandidateId
	
	SELECT		@IncludeInCandidateReport
END
GO
