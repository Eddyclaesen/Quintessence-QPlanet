CREATE VIEW [dbo].[ProjectCategoryDetailDictionaryIndicatorView] AS
	SELECT		[ProjectCategoryDetail2DictionaryIndicator].[Id]				AS		Id,
				[ProjectCategoryDetailView].[Id]								AS		ProjectCategoryDetailId,

				[DictionaryIndicatorView].[Id]									AS		DictionaryIndicatorId,
				[DictionaryIndicatorView].[Name]								AS		DictionaryIndicatorName,
				[DictionaryIndicatorView].[Order]								AS		DictionaryIndicatorOrder,

				[DictionaryLevelView].[Id]										AS		DictionaryLevelId,
				[DictionaryLevelView].[Name]									AS		DictionaryLevelName,
				[DictionaryLevelView].[Level]									AS		DictionaryLevelLevel,

				[DictionaryCompetenceView].[Id]									AS		DictionaryCompetenceId,
				[DictionaryCompetenceView].[Name]								AS		DictionaryCompetenceName,
				[DictionaryCompetenceView].[Order]								AS		DictionaryCompetenceOrder,

				[DictionaryClusterView].[Id]									AS		DictionaryClusterId,
				[DictionaryClusterView].[Name]									AS		DictionaryClusterName,
				[DictionaryClusterView].[Order]									AS		DictionaryClusterOrder,

				[ProjectCategoryDetail2DictionaryIndicator].[IsDefinedByRole]	AS		IsDefinedByRole,
				COALESCE([ProjectRoleDictionaryLevelView].[IsStandard], [ProjectCategoryDetail2DictionaryIndicator].[IsStandard], CAST(0 AS BIT))					
																				AS		IsStandard,
				COALESCE([ProjectRoleDictionaryLevelView].[IsDistinctive], [ProjectCategoryDetail2DictionaryIndicator].[IsDistinctive], CAST(0 AS BIT))				
																				AS		IsDistinctive

	FROM		[ProjectCategoryDetailView]
	
	INNER JOIN	[ProjectCategoryDetail2DictionaryIndicator]
		ON		[ProjectCategoryDetail2DictionaryIndicator].[ProjectCategoryDetailId] = [ProjectCategoryDetailView].[Id]

	INNER JOIN	[DictionaryIndicatorView]
		ON		[DictionaryIndicatorView].[Id] = [ProjectCategoryDetail2DictionaryIndicator].[DictionaryIndicatorId]

	INNER JOIN	[DictionaryLevelView]
		ON		[DictionaryLevelView].[Id] = [DictionaryIndicatorView].[DictionaryLevelId]

	INNER JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [DictionaryLevelView].[DictionaryCompetenceId]

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]
	
	LEFT JOIN	[ProjectCategoryFaDetailView]
		ON		[ProjectCategoryFaDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	
	LEFT JOIN	[ProjectCategoryFdDetailView]
		ON		[ProjectCategoryFdDetailView].[Id] = [ProjectCategoryDetailView].[Id]

	LEFT JOIN	[ProjectRoleDictionaryLevelView]
		ON		[ProjectRoleDictionaryLevelView].[DictionaryIndicatorId] = [ProjectCategoryDetail2DictionaryIndicator].[DictionaryIndicatorId]
		AND		(
					[ProjectRoleDictionaryLevelView].[ProjectRoleId] = [ProjectCategoryFaDetailView].[ProjectRoleId]
				OR	[ProjectRoleDictionaryLevelView].[ProjectRoleId] = [ProjectCategoryFdDetailView].[ProjectRoleId]
				)

