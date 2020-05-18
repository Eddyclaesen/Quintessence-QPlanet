CREATE PROCEDURE [dbo].[ListProjectCandidateCategoryDetailType1MailTags]
	@Id			UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @LanguageId INT
	DECLARE @OfficeId INT
	DECLARE @Table TABLE([Tag] NVARCHAR(MAX), [Value] NVARCHAR(MAX))
	DECLARE @DetailType NVARCHAR(MAX)

	/*Increase performance by putting record in temporary table*/
	SELECT	[ProjectCandidateCategoryDetailType1View].[ScheduledDate]			AS [ScheduledDate],
			[CrmAppointmentView].[EndDate]										AS [ScheduledEndDate],
			CAST(CAST([ProjectCandidateCategoryDetailType1View].[ScheduledDate] AS TIME) AS nvarchar(5)) AS [AppointmentHour],
			[ProjectCandidateCategoryDetailType1View].[OfficeId]				AS [OfficeId],
			[ProjectCandidateView].[CandidateLanguageId]						AS [LanguageId],
			[ProjectTypeCategoryTranslationView].[Name]							AS [DetailTypeName]
	INTO #TempTable 
	FROM [dbo].[ProjectCandidateCategoryDetailType1View]
	INNER JOIN [ProjectCandidateView]
		ON [ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCandidateId]
	INNER JOIN [ProjectCandidateCategoryDetailTypeView]
		ON [ProjectCandidateCategoryDetailTypeView].[Id] = [ProjectCandidateCategoryDetailType1View].[Id]
	INNER JOIN projectcategorydetailtype1view
		ON [ProjectCategoryDetailType1View].[Id] = [ProjectCandidateCategoryDetailTypeView].[ProjectCategoryDetailTypeId]
	INNER JOIN projectcategorydetailview
		ON [ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailType1View].[Id]
	INNER JOIN [ProjectTypeCategoryView]
		ON [ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
	INNER JOIN [ProjectTypeCategoryTranslationView]
		ON [ProjectTypeCategoryTranslationView].[ProjectTypeCategoryId] = [ProjectTypeCategoryView].[Id]
		AND [ProjectTypeCategoryTranslationView].[LanguageId] = [ProjectCandidateView].[CandidateLanguageId]
	INNER JOIN [CrmAppointmentView] on [CrmAppointmentView].[Code] = [ProjectCandidateView].[Code]
	WHERE [ProjectCandidateCategoryDetailType1View].[Id] = @Id
		AND ProjectCandidateCategoryDetailType1View.ScheduledDate = CrmAppointmentView.AppointmentDate
	
	/*Insert into key-value table*/
	SELECT @LanguageId = [LanguageId] 
	FROM #TempTable

	SELECT @DetailType = [DetailTypeName]
	FROM #TempTable

	SELECT @OfficeId = [OfficeId] 
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'SCHEDULEDDATE', CONVERT(VARCHAR(10), [ScheduledDate] , 103)
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'OFFICE', [OfficeTranslationView].[Name]
	FROM [OfficeTranslationView]
	WHERE [OfficeTranslationView].[OfficeId] = @OfficeId
	AND [OfficeTranslationView].[LanguageId] = @LanguageId

	INSERT INTO @Table
	SELECT 'DETAILTYPENAME', LOWER([DetailTypeName])
	FROM #TempTable

	IF @DetailType IN ('Reflectiegesprek','Interview réflexive','Reflective interview','Reflektierenden Interview')
	BEGIN
		IF @LanguageId = 1
			BEGIN
			INSERT INTO @Table
			SELECT 'CUSTOM', 'Ter voorbereiding van dit gesprek vragen wij om het reflectiedocument ten laatste 2 dagen voor het gesprek in te vullen.' 
			FROM #TempTable
			END
		IF @LanguageId = 2
			BEGIN
			INSERT INTO @Table
			SELECT 'CUSTOM', 'En préparation de votre participation, nous vous demandons de remplir un questionnaire (Entretien de réflexion), au plus tard 2 jours avant l''interview réflexive.' 
			FROM #TempTable
			END
		IF @LanguageId = 3
			BEGIN
			INSERT INTO @Table
			SELECT 'CUSTOM', 'In preparation of this interview, we would like to ask you to complete the questionnaire at least 2 days in advance.' 
			FROM #TempTable
			END
		IF @LanguageId = 4
			BEGIN
			INSERT INTO @Table
			SELECT 'CUSTOM', 'Ter voorbereiding van dit gesprek vragen wij om het reflectiedocument ten laatste 2 dagen voor het gesprek in te vullen.' 
			FROM #TempTable
			END
	END
	ELSE
	BEGIN
		INSERT INTO @Table
		SELECT 'CUSTOM', '' 
		FROM #TempTable
	END

	INSERT INTO @Table
	SELECT 'DURATION', DATEDIFF(hh, #TempTable.ScheduledDate, #TempTable.ScheduledEndDate)
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'TIME', [AppointmentHour]
	FROM #TempTable
	
	DROP TABLE #TempTable

	SELECT *
	FROM @Table
END