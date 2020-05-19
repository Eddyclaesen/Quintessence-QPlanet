CREATE PROCEDURE [dbo].[Reporting_ListProjectCompetences]
	@ProjectId UNIQUEIDENTIFIER,
	@LanguageId INT
AS
	DECLARE @CompetenceTable AS TABLE(Id UNIQUEIDENTIFIER)
	
	INSERT INTO	@CompetenceTable
		SELECT		DISTINCT
					[ProjectCategoryDetailDictionaryIndicatorView].[DictionaryCompetenceId]

		FROM		[ProjectCategoryDetailView]

		INNER JOIN	[ProjectCategoryDetailDictionaryIndicatorView]
			ON		[ProjectCategoryDetailDictionaryIndicatorView].[ProjectCategoryDetailId] = [ProjectCategoryDetailView].[Id]	

		WHERE		[ProjectCategoryDetailView].[ProjectId] = @ProjectId
	
	SELECT		CompetenceTable.[Id]									AS	CompetenceId,
				[DictionaryCompetenceTranslationView].[Text]			AS	CompetenceName,
				[DictionaryCompetenceTranslationView].[Description]		AS	CompetenceDescription,
				[DictionaryClusterView].[Color]							AS	ClusterColor,
				[DictionaryClusterView].[Order]							AS	ClusterOrder
				
	FROM		@CompetenceTable CompetenceTable

	INNER JOIN	[DictionaryCompetenceView]
		ON		[DictionaryCompetenceView].[Id] = CompetenceTable.[Id]

	INNER JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = CompetenceTable.[Id]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = @LanguageId

	INNER JOIN	[DictionaryClusterView]
		ON		[DictionaryClusterView].[Id] = [DictionaryCompetenceView].[DictionaryClusterId]

	ORDER BY	[DictionaryCompetenceView].[Order]