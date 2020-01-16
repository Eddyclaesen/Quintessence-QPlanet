CREATE PROCEDURE [dbo].[ListProjectCandidateCategoryDetailType2MailTags]
	@Id			UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @LanguageId INT
	DECLARE @Table TABLE([Tag] NVARCHAR(MAX), [Value] NVARCHAR(MAX))

	/*Increase performance by putting record in temporary table*/
	SELECT	[ProjectCandidateCategoryDetailType2View].[Deadline]				AS [ScheduledDate],
			[ProjectCandidateView].[CandidateLanguageId]						AS [LanguageId],
			[ProjectTypeCategoryTranslationView].[Name]							AS [DetailTypeName]
	INTO #TempTable 
	FROM [dbo].[ProjectCandidateCategoryDetailType2View]
	INNER JOIN [ProjectCandidateView]
		ON [ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCandidateId]
	INNER JOIN [ProjectCandidateCategoryDetailTypeView]
		ON [ProjectCandidateCategoryDetailTypeView].[Id] = [ProjectCandidateCategoryDetailType2View].[Id]
	INNER JOIN projectcategoryDetailType2view
		ON projectcategoryDetailType2view.[Id] = [ProjectCandidateCategoryDetailTypeView].[ProjectCategoryDetailTypeId]
	INNER JOIN projectcategorydetailview
		ON [ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailType2View].[Id]
	INNER JOIN [ProjectTypeCategoryView]
		ON [ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
	INNER JOIN [ProjectTypeCategoryTranslationView]
		ON [ProjectTypeCategoryTranslationView].[ProjectTypeCategoryId] = [ProjectTypeCategoryView].[Id]
		AND [ProjectTypeCategoryTranslationView].[LanguageId] = [ProjectCandidateView].[CandidateLanguageId]
	WHERE [ProjectCandidateCategoryDetailType2View].[Id] = @Id
 
	/*Insert into key-value table*/
	SELECT @LanguageId = [LanguageId] 
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'SCHEDULEDDATE', CONVERT(VARCHAR(10), [ScheduledDate] , 103)
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'DETAILTYPENAME', [DetailTypeName] 
	FROM #TempTable

	DROP TABLE #TempTable

	SELECT *
	FROM @Table
END
GO
