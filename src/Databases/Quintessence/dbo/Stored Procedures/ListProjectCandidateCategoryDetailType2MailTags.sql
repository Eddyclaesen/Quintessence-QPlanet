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

	INSERT INTO @Table
	SELECT 'LOGINLINK', 
	 CASE  
		WHEN [DetailTypeName] = 'Tapas' THEN 
			CASE @LanguageId
				WHEN 1 THEN '<a href="https://platform.tapascity.com/2min-qe"><strong>https://platform.tapascity.com/2min-qe</strong></a><br/>
				Denkt u eraan deze survey zeker 24u voor het Center in te vullen? We integreren namelijk zowel uw persoonlijke als professionele drijfveren en talenten graag op maat in het programma.'
				WHEN 2 THEN '<a href="https://platform.tapascity.com/2min-qe"><strong>https://platform.tapascity.com/2min-qe</strong></a><br/>
				Merci de vous assurer que les tests Tapas soient complétés au moins 24 heures avant votre journée chez Quintessence. Cela afin que nous puissions intégrer vos motivations, talents personnels et professionnels au programme.'
				ELSE '<a href="https://platform.tapascity.com/2min-qe"><strong>https://platform.tapascity.com/2min-qe</strong></a><br/>
				Please make sure the survey is completed at least 24hrs before your day at Quintessence, since we integrate your personal as well as professional drivers and talents into the program. '
			END
				
		WHEN [DetailTypeName] = 'Motivation Questionnaire' THEN 
			CASE @LanguageId
				WHEN 1 THEN '<a href="https://talentcentral.eu.shl.com/player/link/c21605e7d3e34aa7842d5d6ef0c62259"><strong>https://talentcentral.eu.shl.com/player/link/c21605e7d3e34aa7842d5d6ef0c62259</strong></a><br/>
				Denkt u eraan deze survey zeker 24u voor het Center in te vullen? We integreren namelijk zowel uw persoonlijke als professionele drijfveren en talenten graag op maat in het programma.'
				WHEN 2 THEN '<a href="https://talentcentral.eu.shl.com/player/link/80feac2ba0ad4aad8f6d9f5b4c769da0"><strong>https://talentcentral.eu.shl.com/player/link/80feac2ba0ad4aad8f6d9f5b4c769da0</strong></a><br/>
				Merci de vous assurer que les tests MQ soient complétés au moins 24 heures avant votre journée chez Quintessence. Cela afin que nous puissions intégrer vos motivations, talents personnels et professionnels au programme.'
				ELSE '<a href="https://talentcentral.eu.shl.com/player/link/c21605e7d3e34aa7842d5d6ef0c62259"><strong>https://talentcentral.eu.shl.com/player/link/c21605e7d3e34aa7842d5d6ef0c62259</strong></a><br/>
				Please make sure the survey is completed at least 24hrs before your day at Quintessence, since we integrate your personal as well as professional drivers and talents into the program.'
			END
		ELSE 'PASTE LINK HERE'
		END
	FROM #TempTable

	DROP TABLE #TempTable

	SELECT *
	FROM @Table
END