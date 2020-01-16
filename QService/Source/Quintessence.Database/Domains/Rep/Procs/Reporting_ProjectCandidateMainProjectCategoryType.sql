CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateMainProjectCategoryType]
	@ProjectCandidateId UNIQUEIDENTIFIER
AS
	SELECT		[ProjectTypeCategoryView].[Code] 
	
	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCategoryDetailView] 
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectCandidateView].[ProjectId]
	
	INNER JOIN	[ProjectTypeCategoryView] 
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId] 
		AND		[ProjectTypeCategoryView].[IsMain] = 1

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId
		





