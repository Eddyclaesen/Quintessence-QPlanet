CREATE PROCEDURE [dbo].[ListProjectCandidateCategoryDetailType3MailTags]
	@Id			UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @LanguageId INT
	DECLARE @Table TABLE([Tag] NVARCHAR(MAX), [Value] NVARCHAR(MAX))
	DECLARE @DetailType NVARCHAR(MAX)
	DECLARE @Gender CHAR
	
	/*Increase performance by putting record in temporary table*/
	SELECT	[ProjectCandidateCategoryDetailType3View].[Deadline]				AS [ScheduledDate],
			[ProjectCandidateView].[CandidateLanguageId]						AS [LanguageId],
			[ProjectCandidateCategoryDetailType3View].[LoginCode]				AS [LoginCode],
			[ProjectTypeCategoryTranslationView].[Name]							AS [DetailTypeName],
			[ProjectCandidateView].[CandidateGender]							AS [Gender]
	INTO #TempTable 
	FROM [dbo].[ProjectCandidateCategoryDetailType3View]
	INNER JOIN [ProjectCandidateView]
		ON [ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCandidateId]
	INNER JOIN [ProjectCandidateCategoryDetailTypeView]
		ON [ProjectCandidateCategoryDetailTypeView].[Id] = [ProjectCandidateCategoryDetailType3View].[Id]
	INNER JOIN projectcategorydetailtype3view
		ON projectcategorydetailtype3view.[Id] = [ProjectCandidateCategoryDetailTypeView].[ProjectCategoryDetailTypeId]
	INNER JOIN projectcategorydetailview
		ON [ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailType3View].[Id]
	INNER JOIN [ProjectTypeCategoryView]
		ON [ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
	INNER JOIN [ProjectTypeCategoryTranslationView]
		ON [ProjectTypeCategoryTranslationView].[ProjectTypeCategoryId] = [ProjectTypeCategoryView].[Id]
		AND [ProjectTypeCategoryTranslationView].[LanguageId] = [ProjectCandidateView].[CandidateLanguageId]
	WHERE [ProjectCandidateCategoryDetailType3View].[Id] = @Id
 
	/*Insert into key-value table*/
	SELECT @LanguageId = [LanguageId] 
	FROM #TempTable

	SELECT @DetailType = [DetailTypeName]
	FROM #TempTable

	SELECT @Gender = [Gender]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'SCHEDULEDDATE', CONVERT(VARCHAR(10), [ScheduledDate] , 103)
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'LOGINCODE', [LoginCode]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'DETAILTYPENAME', [DetailTypeName] 
	FROM #TempTable

	IF @DetailType IN ('NEO-pir','Leiderschapsstijl','Styles de leadership','Leadership styles','Persoonlijkheidsvragenlijst','Questionnaire de personnalité','Personality Questionnaire','Questionnaire styles de leadership')
		BEGIN
		INSERT INTO @Table
		SELECT 'LINK', '<a href="http://www.iedereencompetent.be/qata/login.aspx">http://www.iedereencompetent.be/qata/login.aspx</a>'
		FROM #TempTable
		END

	IF @DetailType IN ('Reflectiedocument','Entretien de réflection','Reflection interview')
		BEGIN
		INSERT INTO @Table
		SELECT 'LINK', '<a href="http://www.hr-quintessentials.be">http://www.hr-quintessentials.be</a>'
		FROM #TempTable
		END

	INSERT INTO @Table
	SELECT 'DIRIGE',
		CASE @Gender
			WHEN 'F' THEN 'dirigée'
			WHEN 'M' THEN 'dirigé'
		END

	DROP TABLE #TempTable

	SELECT *
	FROM @Table
END
GO
