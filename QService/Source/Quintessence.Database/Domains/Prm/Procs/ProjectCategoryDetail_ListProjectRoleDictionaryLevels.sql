CREATE PROCEDURE [dbo].[ProjectCategoryDetail_ListProjectRoleDictionaryLevels]
	@ProjectRoleId				UNIQUEIDENTIFIER,
	@LanguageId					INT	= NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		[ProjectRoleDictionaryLevelView].[ProjectRoleId], 
				[ProjectRoleDictionaryLevelView].[ProjectRoleName], 
				[ProjectRoleDictionaryLevelView].[ContactId], 
				[ProjectRoleDictionaryLevelView].[DictionaryIndicatorId],
				COALESCE(NULLIF(CAST([DictionaryIndicatorTranslationView].[Text] AS NVARCHAR(MAX)), ''), [ProjectRoleDictionaryLevelView].[DictionaryIndicatorName])			AS	[DictionaryIndicatorName],
				[ProjectRoleDictionaryLevelView].[DictionaryIndicatorOrder],
				[ProjectRoleDictionaryLevelView].[DictionaryLevelId], 
				COALESCE(NULLIF(CAST([DictionaryLevelTranslationView].[Text] AS NVARCHAR(MAX)), ''), [ProjectRoleDictionaryLevelView].[DictionaryLevelName])					AS	[DictionaryLevelName],
				[ProjectRoleDictionaryLevelView].[DictionaryLevelLevel],
				[ProjectRoleDictionaryLevelView].[DictionaryCompetenceId], 
				COALESCE(NULLIF(CAST([DictionaryCompetenceTranslationView].[Text] AS NVARCHAR(MAX)), ''), [ProjectRoleDictionaryLevelView].[DictionaryCompetenceName])		AS	[DictionaryCompetenceName],
				[ProjectRoleDictionaryLevelView].[DictionaryCompetenceOrder],
				[ProjectRoleDictionaryLevelView].[DictionaryClusterId], 
				COALESCE(NULLIF(CAST([DictionaryClusterTranslationView].[Text] AS NVARCHAR(MAX)), ''), [ProjectRoleDictionaryLevelView].[DictionaryClusterName])				AS	[DictionaryClusterName], 
				[ProjectRoleDictionaryLevelView].[DictionaryClusterOrder],
				[ProjectRoleDictionaryLevelView].[DictionaryId], 
				[ProjectRoleDictionaryLevelView].[DictionaryName], 
				[ProjectRoleDictionaryLevelView].[IsStandard], 
				[ProjectRoleDictionaryLevelView].[IsDistinctive]
	
	FROM		[ProjectRoleDictionaryLevelView]

	LEFT JOIN	[DictionaryIndicatorTranslationView]
		ON		[DictionaryIndicatorTranslationView].[DictionaryIndicatorId] = [ProjectRoleDictionaryLevelView].[DictionaryIndicatorId]
		AND		[DictionaryIndicatorTranslationView].[LanguageId] = @LanguageId

	LEFT JOIN	[DictionaryLevelTranslationView]
		ON		[DictionaryLevelTranslationView].[DictionaryLevelId] = [ProjectRoleDictionaryLevelView].[DictionaryLevelId]
		AND		[DictionaryLevelTranslationView].[LanguageId] = @LanguageId

	LEFT JOIN	[DictionaryCompetenceTranslationView]
		ON		[DictionaryCompetenceTranslationView].[DictionaryCompetenceId] = [ProjectRoleDictionaryLevelView].[DictionaryCompetenceId]
		AND		[DictionaryCompetenceTranslationView].[LanguageId] = @LanguageId

	LEFT JOIN	[DictionaryClusterTranslationView]
		ON		[DictionaryClusterTranslationView].[DictionaryClusterId] = [ProjectRoleDictionaryLevelView].[DictionaryClusterId]
		AND		[DictionaryClusterTranslationView].[LanguageId] = @LanguageId

	WHERE		[ProjectRoleDictionaryLevelView].[ProjectRoleId] = 	@ProjectRoleId
END
GO
