CREATE VIEW [dbo].[DictionaryIndicatorMatrixEntryView] AS
	SELECT		[DictionaryIndicatorView].[Id]		AS Id,
				[DictionaryIndicatorView].[Name]	AS DictionaryIndicatorName,
				[DictionaryIndicatorView].[Order]	AS DictionaryIndicatorOrder,

				[DictionaryLevelView].[Id]			AS DictionaryLevelId,
				[DictionaryLevelView].[Level]		AS DictionaryLevelLevel,
				[DictionaryLevelView].[Name]		AS DictionaryLevelName,
				
				[DictionaryCompetenceView].[Id]		AS DictionaryCompetenceId,
				[DictionaryCompetenceView].[Name]	AS DictionaryCompetenceName,
				[DictionaryCompetenceView].[Order]	AS DictionaryCompetenceOrder,
				
				[DictionaryClusterView].[Id]		AS DictionaryClusterId,
				[DictionaryClusterView].[Name]		AS DictionaryClusterName,
				[DictionaryClusterView].[Order]		AS DictionaryClusterOrder,
				
				[Dictionary].[Id]					AS DictionaryId,
				[Dictionary].[Name]					AS DictionaryName

	FROM		[DictionaryIndicatorView]

	INNER JOIN	[DictionaryLevelView]
		ON		[DictionaryLevelView].[Id] = [DictionaryIndicatorView].[DictionaryLevelId]

	INNER JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = [DictionaryLevelView].[DictionaryCompetenceId]

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]

	INNER JOIN	[Dictionary]
		ON		[Dictionary].[Id] = [DictionaryClusterView].[DictionaryId]