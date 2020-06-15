CREATE PROCEDURE [dbo].[ListProjectCandidateCategoryDetailType3MailTags]
	@Id			UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @LanguageId INT
	DECLARE @Table TABLE([Tag] NVARCHAR(MAX), [Value] NVARCHAR(MAX))
	DECLARE @DetailType NVARCHAR(MAX)
	DECLARE @Gender CHAR
	DECLARE @LastName NVARCHAR(MAX)
	DECLARE @FirstName NVARCHAR(MAX)
	DECLARE @LoginId NVARCHAR(MAX)
	DECLARE @CandidateId NVARCHAR(MAX)
	DECLARE @Type3Name NVARCHAR(MAX)
	DECLARE @Company NVARCHAR(MAX)
	DECLARE @InternalCandidate BIT

	SET @LoginId = (select replace(SUBSTRING(CONVERT(varchar(255), NEWID()),8, 8),'-','') as RANDOM)
	
	/*Increase performance by putting record in temporary table*/
	SELECT	[ProjectCandidateCategoryDetailType3View].[Deadline]				AS [ScheduledDate],
			[ProjectCandidateView].[CandidateLanguageId]						AS [LanguageId],
			[ProjectCandidateCategoryDetailType3View].[LoginCode]				AS [LoginCode],
			[ProjectTypeCategoryTranslationView].[Name]							AS [DetailTypeName],
			[ProjectCandidateView].[CandidateGender]							AS [Gender],
			[ProjectCandidateView].CandidateLastName							AS [LastName],
			[ProjectCandidateView].CandidateFirstName							AS [FirstName],
			[ProjectCandidateView].Id											AS [CandidateId],
			[ProjectCandidateCategoryDetailType3View].ProjectCategoryDetailTypeName AS [Type3Name],
			CrmContactView.[Name]												AS [Company],
			ProjectCandidateView.InternalCandidate								AS [InternalCandidate]

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
	INNER JOIN	ProjectView on [ProjectCandidateView].ProjectId = ProjectView.Id
	INNER JOIN	CrmContactView on ProjectView.ContactId = CrmContactView.Id
	WHERE [ProjectCandidateCategoryDetailType3View].[Id] = @Id
 
	/*Insert into key-value table*/
	SELECT @CandidateId = [CandidateId]
	FROM #TempTable

	SELECT @Type3Name = [Type3Name]
	FROM #TempTable

	SELECT @LanguageId = [LanguageId] 
	FROM #TempTable

	SELECT @DetailType = [DetailTypeName]
	FROM #TempTable

	SELECT @Gender = [Gender]
	FROM #TempTable

	SELECT @LastName = [LastName]
	FROM #TempTable
	
	SELECT @FirstName = [FirstName]
	FROM #TempTable

	SELECT @Company = [Company]
	from #TempTable

	SELECT @InternalCandidate = [InternalCandidate]
	from #TempTable

	INSERT INTO @Table
	SELECT 'LCM', CASE WHEN @Company = 'LCM - ANMC' AND [InternalCandidate] = 1 THEN 
						CASE [LanguageId]
							WHEN 1 THEN '<br/>Omwille van veiligheid- en netwerkinstellingen dient u als LCM-ANMC medewerker deze vragenlijst in te vullen op een eigen toestel vanuit een eigen netwerk.'
							WHEN 2 THEN '<br/>En tant que employé de LCM-ANMC nous vous prions de remplir ce questionnaire sur votre propre ordinateur depuis votre propre réseau. Ceci est dû aux paramètres de sécurité informatique.'
							ELSE '<br/>' 
						END
					ELSE CASE WHEN @Company like '%Fod Fin%' THEN 
						CASE [LanguageId]
							WHEN 1 THEN '<br/>Alle instructies zal u op uw scherm kunnen lezen.'
							WHEN 2 THEN ''
							ELSE '<br/>' 
						END
					ELSE '' END
					END
	FROM #TempTable					

	INSERT INTO @Table
	SELECT 'SCHEDULEDDATE', CASE WHEN [ScheduledDate] is not null THEN CASE [LanguageId]
										WHEN 1 THEN 'Gelieve deze vragenlijst in te vullen <strong>voor '+CONVERT(VARCHAR(10), [ScheduledDate] , 103)+'.</strong>'
										WHEN 2 THEN 'Veuillez remplir ce questionnaire <strong>avant le '+CONVERT(VARCHAR(10), [ScheduledDate] , 103)+'.</strong>'
										WHEN 3 THEN 'Please complete this questionnaire <strong>before '+CONVERT(VARCHAR(10), [ScheduledDate] , 103)+'.</strong>'
										ELSE 'Gelieve deze vragenlijst in te vullen <strong>voor '+CONVERT(VARCHAR(10), [ScheduledDate] , 103)+'.</strong>'
										END
						ELSE '' END
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'DETAILTYPENAME', [DetailTypeName] 
	FROM #TempTable

	IF @DetailType IN ('NEO-pir','Leiderschapsstijl','Styles de leadership','Leadership styles','Persoonlijkheidsvragenlijst','Questionnaire de personnalité','Personality Questionnaire','Questionnaire styles de leadership','Persönlichkeitstest')
		BEGIN
		INSERT INTO @Table
		SELECT 'LINK', '<a href="http://www.iedereencompetent.be/qata/login.aspx">http://www.iedereencompetent.be/qata/login.aspx</a>'
		FROM #TempTable
		INSERT INTO @Table
		SELECT 'LOGINCODE', [LoginCode]
		FROM #TempTable
		END

	IF @DetailType IN ('Reflectiedocument','Réflexion personnelle','Reflection interview')
		BEGIN
			IF @LanguageId = 1
				BEGIN
					INSERT INTO @Table
					SELECT 'LINK', '<a href="https://quintessence-consultancy.typeform.com/to/vWQCnT">LINK</a>'
					FROM #TempTable
				END
			IF @LanguageId = 2
				BEGIN
					INSERT INTO @Table
					SELECT 'LINK', '<a href="https://quintessence-consultancy.typeform.com/to/WTUVFf">LINK</a>'
					FROM #TempTable
				END
			IF @LanguageId IN (3,4)
				BEGIN
					INSERT INTO @Table
					SELECT 'LINK', '<a href="https://quintessence-consultancy.typeform.com/to/uRDV2x">LINK</a>'
					FROM #TempTable
				END

		--IF NOT EXISTS (SELECT UserId 
		--		FROM survey.dbo.[User]
		--		WHERE ProjectCandidateId = @CandidateId)
		--			BEGIN
		--				INSERT INTO Survey.dbo.[User] VALUES (@FirstName, @LastName, 'secretariaat@quintessence.be', @LoginId, CASE @Gender WHEN 'M' THEN 1 WHEN 'F' THEN 2 END, @LanguageId, 1, @CandidateId)
		--				UPDATE [ProjectCandidateCategoryDetailType3View] 
		--				SET LoginCode = @LoginId
		--				WHERE ProjectCandidateId = @CandidateId AND ProjectCategoryDetailTypeName = 'Reflectiedocument'
		--			END
		--		ELSE
		--				SET @LoginId = (SELECT LoginCode FROM Survey.dbo.[User] WHERE ProjectCandidateId = @CandidateId) 	
		
		INSERT INTO @Table
		SELECT 'LOGINCODE', ''
		FROM #TempTable

		INSERT INTO @Table
		SELECT 'LOGINTEXT', ''
		FROM #TempTable
		END
	ELSE
		BEGIN
			IF @LanguageId = 1
				BEGIN
					INSERT INTO @Table
					SELECT 'LOGINTEXT','Uw persoonlijke login is: '
					FROM #TempTable
				END
			IF @LanguageId = 2
				BEGIN
					INSERT INTO @Table
					SELECT 'LOGINTEXT','Votre code d''accès est le suivant : '
					FROM #TempTable
				END
			IF @LanguageId = 3
				BEGIN
					INSERT INTO @Table
					SELECT 'LOGINTEXT','Your personal login: '
					FROM #TempTable
				END
			IF @LanguageId = 4
				BEGIN
					INSERT INTO @Table
					SELECT 'LOGINTEXT','Ihr persönliches Login lautet: '
					FROM #TempTable
				END
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