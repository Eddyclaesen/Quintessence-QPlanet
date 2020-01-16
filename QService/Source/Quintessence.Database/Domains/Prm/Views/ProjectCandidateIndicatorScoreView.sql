
CREATE VIEW [dbo].[ProjectCandidateIndicatorScoreView] AS
	SELECT		[ProjectCandidateIndicatorScore].*,
				COALESCE([ProjectRoleDictionaryLevelView].[IsStandard], [ProjectCategoryDetail2DictionaryIndicator].[IsStandard], CAST(0 AS BIT))						
																											AS	[IsStandard],
				COALESCE([ProjectRoleDictionaryLevelView].[IsDistinctive], [ProjectCategoryDetail2DictionaryIndicator].[IsDistinctive], CAST(0 AS BIT))					
																											AS	[IsDistinctive]
				
	FROM		[ProjectCandidateIndicatorScore]	WITH (NOLOCK)

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateIndicatorScore].[ProjectCandidateId]

	INNER JOIN	[DictionaryIndicatorView]
		ON		[DictionaryIndicatorView].[Id] = [ProjectCandidateIndicatorScore].[DictionaryIndicatorId]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectCandidateView].[ProjectId]

	LEFT JOIN	[ProjectCategoryFaDetailView]
		ON		[ProjectCategoryFaDetailView].[Id] = [ProjectCategoryDetailView].[Id]	

	LEFT JOIN	[ProjectCategoryFdDetailView]
		ON		[ProjectCategoryFdDetailView].[Id] = [ProjectCategoryDetailView].[Id]

	LEFT JOIN	[ProjectRoleDictionaryLevelView]
		ON		[ProjectRoleDictionaryLevelView].[DictionaryIndicatorId] = [DictionaryIndicatorView].[Id]
		AND		(
					[ProjectRoleDictionaryLevelView].[ProjectRoleId] = [ProjectCategoryFaDetailView].[ProjectRoleId] 
				OR	[ProjectRoleDictionaryLevelView].[ProjectRoleId] = [ProjectCategoryFdDetailView].[ProjectRoleId]
				)

	INNER JOIN	[ProjectCategoryDetail2DictionaryIndicator]
		ON		[ProjectCategoryDetail2DictionaryIndicator].[ProjectCategoryDetailId] = [ProjectCategoryDetailView].[Id]
		AND		[ProjectCategoryDetail2DictionaryIndicator].[DictionaryIndicatorId] = [ProjectCandidateIndicatorScore].[DictionaryIndicatorId]

	WHERE		[ProjectCandidateIndicatorScore].[Audit_IsDeleted] = 0