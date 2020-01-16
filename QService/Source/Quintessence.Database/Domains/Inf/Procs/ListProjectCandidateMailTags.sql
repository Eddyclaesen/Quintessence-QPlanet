CREATE PROCEDURE [dbo].[ListProjectCandidateMailTags]
	@Id			UNIQUEIDENTIFIER
AS
BEGIN

	DECLARE @LanguageId INT
	DECLARE @OfficeId INT
	DECLARE @ProjectType NVARCHAR(MAX)
	DECLARE @ContactName NVARCHAR(MAX)
	DECLARE @Table TABLE([Tag] NVARCHAR(MAX), [Value] NVARCHAR(MAX))
	DECLARE @Gender CHAR

	/*Increase performance by putting record in temporary table*/
	SELECT	[ProjectCandidateView].[CandidateLanguageId]	AS [LanguageId],
			[ProjectCandidateView].[OfficeId]				AS [OfficeId],
			[ProjectCandidateView].[CandidateFirstName]		AS [FirstName],
			[ProjectCandidateView].[CandidateLastName]		AS [LastName],
			[ProjectCandidateView].[CandidateGender]		AS [Gender],	
			[CrmAppointmentView].[AppointmentDate]			AS [AppointmentDate],
			CAST(CAST([CrmAppointmentView].[AppointmentDate] AS TIME) AS nvarchar(5)) AS [AppointmentHour],
			CASE CAST(CAST([CrmAppointmentView].[AppointmentDate] AS TIME) AS nvarchar(5))
				WHEN '07:30' THEN '07:15'
				ELSE CAST(dateadd(minute,-30,CAST([CrmAppointmentView].[AppointmentDate] AS TIME)) AS nvarchar(5)) 
				END AS [OfficeHour],
			[CrmContactView].[Name]							AS [ContactName],
			[ProjectTypeCategoryView].[Name]				AS [ProjectTypeName]

	INTO #TempTable 
	FROM [dbo].[ProjectCandidateView]
	INNER JOIN [CrmAppointmentView]
		ON [CrmAppointmentView].[Id] = [ProjectCandidateView].[CrmCandidateAppointmentId]
	INNER JOIN [ProjectView]
		ON [ProjectView].[Id] = [ProjectCandidateView].[ProjectId]
	INNER JOIN [CrmContactView]
		ON [CrmContactView].[Id] = [ProjectView].[ContactId]
	INNER JOIN [ProjectTypeView]
		ON [ProjectTypeView].[Id] = [ProjectView]. [ProjectTypeId]
	INNER JOIN [ProjectTypeCategoryView]
		ON [ProjectTypeCategoryView].[CrmTaskId] = [CrmAppointmentView].[TaskId]

	WHERE [ProjectCandidateView].[Id] = @Id
 
	/*Insert into key-value table*/
	SELECT @LanguageId = [LanguageId] 
	FROM #TempTable

	SELECT @OfficeId = [OfficeId] 
	FROM #TempTable

	SELECT @ProjectType = [ProjectTypeName]
	FROM #TempTable

	SELECT @ContactName = [ContactName]
	FROM #TempTable

	SELECT @Gender = [Gender]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'GENDER', [dbo].[GenderToSalutation] (Gender, @LanguageId)
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'OFFICE', [OfficeTranslationView].[Name]
	FROM [OfficeTranslationView]
	WHERE [OfficeTranslationView].[OfficeId] = @OfficeId
	AND [OfficeTranslationView].[LanguageId] = @LanguageId

	INSERT INTO @Table
	SELECT 'FIRSTNAME', [FirstName]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'LASTNAME', [LastName]
	FROM #TempTable

	IF @LanguageId = 1
		BEGIN
		SET LANGUAGE Dutch
		INSERT INTO @Table
		SELECT 'DATE', CAST(DAY([AppointmentDate]) AS NVARCHAR(MAX))+' '+CAST(CONVERT(nvarchar(max), DATENAME(mm, [AppointmentDate]),100) AS NVARCHAR(MAX))+' '+CAST(YEAR([AppointmentDate]) AS NVARCHAR(MAX))
		FROM #TempTable
		SET LANGUAGE US_English
		END

	IF @LanguageId = 2
		BEGIN
		SET LANGUAGE French
		INSERT INTO @Table
		SELECT 'DATE', CAST(DAY([AppointmentDate]) AS NVARCHAR(MAX))+' '+CAST(CONVERT(nvarchar(max), DATENAME(mm, [AppointmentDate]),100) AS NVARCHAR(MAX))+' '+CAST(YEAR([AppointmentDate]) AS NVARCHAR(MAX))
		FROM #TempTable
		SET LANGUAGE US_English
		END

	IF @LanguageId = 3
		BEGIN 
		INSERT INTO @Table
		SELECT 'DATE', CAST(DAY([AppointmentDate]) AS NVARCHAR(MAX))+' '+CAST(CONVERT(nvarchar(max), DATENAME(mm, [AppointmentDate]),100) AS NVARCHAR(MAX))+' '+CAST(YEAR([AppointmentDate]) AS NVARCHAR(MAX))
		FROM #TempTable
		END

	INSERT INTO @Table
	SELECT 'TIME', [AppointmentHour]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'CONTACT', [ContactName]
	FROM #TempTable

	IF @ContactName = 'Bpost'
		BEGIN
	INSERT INTO @Table
		SELECT 'PROJECTTYPE', 'assessment center'
		FROM #TempTable
		END
	ELSE BEGIN
		INSERT INTO @Table
	SELECT 'PROJECTTYPE', LOWER([ProjectTypeName])
	FROM #TempTable
		END

	INSERT INTO @Table
	SELECT 'WEGENWERKEN',
		CASE @LanguageId
			WHEN 1 THEN
				CASE @OfficeId
					WHEN 1 THEN 'Aangezien er vanaf 4 augustus tot begin september werken zijn aan de afrit Kontich op de E19, kan u bijkomende informatie doornemen voor alternatieve routes. http://www.edegem.be/nieuwsdetail.aspx?id=5243'
					ELSE ''
				END				
			WHEN 2 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Etant donné que des travaux sont prévus entre le 04 aout et début septembre à la sortie E19 Kontich, vous pouvez consulter les informations suivantes pour trouver un route alternative pour rejoindre nos bureaux. http://www.edegem.be/nieuwsdetail.aspx?id=5243'
					ELSE ''
				END		
			WHEN 3 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Starting 4th August till September there will be construction at the Kontich highway exit. Additional information is available at http://www.edegem.be/nieuwsdetail.aspx?id=5243'
					ELSE ''
				END		
			WHEN 4 THEN 
				CASE @OfficeId
					WHEN 1 THEN ''
					ELSE ''
				END		
			ELSE ''
		END

	INSERT INTO @Table
	SELECT 'OFFICEADDRESS', 
		CASE @LanguageId
			WHEN 1 THEN
				CASE @OfficeId
					WHEN 1 THEN 'Prins Boudewijnlaan 41, 2650 Edegem.'
					WHEN 2 THEN 'Generaal Wahislaan 3, 1030 Brussel.'
					WHEN 3 THEN 'Gaston Crommenlaan 4, 9de verdiep, 9050 Gent.'
					ELSE ''
				END				
			WHEN 2 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Prins Boudewijnlaan 41, 2650 Anvers-Edegem.'
					WHEN 2 THEN 'Boulevard Général Wahis 3, 1030 Bruxelles.'
					WHEN 3 THEN 'Gaston Crommenlaan 4, 9e étage, 9050 Gand.'
					ELSE ''
				END		
			WHEN 3 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Prins Boudewijnlaan 41, 2650 Antwerp-Edegem.'
					WHEN 2 THEN 'Blvd. General Wahis 3, 1030 Brussels.'
					WHEN 3 THEN 'Gaston Crommenlaan 4, 9th floor, 9050 Ghent.'
					ELSE ''
				END		
			WHEN 4 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Prins Boudewijnlaan 41, 2650 Antwerp-Edegem.'
					WHEN 2 THEN 'Blvd. General Wahis 3, 1030 Brussels.'
					WHEN 3 THEN 'Gaston Crommenlaan 4, 9th floor, 9050 Ghent.'
					ELSE ''
				END		
			ELSE 'No address set'
		END

	INSERT INTO @Table
	SELECT 'OFFICEPARKING', 
		CASE @LanguageId
			WHEN 1 THEN
				CASE @OfficeId
					WHEN 1 THEN 'Indien u met de wagen komt, kan u uw wagen parkeren op een Quintessence parkeerplaats.'
					WHEN 2 THEN 'Indien u met de wagen komt, kan u uw wagen parkeren op een Quintessence parkeerplaats.'
					WHEN 3 THEN 'Indien u met de wagen komt, kan u uw wagen parkeren in de ondergrondse parking.'
					ELSE ''
				END				
			WHEN 2 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Vous pouvez garer votre voiture sur les emplacements spécialement réservés pour Quintessence.'
					WHEN 2 THEN 'Vous pouvez garer votre voiture sur les emplacements spécialement réservés pour Quintessence.'
					WHEN 3 THEN 'Si vous venez en voiture, vous pouvez garer votre voiture dans le garage souterrain.'
					ELSE ''
				END		
			WHEN 3 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'If you come by car, you can park your car on a Quintessence parking place.'
					WHEN 2 THEN 'If you come by car, you can park your car on a Quintessence parking place.'
					WHEN 3 THEN 'If you come by car, you can park your car underground.'
					ELSE ''
				END		
			WHEN 4 THEN 
				CASE @OfficeId
					WHEN 1 THEN ''
					WHEN 2 THEN ''
					WHEN 3 THEN ''
					ELSE ''
				END		
			ELSE 'No parking set'
		END

	IF @ProjectType IN ('Assessment center','Executive Assessment','Custom with scoring')
		BEGIN
		INSERT @Table
		SELECT 'ARTICLE','L'''
		END
	ELSE BEGIN
		INSERT @Table
		SELECT 'ARTICLE','Le '
		END

	INSERT INTO @Table
	SELECT 'OFFICEHOURS', [OfficeHour]
		--CASE @OfficeId
		--	WHEN 1 THEN '08:00'
		--	WHEN 2 THEN '08:00'
		--	WHEN 3 THEN '08:30'
		--	ELSE '08:00'
		--END
	FROM #TempTable

	IF @ContactName <> 'Bpost'
	BEGIN
	INSERT INTO @Table
	SELECT 'DURATION', 
		CASE @LanguageId
			WHEN 1 THEN
				CASE @ProjectType
					WHEN 'Focused Assessment' THEN 'een halve dag'
					WHEN 'Focused Development' THEN 'een halve dag'
					WHEN 'Second Opinion' THEN 'een halve dag'
					ELSE 'ongeveer een dag'
				END				
			WHEN 2 THEN 
				CASE @ProjectType
					WHEN 'Focused Assessment' THEN 'une demi-journée'
					WHEN 'Focused Development' THEN 'une demi-journée'
					WHEN 'Second Opinion' THEN 'une demi-journée'
					ELSE 'toute une journée'
				END		
			WHEN 3 THEN 
				CASE @ProjectType
					WHEN 'Focused Assessment' THEN 'half a day'
					WHEN 'Focused Development' THEN 'half a day'
					WHEN 'Second Opinion' THEN 'half a day'
					ELSE 'about a day'
				END		
			WHEN 4 THEN 
				CASE @ProjectType
					WHEN 'Focused Assessment' THEN 'etwa einen halben Tag'
					WHEN 'Focused Development' THEN 'etwa einen halben Tag'
					WHEN 'Second Opinion' THEN 'etwa einen halben Tag'
					ELSE 'etwa einen Tag'
				END		
			ELSE 'No duration set'
		END
	END
	ELSE
		BEGIN
		INSERT INTO @Table
		SELECT 'DURATION', 
			CASE @LanguageId
			WHEN 1 THEN 'een halve dag'
			WHEN 2 THEN 'une demi-journée'
			WHEN 3 THEN 'half a day'
			WHEN 4 THEN 'etwa einen halben Tag'
			END
		END

	INSERT INTO @Table
	SELECT 'CV', 
		CASE @LanguageId
			WHEN 1 THEN
				CASE @ProjectType
					WHEN 'Focused Assessment' THEN ''
					WHEN 'Focused Development' THEN ''
					ELSE 'Gelieve vooraf uw <b>curriculum vitae</b> per mail door te sturen.<br><br>'
				END				
			WHEN 2 THEN 
				CASE @ProjectType
					WHEN 'Focused Assessment' THEN ''
					WHEN 'Focused Development' THEN ''
					ELSE 'Veuillez s''il vous plait envoyer, au préalable, votre <b>curriculum vitae</b> par mail.<br><br>'
				END		
			WHEN 3 THEN 
				CASE @ProjectType
					WHEN 'Focused Assessment' THEN ''
					WHEN 'Focused Development' THEN ''
					ELSE 'Please e-mail your <b>curriculum vitae</b> beforehand.<br><br>'
				END		
			WHEN 4 THEN 
				CASE @ProjectType
					WHEN 'Focused Assessment' THEN ''
					WHEN 'Focused Development' THEN ''
					ELSE '<br>'
				END		
			ELSE ''
		END

	IF @ContactName = 'Bpost'
		BEGIN
		INSERT INTO @Table
		SELECT 'BPOST',
			CASE @LanguageId
			WHEN 1 THEN 'Daarnaast vragen wij u bijgaande vragenlijsten (Schein en Reflectiedocument) in te vullen en ons ten laatste twee dagen voor het assessment center per e-mail terug te bezorgen.'
			WHEN 2 THEN 'Nous vous demandons aussi de remplir les questionnaires ci-attachés (Schein et Document de réflection) et de nous les renvoyer par e-mail au plus tard deux jours avant votre assessment center.'
			ELSE ''
			END
		END
	ELSE BEGIN
		INSERT INTO @Table
		SELECT 'BPOST',''
	END

	IF @Gender = 'M'
		BEGIN
		INSERT INTO @Table
		SELECT 'INFORME', 'informé'
		INSERT INTO @Table
		SELECT 'DETENDU', 'détendu'
		END

	IF @Gender = 'F'
		BEGIN
		INSERT INTO @Table
		SELECT 'INFORME', 'informée'
		INSERT INTO @Table
		SELECT 'DETENDU', 'détendue'
		END

	DROP TABLE #TempTable

	SELECT *
	FROM @Table
END
GO
