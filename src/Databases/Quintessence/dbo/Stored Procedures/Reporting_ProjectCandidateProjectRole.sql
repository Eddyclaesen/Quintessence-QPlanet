CREATE PROCEDURE [dbo].[Reporting_ProjectCandidateProjectRole]
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[ProjectRoleTranslationView].[Text]

	FROM		[ProjectCandidateView]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
		AND		[ProjectTypeCategoryView].[IsMain] = 1

	LEFT JOIN	[ProjectCategoryFaDetailView]
		ON		[ProjectCategoryFaDetailView].[Id] = [ProjectCategoryDetailView].[Id]

	LEFT JOIN	[ProjectCategoryFdDetailView]
		ON		[ProjectCategoryFdDetailView].[Id] = [ProjectCategoryDetailView].[Id]

	LEFT JOIN	[ProjectRoleTranslationView]
		ON		(
					[ProjectRoleTranslationView].[ProjectRoleId] = [ProjectCategoryFaDetailView].[ProjectRoleId]
			OR		[ProjectRoleTranslationView].[ProjectRoleId] = [ProjectCategoryFdDetailView].[ProjectRoleId]
				)
		AND		[ProjectRoleTranslationView].[LanguageId] = [ProjectCandidateView].[ReportLanguageId]

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId
END