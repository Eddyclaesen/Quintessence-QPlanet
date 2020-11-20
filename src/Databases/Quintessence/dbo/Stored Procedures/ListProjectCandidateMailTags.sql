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
	DECLARE @ProjectId NVARCHAR(MAX)
	DECLARE @CrmProjectId INT
	DECLARE @Context NVARCHAR(MAX)
	DECLARE @IsOnline BIT
	DECLARE @ProjectName NVARCHAR(MAX)
	DECLARE @FunctionTitle NVARCHAR(MAX)
	DECLARE @CandidateId NVARCHAR(MAX)
	DECLARE @AppointmentDate DATETIME
	DECLARE @CandidateReport nvarchar(max)

	/*Increase performance by putting record in temporary table*/
	SELECT	[ProjectCandidateView].[CandidateLanguageId]	AS [LanguageId],
			[ProjectCandidateView].[OfficeId]				AS [OfficeId],
			[ProjectCandidateView].[CandidateFirstName]		AS [FirstName],
			[ProjectCandidateView].[CandidateLastName]		AS [LastName],
			[ProjectCandidateView].[CandidateGender]		AS [Gender],
			[ProjectCandidateView].[OnlineAssessment]		AS [OnlineAssessment],
			[CrmAppointmentView].[AppointmentDate]			AS [AppointmentDate],
			CAST(CAST([CrmAppointmentView].[AppointmentDate] AS TIME) AS nvarchar(5)) AS [AppointmentHour],
			CASE CAST(CAST([CrmAppointmentView].[AppointmentDate] AS TIME) AS nvarchar(5))
				WHEN '07:30' THEN '07:15'
				ELSE CAST(dateadd(minute,-30,CAST([CrmAppointmentView].[AppointmentDate] AS TIME)) AS nvarchar(5)) 
				END AS [OfficeHour],
			[CrmContactView].[Name]							AS [ContactName],
			[ProjectTypeCategoryView].[Name]				AS [ProjectTypeName],
			[ProjectCandidateView].[ProjectId]				AS [ProjectId],
			CrmProjectView.Id								AS CrmProjectId,
			CONCAT(AC.Name,DC.Name,CA.Name,EA.Name,FA.Name,FD.Name,PS.Name,SO.Name) AS [Context],
			[ProjectView].Name								AS [ProjectName],
			ACDC.FunctionTitle								AS [FunctionTitle],
			[ProjectCandidateView].CandidateId				AS [CandidateId],
			ACDC.CandidateReportDefinitionId				AS [CandidateReport]
	
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
	LEFT JOIN Project2CrmProjectView
		ON ProjectView.Id = Project2CrmProjectView.ProjectId 
	LEFT JOIN CrmProjectView
		ON Project2CrmProjectView.CrmProjectId = CrmProjectView.Id And CrmProjectView.ProjectStatusId = 3
	left join ProjectCategoryDetail on projectcandidateview.projectid = ProjectCategoryDetail.projectid
	inner join ProjectTypeCategoryView on ProjectCategoryDetail.ProjectTypeCategoryId = ProjectTypeCategoryView.Id and ProjectTypeCategoryView.IsMain = 1
	left join ProjectCategoryAcDetail on ProjectCategoryDetail.Id = ProjectCategoryAcDetail.Id AND ProjectTypeCategoryView.Code = 'AC'
	left join ProjectCategoryDcDetail on ProjectCategoryDetail.Id = ProjectCategoryDcDetail.Id AND ProjectTypeCategoryView.Code = 'DC'
	left join ProjectCategoryCaDetail on ProjectCategoryDetail.Id = ProjectCategoryCaDetail.Id AND ProjectTypeCategoryView.Code = 'CA'
	left join ProjectCategoryEaDetail on ProjectCategoryDetail.Id = ProjectCategoryEaDetail.Id AND ProjectTypeCategoryView.Code = 'EA'
	left join ProjectCategoryFaDetail on ProjectCategoryDetail.Id = ProjectCategoryFaDetail.Id AND ProjectTypeCategoryView.Code = 'FA'
	left join ProjectCategoryFdDetail on ProjectCategoryDetail.Id = ProjectCategoryFdDetail.Id AND ProjectTypeCategoryView.Code = 'FD'
	left join ProjectCategoryPsDetail on ProjectCategoryDetail.Id = ProjectCategoryPsDetail.Id AND ProjectTypeCategoryView.Code = 'PS'
	left join ProjectCategorySoDetail on ProjectCategoryDetail.Id = ProjectCategorySoDetail.Id AND ProjectTypeCategoryView.Code = 'SO'
	left join SimulationContextView AC on ProjectCategoryACDetail.SimulationContextId = AC.Id
	left join SimulationContextView DC on ProjectCategoryDCDetail.SimulationContextId = DC.Id
	left join SimulationContextView CA on ProjectCategoryCADetail.SimulationContextId = CA.Id
	left join SimulationContextView EA on ProjectCategoryEADetail.SimulationContextId = EA.Id
	left join SimulationContextView FA on ProjectCategoryFADetail.SimulationContextId = FA.Id
	left join SimulationContextView FD on ProjectCategoryFDDetail.SimulationContextId = FD.Id
	left join SimulationContextView PS on ProjectCategoryPSDetail.SimulationContextId = PS.Id
	left join SimulationContextView SO on ProjectCategorySODetail.SimulationContextId = SO.Id
	left join AssessmentDevelopmentProjectView ACDC on ProjectView.Id = ACDC.Id

	WHERE [ProjectCandidateView].[Id] = @Id
	and ProjectCategoryDetail.Audit_IsDeleted = 0
 
	/*Insert into key-value table*/
	SELECT @CandidateId = [CandidateId]
	FROM #TempTable

	SELECT @IsOnline = [OnlineAssessment]
	FROM #TempTable

	SELECT @Context = Context
	FROM #TempTable

	SELECT @CrmProjectId = CrmProjectId
	FROM #TempTable

	SELECT @ProjectId = [ProjectId] 
	FROM #TempTable

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

	SELECT @ProjectName = [ProjectName]
	FROM #TempTable

	SELECT @AppointmentDate = [AppointmentDate]
	FROM #TempTable

	SELECT @FunctionTitle = CASE 
					WHEN @ContactName = 'Fod Financiën' THEN CASE @LanguageId
						WHEN 1 THEN [FunctionTitle]
						WHEN 2 THEN REPLACE(REPLACE([FunctionTitle],'leidinggevend','dirigeant'),'projectleider','chef de projet')
						WHEN 4 THEN [FunctionTitle]
					ELSE [FunctionTitle]
					END
				ELSE [FunctionTitle]
				END
	FROM #TempTable

	SELECT @CandidateReport = [CandidateReport]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'GENDER', [dbo].[GenderToSalutation] (Gender, @LanguageId)
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'ONLINE', CASE @IsOnline WHEN 0 THEN '' WHEN 1 THEN '' END
	FROM #TempTable

	IF (@ContactName = 'Fod Financiën')
		BEGIN
			INSERT INTO @Table
			SELECT 'FODFINPRELOC',
				CASE @LanguageId
				WHEN 1 THEN CASE WHEN @ProjectType like '%Custom%' THEN 'Er is geen wijziging van datum mogelijk. Indien u niet aanwezig kan zijn voor deze proef, gelieve ons hiervan zo snel mogelijk te verwittigen via mail. '
					ELSE 'Er is geen wijziging van datum mogelijk. Indien u niet aanwezig kan zijn voor deze proef, gelieve ons hiervan zo snel mogelijk te verwittigen. ' END
				WHEN 2 THEN CASE WHEN @ProjectType like '%Custom%' THEN 'Aucun changement de date n''est possible. Si vous ne pouvez-vous présenter à cette épreuve, veuillez-nous en avertir au plus vite via mail. '
					ELSE 'Aucun changement de date n''est possible. Si vous ne pouvez-vous présenter à cette épreuve, veuillez-nous en avertir au plus vite. ' END
				WHEN 4 THEN CASE WHEN @ProjectType like '%Custom%' THEN 'Eine Terminänderung ist nicht möglich. Falls Sie zu diesem Gespräch nicht anwesend sein können, setzen Sie uns bitte schnellstmöglich darüber in Kenntnis. '
					ELSE 'Eine Terminänderung ist nicht möglich. Falls Sie zu diesem Test nicht anwesend sein können, unterrichten Sie uns bitte schnellstmöglich darüber. ' END
				ELSE ''
				END
			FROM #TempTable
		END
		ELSE BEGIN
		INSERT INTO @Table
		SELECT 'FODFINPRELOC',''
	END

	INSERT INTO @Table
	SELECT 'ONLINELOC', 
			CASE WHEN @ContactName <> 'Fod Financiën' THEN
				CASE @IsOnline 
					WHEN 0 THEN 
						CASE @LanguageId
							WHEN 1 THEN	'U bent welkom in ons kantoor:&nbsp;</span></p><p style="padding-left: 30px;"><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--OFFICE--&gt;<br />&lt;!--OFFICEADDRESS--&gt;<br />T: 32 3 281 44 88</span></strong>'
								+CASE 
									WHEN @OfficeId = 1 THEN '<br /><span style="color: #002649; font-family: calibri; font-size: 11pt;">Hoe bereikt u onze kantoren in <a href="https://c.assets.sh/4QABo46c_YGYheoh-w/original">Antwerpen</a>?</span></p>'
									WHEN @OfficeId = 2 THEN '<br /><span style="color: #002649; font-family: calibri; font-size: 11pt;">Hoe bereikt u onze kantoren in <a href="https://c.assets.sh/_QABo46c_YGYmeoh-w/original">Brussel</a>?</span></p>'
									WHEN @OfficeId = 3 THEN '<br /><span style="color: #002649; font-family: calibri; font-size: 11pt;">Hoe bereikt u onze kantoren in <a href="https://c.assets.sh/6wABo46c_YGYie4j-w/original">Gent?</a></span></p>'
									ELSE '</p>'
								END
							WHEN 2 THEN 'Vous serez re&ccedil;u dans nos bureaux :&nbsp;</span></p><p style="padding-left: 30px;"><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--OFFICE--&gt;<br />&lt;!--OFFICEADDRESS--&gt;<br />T: 32 3 281 44 88</span></strong>'
								+CASE 
									WHEN @OfficeId = 1 THEN '<br /><span style="color: #002649; font-family: calibri; font-size: 11pt;">Comment atteindre nos bureaux à <a href="https://c.assets.sh/0wABo46c_YGYse4j-w/original">Anvers</a> ?</span></p>'
									WHEN @OfficeId = 2 THEN '<br /><span style="color: #002649; font-family: calibri; font-size: 11pt;">Comment atteindre nos bureaux à <a href="https://c.assets.sh/_wABo46c_YGYm-oh-w/original">Bruxelles</a> ?</span></p>'
									WHEN @OfficeId = 3 THEN '<br /><span style="color: #002649; font-family: calibri; font-size: 11pt;">Comment atteindre nos bureaux à <a href="https://c.assets.sh/1QABo46c_YGYt-4j-w/original">Gand</a> ?</span></p>'
									ELSE '</p>'
								END
							WHEN 3 THEN 'We look forward to seeing you at our offices:&nbsp;</span></p><p style="padding-left: 30px;"><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--OFFICE--&gt;<br />&lt;!--OFFICEADDRESS--&gt;<br />T: 32 3 281 44 88</span></strong>'
								+CASE 
									WHEN @OfficeId = 1 THEN '<br /><span style="color: #002649; font-family: calibri; font-size: 11pt;">How to reach our office in <a href="https://c.assets.sh/0QABo46c_YGYs-4j-w/original">Antwerp</a>?</span></p>'
									WHEN @OfficeId = 2 THEN '<br /><span style="color: #002649; font-family: calibri; font-size: 11pt;">How to reach our office in <a href="https://c.assets.sh/6QABo46c_YGYi-4j-w/original">Brussels</a>?</span></p>'
									WHEN @OfficeId = 3 THEN '<br /><span style="color: #002649; font-family: calibri; font-size: 11pt;">How to reach our office in <a href="https://c.assets.sh/4QABo46c_YGYg-4j-w/original">Ghent</a>?</span></p>'
									ELSE '</p>'
								END
							WHEN 4 THEN 'Wir begrüßen Sie in unserem Büro:&nbsp;</span></p><p style="padding-left: 30px;"><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--OFFICE--&gt;<br />&lt;!--OFFICEADDRESS--&gt;<br />T: 32 3 281 44 88</span></strong></p>'				
						END 		 
					WHEN 1 THEN '' 
					END
				ELSE 
				CASE @IsOnline
					WHEN 0 THEN 
						CASE @LanguageId
							WHEN 1 THEN 
								CASE WHEN @AppointmentDate >= '2018-09-24' THEN '<p>U bent welkom in de gebouwen van FOD Financiën:</p><p>North Galaxy - Toren B - 17de verdieping - Koning Albert II-laan 33, 1030 BRUSSEL</p> '
								ELSE 'U bent welkom in ons kantoor:&nbsp;</span></p><p style="padding-left: 30px;"><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--OFFICE--&gt;<br />&lt;!--OFFICEADDRESS--&gt;<br />T: 32 3 281 44 88</span></strong></p>' END
							WHEN 2 THEN 
								CASE WHEN @AppointmentDate >= '2018-09-24' THEN '<p>Vous serez reçu dans les bureaux du SPF Finance :</p><p>North Galaxy – Tour B - 17ème étage - Boulevard du Roi Albert II 33, 1030 BRUXELLES</p> '
								ELSE 'Vous serez re&ccedil;u dans nos bureaux :&nbsp;</span></p><p style="padding-left: 30px;"><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--OFFICE--&gt;<br />&lt;!--OFFICEADDRESS--&gt;<br />T: 32 3 281 44 88</span></strong></p>' END
							WHEN 4 THEN 
								CASE WHEN @AppointmentDate >= '2018-09-24' THEN '<p>Sie sind willkommen in den Gebäuden von FOD Finanzen :</p><p>Boulevard du Roi Albert II 33, 1030 Brüssel<br />In der Abteilung Promotion (Shared Service Center) der P&O-Personalabteilung auf der Etage B17.</p> '
								ELSE 'Wir begrüßen Sie in unserem Büro: </span></p><p style="padding-left: 30px;"><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--OFFICE--&gt;<br />&lt;!--OFFICEADDRESS--&gt;<br />T: 32 3 281 44 88</span></strong></p>' END					
							ELSE '' 
						END
					ELSE ''
				END
		END
	FROM #TempTable 

	INSERT INTO @Table
	SELECT 'ONLINEDESC', 
	CASE WHEN @ContactName <> 'Fod Financiën' THEN
		CASE @IsOnline 
			WHEN 0 THEN '' 
			WHEN 1 THEN 
				CASE @LanguageId
					WHEN 1 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Voor het online assessment maken we gebruik van <strong>Zoom</strong>. Hieronder vindt u de link naar de <strong>online vergaderruimte:</strong></span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><strong>Join Zoom Meeting</strong><br/>Meeting ID: <br/>Wachtwoord: </span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><strong>U kan zoom op voorhand al even testen via de volgende link: <a href="https://zoom.us/test">https://zoom.us/test</a></strong></span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Indien u hierbij problemen ondervindt, welke u zelf niet kan oplossen, dan kan u ons altijd contacteren. Op deze manier zorgen we er samen voor dat het online assessment voor iedereen op een vlotte manier kan starten.</span></p>'
					WHEN 2 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Pour votre assessment digital, nous utiliserons <strong>Zoom</strong>. Vous trouverez ci-dessous le lien vers <strong>la réunion en ligne :</strong></span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><strong>Join Zoom Meeting</strong><br/>Meeting ID: <br/>Mot de passe: </span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><strong>Vous pouvez tester le zoom à l''avance via le lien suivant : <a href="https://zoom.us/test">https://zoom.us/test</a></strong></span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Si vous rencontrez des problèmes, que vous ne pouvez pas résoudre vous-même, n’hésitez pas à nous contacter. De cette façon l’assessment se lancera en douceur pour tout le monde.</span></p>'
					WHEN 3 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">For the online assessment we make use of <strong>Zoom</strong>. Below you can find the link to the <strong>online meeting room:</strong></span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><strong>Join Zoom Meeting</strong><br/>Meeting ID: <br/>Password: </span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><strong>You can test Zoom in advance via the following link: <a href="https://zoom.us/test">https://zoom.us/test</a></strong></span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">If you encounter problems you are not able to solve do not hesitate to contact us. This way, we make sure that the online assessment can start in a smooth manner.</span></p>'
					WHEN 4 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">For the online assessment we make use of <strong>Zoom</strong>. Below you can find the link to the <strong>online meeting room:</strong></span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><strong>Join Zoom Meeting</strong><br/>Meeting ID: <br/>Password: </span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><strong>You can test Zoom in advance via the following link: <a href="https://zoom.us/test">https://zoom.us/test</a></strong></span></p>
					<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">If you encounter problems you are not able to solve do not hesitate to contact us. This way, we make sure that the online assessment can start in a smooth manner.</span></p>'
				END
			END
		ELSE 
			CASE @IsOnline
			  WHEN 0 THEN ''
			  WHEN 1 THEN
				CASE @LanguageId
					WHEN 1 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Voor het online assessment maken we gebruik van <strong>Microsoft Teams</strong>. De Microsoft Teams-uitnodiging voor deze evaluatie ontvangt u via een aparte mail.</p>'
					WHEN 2 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Pour l''évaluation en ligne, nous utilisons <strong>Microsoft Teams</strong>. Vous recevrez l''invitation par un courriel séparé.</p>'
					ELSE '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">The online assessment will be held in <strong>Microsoft Teams</strong>. The invitation will be sent separately.</p>' 
				END
			END
	END
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'ONLINEPARK', CASE @IsOnline 
				WHEN 0 THEN 
					CASE WHEN @ContactName = 'Fod Financiën' THEN ''
					ELSE 		
						CASE @LanguageId 
							WHEN 1 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">De dag van uw assessment kunt u bij ons terecht vanaf &lt;!--OFFICEHOURS--&gt;.&nbsp;&lt;!--OFFICEPARKING--&gt; Mocht u problemen met het verkeer ondervinden, gelieve ons even te verwittigen zodat wij uw komst goed kunnen voorbereiden.</span></p>
							<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">We hechten bijzonder belang aan uw persoonsgegevens en onze confidentialiteit. Voor een verdere duiding van de verschillende opties waarover u beschikt om uw persoonlijke gegevens te beheren en te controleren, verwijzen we u daarom graag naar onze <a href="https://www.quintessence.be/nl/privacy-policy" target="_blank">privacy policy</a>.</span></p>'
							WHEN 2 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Le jour de votre assessment, vous pouvez vous pr&eacute;senter &agrave; partir de &lt;!--OFFICEHOURS--&gt;.&nbsp;&lt;!--OFFICEPARKING--&gt; En cas de probl&egrave;mes de circulation, veuillez nous en informer de sorte que nous puissions pr&eacute;parer votre arriv&eacute;e au mieux.</span></p>
							<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Nous attachons une importance particulière à vos données personnelles et à notre confidentialité. Pour de plus amples explications sur les différentes options qui s''offrent à vous pour gérer et contrôler vos données personnelles, nous vous invitons à consulter notre <a href="https://www.quintessence.be/fr/privacy-policy" target="_blank">politique de confidentialité</a>.</span></p>'
							WHEN 3 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">We expect you on the day of your assessment as from &lt;!--OFFICEHOURS--&gt;.&nbsp;&lt;!--OFFICEPARKING--&gt; If you have any trouble with traffic, please inform us so that we can prepare for your arrival.</span></p>
							<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">We attach particular importance to your personal data and our confidentiality. For a further explanation of the various options available to you to manage and control your personal data, we would like to refer you to our <a href="https://www.quintessence.be/en/privacy-policy" target="_blank">privacy policy</a>.</span></p>'
							WHEN 4 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">We expect you on the day of your assessment as from &lt;!--OFFICEHOURS--&gt;.&nbsp;&lt;!--OFFICEPARKING--&gt; If you have any trouble with traffic, please inform us so that we can prepare for your arrival.</span></p>
							<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">We attach particular importance to your personal data and our confidentiality. For a further explanation of the various options available to you to manage and control your personal data, we would like to refer you to our <a href="https://www.quintessence.be/en/privacy-policy" target="_blank">privacy policy</a>.</span></p>'
						END
					END
				WHEN 1 THEN ''
				END

	INSERT INTO @Table
	SELECT 'OFFICE', [OfficeTranslationView].[Name]
	FROM [OfficeTranslationView]
	WHERE [OfficeTranslationView].[OfficeId] = @OfficeId
	AND [OfficeTranslationView].[LanguageId] = @LanguageId

	INSERT INTO @Table
	SELECT 'CONTEXT', CASE 
						WHEN @ContactName = 'Fod Financiën' AND @Context = 'Centrum voor Klimaatverbetering' THEN CASE @LanguageId
														WHEN 1 THEN 'Centrum voor Klimaatverbetering'
														WHEN 2 THEN 'Le Centre pour l''amélioration du climat'
														WHEN 4 THEN 'Zentrum für Klimaverbesserung'
														ELSE '[EN]Centrum voor Klimaatverbetering'
														END
						ELSE @Context END

	INSERT INTO @Table
	SELECT 'WEBSITE', CASE @LanguageId 
						WHEN 1 THEN CASE
								WHEN @ContactName = 'Fod Financiën' THEN '<a style="font-family: calibri; font-size: 11pt;" href="http://cvk.quintessenceconsulting.be"><span style="color: #002649;"><span style="color: #0000ff;"><strong>http://cvk.quintessenceconsulting.be</strong></span></span></a>'
								ELSE '<a style="font-family: calibri; font-size: 11pt;" href="https://simulation.quintessence.be/nl/login"><span style="color: #002649;"><span style="color: #0000ff;"><strong>https://simulation.quintessence.be/nl/login</strong></span></span></a>'
								END
						WHEN 2 THEN CASE
								WHEN @ContactName = 'Fod Financiën' THEN '<a style="font-family: calibri; font-size: 11pt;" href="http://cac.quintessenceconsulting.be"><span style="color: #002649;"><span style="color: #0000ff;"><strong>http://cac.quintessenceconsulting.be</strong></span></span></a>'
								ELSE '<a style="font-family: calibri; font-size: 11pt;" href="https://simulation.quintessence.be/fr/login"><span style="color: #002649;"><span style="color: #0000ff;"><strong>https://simulation.quintessence.be/fr/login</strong></span></span></a>'
								END
						WHEN 3 THEN CASE
								WHEN @ContactName = 'Fod Financiën' THEN '<a style="font-family: calibri; font-size: 11pt;" href="http://cvk.quintessenceconsulting.be"><span style="color: #002649;"><span style="color: #0000ff;"><strong>http://cvk.quintessenceconsulting.be</strong></span></span></a>'
								ELSE '<a style="font-family: calibri; font-size: 11pt;" href="https://simulation.quintessence.be/en/login"><span style="color: #002649;"><span style="color: #0000ff;"><strong>https://simulation.quintessence.be/en/login</strong></span></span></a>'
								END
						WHEN 4 THEN CASE
								WHEN @ContactName = 'Fod Financiën' THEN '<a style="font-family: calibri; font-size: 11pt;" href="http://zfk.quintessenceconsulting.be"><span style="color: #002649;"><span style="color: #0000ff;"><strong>http://zfk.quintessenceconsulting.be</strong></span></span></a>'
								ELSE '<a style="font-family: calibri; font-size: 11pt;" href="https://simulation.quintessence.be/en/login"><span style="color: #002649;"><span style="color: #0000ff;"><strong>https://simulation.quintessence.be/en/login</strong></span></span></a>'
								END
					  END
	
	INSERT INTO @Table
	SELECT 'BELFIUSAC', CASE @CrmProjectId
						WHEN 5663 THEN 
							CASE @LanguageId
							WHEN 1 THEN ', selectieve proef voor de overgang naar een leidinggevende functie,'
							WHEN 2 THEN ', test sélectif pour la transition à une fonction managériale'
							ELSE '' END
						ELSE '' END

	INSERT INTO @Table
	SELECT 'BELFIUSBODY', CASE @CrmProjectId
						WHEN 5663 THEN 
							CASE @LanguageId
							WHEN 1 THEN						
								'<p>Belfius heeft de ambitie te waken over het engagement van haar medewerkers en de opvolging van talenten. Door deze proeven te organiseren, bieden wij de medewerkers de mogelijkheid om een functie te vinden in overeenstemming met hun competenties en waarbinnen zij zich kunnen ontplooien. Het referentiekader dat gebruikt wordt tijdens deze proef vertrekt vanuit de 4 waarden van Belfius en stemt overeen met deze visie.</p>
								<p>Deze oefening zal u toelaten om uw talenten en progressiemarge beter in te schatten, evenals uw ontwikkelingsplan te bepalen.</p>
								<p>U zal eveneens een link krijgen met de vraag om een persoonlijkheids- en motivatievragenlijst in te vullen.  Gelieve deze vóór uw assessment center in te vullen.</p>
								<p>Gelieve vooraf uw curriculum vitae per mail door te sturen naar Quintessence en deze ook toe te voegen aan uw profiel via e-HR. Hierbij alvast de link die u toegang verleent tot uw profiel : <a href="http://erpportal.dbb.int.dexwired.net/">http://erpportal.dbb.int.dexwired.net/</a></p>'
							WHEN 2 THEN
								'<p>Belfius a l’objectif de garder l’engagement de ses collaborateurs et le suivi des talents. En organisant ces tests, nous  offrons nos collaborateurs l’opportunité de trouver une fonction qui correspond à leurs compétences et dans lequel ils peuvent se développer. Le cadre référentiel utilisé pendant ce test part de 4 valeurs de Belfius et correspond à cette vision.</p>
								<p>Cet exercice vous permet de mieux estimer vos talents et marge de progression, ainsi de construire votre plan de développement.</p>
								<p>Egalement vous recevrez un link avec la demande de remplir le questionnaire de personnalité – et de motivation. Veuillez remplir ceux-ci avant votre assessment.</p>
								<p>Veuillez envoyer votre curriculum vitae par avance à Quintessence et l’ajouter à votre profil via e-HR. Voici votre link qui donne accès à votre profil : <a href="http://erpportal.dbb.int.dexwired.net/">http://erpportal.dbb.int.dexwired.net/</a></p>'
							ELSE '' END
						ELSE '' END

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
		SET LANGUAGE English
		INSERT INTO @Table
		SELECT 'DATE', CAST(CONVERT(nvarchar(max), DATENAME(mm, [AppointmentDate]),100) AS NVARCHAR(MAX))+' '+CAST(DAY([AppointmentDate]) AS NVARCHAR(MAX))+', '+CAST(YEAR([AppointmentDate]) AS NVARCHAR(MAX))
		FROM #TempTable
		END

	IF @LanguageId = 4
		BEGIN 
		SET LANGUAGE German
		INSERT INTO @Table
		SELECT 'DATE', CAST(DAY([AppointmentDate]) AS NVARCHAR(MAX))+' '+CAST(CONVERT(nvarchar(max), DATENAME(mm, [AppointmentDate]),100) AS NVARCHAR(MAX))+' '+CAST(YEAR([AppointmentDate]) AS NVARCHAR(MAX))
		FROM #TempTable
		END

	INSERT INTO @Table
	SELECT 'TIME', [AppointmentHour]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'CONTACT', CASE [ContactName]
						WHEN 'BELFIUS BANK' THEN 'Belfius'
						WHEN 'Fod Financiën' THEN CASE @LanguageId
													WHEN 1 THEN 'de FOD Financiën'
													WHEN 2 THEN 'SPF Finances'
													WHEN 4 THEN 'FÖD Finanzen'
													ELSE '[EN]Fod Financiën'
													END
						ELSE [ContactName] END
	FROM #TempTable

	IF @ProjectId = '46026002-c8b1-4e00-958c-99be772281bc'
		BEGIN
			INSERT INTO @Table
			SELECT 'PROJECTTYPE', 'assessment center'
			FROM #TempTable
		END
		ELSE BEGIN
			INSERT INTO @Table
			SELECT 'PROJECTTYPE', CASE 
						WHEN (@ContactName <> 'Fod Financiën' AND [ProjectTypeName] LIKE '%Custom%') THEN 'assessment center'
						WHEN (@ContactName = 'Fod Financiën' AND [ProjectTypeName] NOT LIKE '%Custom%') THEN CASE @LanguageId
																WHEN 1 THEN 'evaluatie'
																WHEN 2 THEN 'évaluation'
																WHEN 4 THEN 'evaluierung'
																ELSE 'evaluation'
																END
						WHEN (@ContactName = 'Fod Financiën' AND [ProjectTypeName] LIKE '%Custom%') THEN CASE @LanguageId
																WHEN 1 THEN 'interview'
																WHEN 2 THEN 'interview'
																WHEN 4 THEN 'Motivationsgespräch'
																ELSE 'evaluation'
																END 
						ELSE LOWER([ProjectTypeName])
						END
			FROM #TempTable

			INSERT INTO @Table
			SELECT 'PROJECTTYPEFOD', CASE
					WHEN @ContactName = 'Fod Financiën' THEN 
						CASE WHEN @IsOnline = 1 THEN ' <strong>online</strong> '
						ELSE ''
						END +
						CASE @LanguageId
							WHEN 1 THEN CASE WHEN [ProjectTypeName] LIKE '%Custom%' THEN ' uit voor <span style="color: #003873;"><strong>een interview - in het kader van een promotieprocedure voor de functie '+@FunctionTitle+'</strong> '
									ELSE 
										CASE WHEN @CandidateReport = 'F7CD4578-7978-4088-AEC2-27BDBDC4B967'--@ProjectName LIKE '%Golf 11%' 
											THEN ' uit, <span style="color: #003873;"><strong>in het kader van de bevorderingsprocedure voor één (of meerdere functie(s) '+@FunctionTitle+', voor een evaluatie van de specifieke bekwaamheden met betrekking tot deze rol en dit</strong> ' 
											ELSE ', in het kader van een mogelijke promotie tot niveau '+@FunctionTitle+', uit voor <span style="color: #003873;"><strong>een evaluatie van het algemeen functioneren</strong> ' 
										END									
									END
							WHEN 2 THEN CASE WHEN [ProjectTypeName] LIKE '%Custom%' THEN 'pour <span style="color: #003873;"><strong>un interview - dans le cadre d’une éventuelle promotion pour la fonction de '+@FunctionTitle+'</strong>'
									ELSE 
										CASE WHEN @CandidateReport = 'F7CD4578-7978-4088-AEC2-27BDBDC4B967'--@ProjectName LIKE '%Golf 11%' 
											THEN '<span style="color: #003873;"><strong>dans le cadre de la procédure de promotion pour une (ou plusieurs) fonction(s) '+@FunctionTitle+', à une évaluation des aptitudes spécifiques à ce rôle</strong> ' 
											ELSE 'dans le cadre d’une éventuelle promotion vers un niveau '+@FunctionTitle+' pour <span style="color: #003873;"><strong>une évaluation du fonctionnement général</strong> ' 
										END
									END
							WHEN 4 THEN CASE WHEN [ProjectTypeName] LIKE '%Custom%' THEN 'zu <span style="color: #003873;"><strong>einem Motivationsgespräch – Beförderungen auf Niveau '+@FunctionTitle+'</strong> '
									ELSE 'einer Evaluierung des allgemeinen Funktionierens – Beförderung auf Niveau '+@FunctionTitle+' ' END
							ELSE '[EN]een evaluatie van het algemeen functioneren - promoties tot niveau '+@FunctionTitle+' '
							END	
					ELSE CASE 
						WHEN [ProjectTypeName] LIKE '%Custom%' THEN CASE @LanguageId
							WHEN 1 THEN ' uit voor een assessment center'
							WHEN 2 THEN 'à un assessment center'
							WHEN 3 THEN 'for an assessment center'
							WHEN 4 THEN 'for an assessment center'
							END 
						ELSE CASE @LanguageId
							WHEN 1 THEN ' uit voor een '
								+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END
								+LOWER([ProjectTypeName])
							WHEN 2 THEN 'à un '
							+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END
							+LOWER([ProjectTypeName])
							WHEN 3 THEN 'for '+CASE 
										WHEN LOWER([ProjectTypeName]) LIKE 'assessment%' THEN 'an '
										+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END
										WHEN LOWER([ProjectTypeName]) LIKE 'executive%' THEN 'an '
										+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END
										WHEN LOWER([ProjectTypeName]) LIKE 'ori%' THEN 'an '
										+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END
										ELSE 'a '
										+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END 
										END
										+LOWER([ProjectTypeName])
							WHEN 4 THEN 'for '+CASE 
										WHEN LOWER([ProjectTypeName]) LIKE 'assessment%' THEN 'an '
										+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END
										WHEN LOWER([ProjectTypeName]) LIKE 'executive%' THEN 'an '
										+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END
										WHEN LOWER([ProjectTypeName]) LIKE 'ori%' THEN 'an '
										+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END
										ELSE 'a '
										+CASE WHEN @IsOnline = 1 THEN '<strong>online</strong> ' ELSE '' END 
										END
										+LOWER([ProjectTypeName])
							END
						END
					END
			FROM #TempTable
	END


	INSERT INTO @Table
	SELECT 'WEGENWERKEN',
	CASE @LanguageId
		WHEN 1 THEN
			CASE @OfficeId
				WHEN 1 THEN 
					CASE WHEN @AppointmentDate BETWEEN '2018-04-09' AND '2018-04-16' THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><strong>Ingrijpende herstellingen aan wegdek Antwerpse ring (R1) ter hoogte van Deurne tijdens 2e week paasvakantie: 9 t.e.m. 15 april: </strong><a href="http://wegenenverkeer.be/werken/ingrijpende-herstellingen-aan-wegdek-antwerpse-ring-r1-ter-hoogte-van-deurne">meer info</a></p>' 
						ELSE '' 
						END
				ELSE ''
			END
		ELSE ''
	END

	INSERT INTO @Table
	SELECT 'OFFICEADDRESS', 
		CASE @LanguageId
			WHEN 1 THEN
				CASE @OfficeId
					WHEN 1 THEN 'Prins Boudewijnlaan 41/1, 2650 Edegem.'
					WHEN 2 THEN 'Generaal Wahislaan 3, 1030 Brussel.'
					WHEN 3 THEN CASE WHEN @AppointmentDate > '2017-04-01' 
							THEN 'Oktrooiplein 1 (Quantum Building) - 6de verdiep, 9000 Gent.'
							ELSE 'Gaston Crommenlaan 4, 1ste verdieping, 9050 Gent.'
							END
					ELSE ''
				END				
			WHEN 2 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Prins Boudewijnlaan 41/1, 2650 Anvers-Edegem.'
					WHEN 2 THEN 'Boulevard Général Wahis 3, 1030 Bruxelles.'
					WHEN 3 THEN CASE WHEN @AppointmentDate > '2017-04-01' 
							THEN 'Oktrooiplein 1 (Quantum Building) - 6e étage, 9000 Gand.'
							ELSE 'Gaston Crommenlaan 4, 1e étage, 9050 Gand.'
							END
					ELSE ''
				END		
			WHEN 3 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Prins Boudewijnlaan 41/1, 2650 Antwerp-Edegem.'
					WHEN 2 THEN 'Blvd. General Wahis 3, 1030 Brussels.'
					WHEN 3 THEN CASE WHEN @AppointmentDate > '2017-04-01' 
							THEN 'Oktrooiplein 1 (Quantum Building) - 6th floor, 9000 Ghent.'
							ELSE 'Gaston Crommenlaan 4, 1th floor, 9050 Ghent.'
							END
					ELSE ''
				END		
			WHEN 4 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Prins Boudewijnlaan 41/1, 2650 Antwerp-Edegem.'
					WHEN 2 THEN 'Generaal Wahislaan 3, 1030 Brüssel, Belgien.'
					WHEN 3 THEN CASE WHEN @AppointmentDate > '2017-04-01' 
							THEN 'Oktrooiplein 1 (Quantum Building) - 6th floor, 9000 Ghent.'
							ELSE 'Gaston Crommenlaan 4, 1th floor, 9050 Ghent.'
							END
					ELSE ''
				END		
			ELSE 'No address set'
		END

	INSERT INTO @Table
	SELECT 'OFFICEPARKING', 
		CASE @LanguageId
			WHEN 1 THEN
				CASE @OfficeId
					WHEN 1 THEN 'Indien u met de wagen komt, parkeer uw wagen vrij op een Quintessence <strong>parkeerplaats</strong>.'
					WHEN 2 THEN 'Indien u met de wagen komt, parkeer uw wagen vrij op een Quintessence <strong>parkeerplaats</strong>.'
					WHEN 3 THEN CASE WHEN @AppointmentDate > '2017-04-01' 
							THEN 'Indien u met de wagen komt, kan u uw wagen parkeren op de openbare parking Dampoort aan de overzijde van het gebouw.'
							ELSE 'Indien u met de wagen komt, kan u uw wagen parkeren in de ondergrondse parking.'
							END
					ELSE ''
				END				
			WHEN 2 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'Vous pouvez garer votre voiture sur <strong>les emplacements spécialement réservés pour Quintessence</strong>.'
					WHEN 2 THEN 'Vous pouvez garer votre voiture sur <strong>les emplacements spécialement réservés pour Quintessence</strong>.'
					WHEN 3 THEN CASE WHEN @AppointmentDate > '2017-04-01' 
							THEN 'Si vous venez en voiture, vous pouvez garer votre voiture au parking public de l''autre côté du bâtiment.'
							ELSE 'Si vous venez en voiture, vous pouvez garer votre voiture dans le garage souterrain.' 
							END
					ELSE ''
				END		
			WHEN 3 THEN 
				CASE @OfficeId
					WHEN 1 THEN 'If you are coming by car, please park in a <strong>Quintessence parking spot</strong>.'
					WHEN 2 THEN 'If you are coming by car, please park in a <strong>Quintessence parking spot</strong>.'
					WHEN 3 THEN CASE WHEN @AppointmentDate > '2017-04-01' 
							THEN 'If you are coming by car, you can park in the public parking lot across the building.'
							ELSE 'If you are coming by car, you can park underground.'
							END
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

	IF @ProjectId NOT IN ('46026002-c8b1-4e00-958c-99be772281bc','33387cc2-4946-48cc-9b47-ab20603c8718')
			AND @ContactName <> 'Fod Financiën'
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
			WHEN 1 THEN CASE WHEN (@ContactName = 'Fod Financiën' AND @ProjectType LIKE '%Custom%') THEN '45 minuten'
					ELSE 'een halve dag' END
			WHEN 2 THEN CASE WHEN (@ContactName = 'Fod Financiën' AND @ProjectType LIKE '%Custom%') THEN '45 minutes'
					ELSE 'une demi-journée' END
			WHEN 3 THEN 'half a day'
			WHEN 4 THEN CASE WHEN (@ContactName = 'Fod Financiën' AND @ProjectType LIKE '%Custom%') THEN '1 Stunde'
					ELSE 'etwa einen halben Tag' END
			END
		END

	IF (@ContactName <> 'Fod Financiën')
	BEGIN
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
		END
		ELSE BEGIN
			INSERT INTO @Table
			SELECT 'CV', 
			CASE @LanguageId
				WHEN 1 THEN CASE WHEN @ProjectType NOT LIKE '%Custom%' THEN '<p><strong>De resultaten zullen u door de FOD Financiën meegedeeld worden.</strong> De conclusies van deze evaluatie zullen geldig zijn voor alle betrekkingen waarvoor u heeft gesolliciteerd en waarvoor het competentieprofiel van de rol identiek is.</p>'						
							+CASE WHEN @CandidateReport <> 'F7CD4578-7978-4088-AEC2-27BDBDC4B967' THEN --WHEN @ProjectName not like '%Golf 11%' THEN
								+'<p>U zal op een later moment een uitnodiging ontvangen voor het tweede deel van de evaluatie van het algemeen functioneren. U zal voor dit tweede deel uitgenodigd worden voor iedere functie waarvoor u gesolliciteerd heeft.</p>'
								ELSE '' END
							ELSE '<p><strong>De resultaten zullen u door de FOD Financiën meegedeeld worden.</strong></p>' END
				WHEN 2 THEN CASE WHEN @ProjectType NOT LIKE '%Custom%' THEN '<p><strong>Les résultats vous seront communiqués par le SPF Finances.</strong> Les conclusions de cette évaluation seront valables pour toutes les fonctions pour lesquelles vous avez sollicité et dont le profil d’aptitudes spécifiques au rôle est identique.</p>'
							+CASE WHEN @CandidateReport <> 'F7CD4578-7978-4088-AEC2-27BDBDC4B967' THEN --@ProjectName not like '%Golf 11%' THEN
								+'<p>Vous recevrez ultérieurement votre invitation pour la seconde partie de l''évaluation du fonctionnement général. Vous serez invité à cette seconde partie pour chaque fonction pour laquelle vous avez postulé.</p>'
								ELSE '' END
							ELSE '<p><strong>Les résultats vous seront communiqués par le SPF Finances.</strong></p>' END
				WHEN 4 THEN CASE WHEN @ProjectType NOT LIKE '%Custom%' THEN '<p><strong>Die Ergebnisse werden Ihnen vom FÖD Finanzen mitgeteilt werden.</strong> Die Schlussfolgerungen dieser Evaluierung werden für alle Stellen, für die Sie sich beworben haben und deren Kompetenzprofil der Funktion identisch ist, gelten.</p>'
							+CASE WHEN @CandidateReport <> 'F7CD4578-7978-4088-AEC2-27BDBDC4B967' THEN --@ProjectName not like '%Golf 11%' THEN
								+'<p>Sie werden zu einem späteren Zeitpunkt eine Einladung für den zweiten Teil der Evaluierung des allgemeinen Funktionierens erhalten. Für diesen zweiten Teil werden Sie für jede Funktion, für die Sie sich beworben haben, gesondert eingeladen werden.</p>'
								ELSE '' END
							ELSE '<p><strong>Die Ergebnisse werden Ihnen vom FÖD Finanzen mitgeteilt werden.</strong></p>' END
				ELSE '[EN]De conclusies van deze evaluatie zullen geldig zijn voor alle gesolliciteerde betrekkingen waarvan het profiel van de generieke competenties (leidinggevende, expert of projectleider) identiek is.'
				END
		END

	IF @ProjectId = '46026002-c8b1-4e00-958c-99be772281bc'
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

	IF (@ContactName = 'Fod Financiën')
		BEGIN
			DECLARE @UserExist INT
			DECLARE @QUserName NVARCHAR(MAX)
			DECLARE @QPassword NVARCHAR(MAX)

			SET @UserExist = (SELECT COUNT(*) FROM datawarehouse.[dbo].[FodFinToolboxUsers] FF
							WHERE FF.QplanetId = @CandidateId)

			IF (@UserExist = 0)
				BEGIN
					DECLARE @LastName NVARCHAR(MAX) = (SELECT LastName from Quintessence.dbo.CandidateView where Id = @CandidateId)
					DECLARE @FirstName NVARCHAR(MAX) = (SELECT FirstName from Quintessence.dbo.CandidateView where Id = @CandidateId)
					DECLARE @Email NVARCHAR(MAX) = (SELECT ISNULL(Email,'') from Quintessence.dbo.CandidateView WHERE Id = @CandidateId)
					DECLARE @Geslacht INT = (SELECT CASE Gender 
												WHEN 'M' THEN 1
												ELSE 2
												END from Quintessence.dbo.CandidateView WHERE Id = @CandidateId)
					DECLARE @Taal INT = (SELECT CASE @LanguageId
											WHEN 1 THEN 2
											WHEN 2 THEN 3
											WHEN 4 THEN 6
											ELSE 1 END)

					DECLARE	@return_value int
					EXEC	@return_value = Q.[dbo].[AddUser_QPlanet] @LastName,@FirstName,408,'1990-01-01',@Geslacht,'',@Email,@Taal,3,0,0,'','','',1,NULL,0
					INSERT INTO datawarehouse.[dbo].[FodFinToolboxUsers] VALUES (@LastName,@FirstName,408,'1990-01-01',@Geslacht,'',@Email,@Taal,3,0,0,'','','',1,NULL,0,@CandidateId,@return_value)					
				END

			SET @QUserName = (SELECT UserName from Q.dbo.Users WHERE UserId = (SELECT ToolboxId FROM datawarehouse.[dbo].[FodFinToolboxUsers] where QPlanetId = @CandidateId))			
			SET @QPassword = (SELECT [Password] from Q.dbo.Users WHERE UserId = (SELECT ToolboxId FROM datawarehouse.[dbo].[FodFinToolboxUsers] where QPlanetId = @CandidateId))	

			DECLARE @Link NVARCHAR(MAX) = 'http://www.hr-quintessentials.be/login.asp?username='+@QUserName+'&password='+@QPassword

			INSERT INTO @Table
			SELECT 'FODFIN',
			CASE @LanguageId
			WHEN 1 THEN 'Via volgende link kan u uw persoonlijke gegevens aanpassen: <a href='+@Link+'>LINK</a><br><br>
				<p>Mocht u nog vragen hebben, neem dan gerust contact op met ons. We helpen u graag verder.</p>
				<p>Wij wensen u alvast veel succes.</p>'						
			WHEN 2 THEN 'Vous pouvez modifier vos données personnelles via le lien suivant : <a href='+@Link+'>LINK</a><br><br>
			<p>Si vous avez d''autres questions, n''hésitez pas à nous contacter. Nous nous ferons un plaisir de vous aider.</p>
			<p>Nous vous souhaitons bonne chance.</p>'		
			WHEN 4 THEN 'Über den folgenden Link können Sie Ihre persönlichen Daten anpassen: <a href='+@Link+'>LINK</a><br><br>
				<p>Sollten Sie noch Fragen haben, können Sie jederzeit Kontakt mit uns aufnehmen. Wir sind Ihnen gerne behilflich.</p>
				<p>Wir wünschen Ihnen bereits viel Erfolg.</p>'
			ELSE ''
			END					
		END

	ELSE BEGIN
		INSERT INTO @Table
		SELECT 'FODFIN',''
	END

	IF (@ContactName <> 'Fod Financiën')
		BEGIN
			IF NOT EXISTS(SELECT QplanetCandidateId FROM datawarehouse.dbo.QuintessenceCandidateUserProfile where QplanetCandidateId = @CandidateId)
				BEGIN
					DECLARE @Password nvarchar(max)
					SELECT @Password = DataWarehouse.dbo.GenPass() 

					INSERT INTO datawarehouse.dbo.QuintessenceCandidateUserProfile
					VALUES (@CandidateId, (SELECT FirstName from Quintessence.dbo.CandidateView where Id = @CandidateId), (SELECT LastName from Quintessence.dbo.CandidateView where Id = @CandidateId), @Password, NULL, getdate(), 0, NULL)
				END
		END

	INSERT INTO @Table
	SELECT 'MAIL', CASE WHEN @ContactName = 'Fod Financiën' THEN
						CASE WHEN @ProjectType not like '%Custom%' THEN
						CASE @LanguageId
							WHEN 1 THEN '<p>Om uw evaluatie voor te bereiden, is het erg belangrijk om de informatie door te nemen op volgende website: <a href="http://cvk.quintessenceconsulting.be/"><strong>http://cvk.quintessenceconsulting.be</strong></a>.</p>
							<p>U vindt er informatie over volgende zaken :</p>
							<p class="MsoNormal" style="margin-left: 30pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto;"><span style="color: rgb(0, 38, 73); font-family: Symbol; font-size: 11pt; mso-ascii-font-family: Calibri; mso-bidi-font-family: Calibri;">·</span><span style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US; mso-hansi-font-family: Symbol;">   </span><span lang="EN-US" style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US;">Informatie over de evaluatie zelf en het verloop<br>
							</span><span style="color: rgb(0, 38, 73); font-family: Symbol; font-size: 11pt; mso-ascii-font-family: Calibri; mso-bidi-font-family: Calibri;">·</span><span style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US; mso-hansi-font-family: Symbol;">   </span><span lang="EN-US" style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US;">Informatie over de context: tijdens uw evaluatie willen wij graag kunnen zien hoe u met een aantal werksituaties omgaat. Deze praktijkcases spelen zich af in een <strong><span style="font-family: "Calibri",sans-serif;">fictieve context: Het Centrum voor klimaatverbetering</span></strong>. Het is belangrijk dat u voor uw evaluatie de informatie over het Centrum voor klimaatverbetering minstens een keer grondig doorneemt.</span></p>'
							WHEN 2 THEN '<p>Il est très important de parcourir l’information à votre disposition sur le site internet pour préparer votre évaluation : <a href="http://cac.quintessenceconsulting.be/"><strong>http://cac.quintessenceconsulting.be</strong></a>.</p>
							<p>Vous y trouverez plus d’information sur les points suivants :</p>
							<p class="MsoNormal" style="margin-left: 30pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto;"><span style="color: rgb(0, 38, 73); font-family: Symbol; font-size: 11pt; mso-ascii-font-family: Calibri; mso-bidi-font-family: Calibri;">·</span><span style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US; mso-hansi-font-family: Symbol;">   </span><span lang="EN-US" style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US;">Des informations à propos de <strong><span style="font-family: "Calibri",sans-serif;">l''évaluation</span></strong> et
							le déroulement du programme.<br>
							</span><span style="color: rgb(0, 38, 73); font-family: Symbol; font-size: 11pt; mso-ascii-font-family: Calibri; mso-bidi-font-family: Calibri;">·</span><span style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US; mso-hansi-font-family: Symbol;">   </span><span lang="EN-US" style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US;">Information concernant le contexte : durant votre
							évaluation, nous souhaitons voir de quelle manière vous abordez un certain nombre de situations professionnelles. Ces cas pratiques se déroulent	dans un <strong><span style="font-family: "Calibri",sans-serif;">contexte fictif
							: Le Centre pour l''amélioration du climat</span></strong>. Il est important que vous puissiez parcourir de manière approfondie au moins une fois avant votre évaluation l''information sur le Centre pour l''amélioration du climat.</span></p>'
							WHEN 4 THEN '<p>Um Ihre Evaluierung vorzubereiten, ist es sehr wichtig, die Informationen auf der folgenden Website durchzugehen: <a href="http://zfk.quintessenceconsulting.be/"><strong>http://zfk.quintessenceconsulting.be</strong></a>.</p>
							<p>Sie finden dort Informationen zu den folgenden Aspekten:</p>
							<p class="MsoNormal" style="margin-left: 30pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto;"><span style="color: rgb(0, 38, 73); font-family: Symbol; font-size: 11pt; mso-ascii-font-family: Calibri; mso-bidi-font-family: Calibri;">·</span><span style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US; mso-hansi-font-family: Symbol;">   </span><span lang="EN-US" style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US;">Informationen über die Evaluierung an sich und deren Ablauf<br>
							</span><span style="color: rgb(0, 38, 73); font-family: Symbol; font-size: 11pt; mso-ascii-font-family: Calibri; mso-bidi-font-family: Calibri;">·</span><span style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US; mso-hansi-font-family: Symbol;">   </span><span lang="EN-US" style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US;">Informationen über den Kontext: Während Ihrer Evaluierung möchten wir betrachten, wie Sie mit bestimmten Arbeitssituationen umgehen. Diese Praxisfälle spielen sich in einem <strong><span style="font-family: "Calibri",sans-serif;">fiktiven Kontext ab: Das Zentrum für Klimaverbesserung</span></strong>. Es ist wichtig, dass Sie vor Ihrer Evaluierung die Informationen über das Zentrum für Klimaverbesserung mindestens einmal gründlich durchlesen.<br>
							</span><span style="color: rgb(0, 38, 73); font-family: Symbol; font-size: 11pt; mso-ascii-font-family: Calibri; mso-bidi-font-family: Calibri;">·</span><span style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US; mso-hansi-font-family: Symbol;">   </span><strong><span lang="EN-US" style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US;"></span></strong><span lang="EN-US" style="color: rgb(0, 38, 73); font-family: "Calibri",sans-serif; font-size: 11pt; mso-ansi-language: EN-US;">Eine Anfahrtsbeschreibung zu unseren Büroräumen<o:p></o:p></span></p>'
							ELSE ''
						END
						ELSE '' END
					ELSE CASE @LanguageId
						WHEN 1 THEN '<p>Tijdens uw '+LOWER(@ProjectType)+' willen wij graag kunnen zien hoe u met een aantal werksituaties omgaat. 
						Deze werksituaties zijn uiteraard niet uw werkelijke problematieken, maar een aantal cases waarin u kunt tonen wat uw sterkten zijn. 
						Deze cases spelen zich af in een fictieve context, u wordt voor even werknemer van een denkbeeldige organisatie, <strong>nl. '+@Context+'</strong>. Het is belangrijk dat u <strong>voor uw '+LOWER(@ProjectType)+'</strong> de website van <strong>'+@Context+' al eens grondig doorneemt</strong> zodat u zich reeds een goed beeld kan vormen van de organisatie waarin u terecht zal komen.</p>
						<p><strong>Het is niet de bedoeling dat u deze informatie uit het hoofd leert!</strong>
						Maar maak zeker kennis met de structuur, context, de waarden en de normen van de <strong>'+@Context+'</strong> organisatie. Het zal u comfort geven tijdens uw '+LOWER(@ProjectType)+'.</p>
						<p>U vindt alle belangrijke gegevens hier terug: <a href="https://simulation.quintessence.be/nl/login"><strong>https://simulation.quintessence.be/nl/login</strong></a></p>'
						WHEN 2 THEN '<p>Durant votre '+LOWER(@ProjectType)+', nous souhaiterions voir de quelle manière vous abordez un certain nombre de situations professionnelles. 
						Ces situations professionnelles ne sont bien sûr pas vos problématiques professionnelles réelles, mais quelques cas pratiques dans lesquels vous pourrez montrer quels sont vos points forts. 
						Ces cas pratiques se déroulent dans un contexte fictif : vous êtes employé d''une organisation fictive : <strong>'+@Context+'</strong>. Il est important que vous puissiez parcourir au moins une fois de manière approfondie le site Web de <strong>'+@Context+' avant votre '+LOWER(@ProjectType)+'</strong>, ceci afin que vous ayez déjà une vision précise de l''organisation dans laquelle vous allez vous retrouver.</p> 
						<p><strong>Le but n''est pas que vous appreniez ces informations par cœur !</strong> Familiarisez-vous plutôt avec la structure, le contexte, les valeurs et les normes en vigueur au sein de l''organisation <strong>'+@Context+'</strong>.
						Cela vous mettra dans une position plus confortable pendant votre '+LOWER(@ProjectType)+'.</p>
						<p>Vous trouverez toutes les informations importantes ici : <a href="https://simulation.quintessence.be/fr/login"><strong>https://simulation.quintessence.be/fr/login</strong></a></p>'
						WHEN 3 THEN '<p>During your '+LOWER(@ProjectType)+', we would like to see how you approach a number of different work situations. 
						It goes without saying that they are not the actual issues you face, but a number of cases that give you the opportunity to show what your strengths are. 
						These cases take place in a fictional context in which you work for an imaginary organisation: <strong>'+@Context+'</strong>. 
						It’s important that you have <strong>already thoroughly examined the '+@Context+'</strong> website so that you can form a good image of the organisation in which you’ll find yourself.</p>
						<p><strong>You’re not expected to learn this information by heart!</strong> 
						But please make sure you know the structure, context, values and standards of the <strong>'+@Context+'</strong> organisation. It will be of help to you during your '+LOWER(@ProjectType)+'.</p>
						<p>You’ll find all the important details here: <a href="https://simulation.quintessence.be/en/login"><strong>https://simulation.quintessence.be/en/login</strong></a></p>'
						WHEN 4 THEN '<p>During your '+LOWER(@ProjectType)+', we would like to see how you approach a number of different work situations. 
						It goes without saying that they are not the actual issues you face, but a number of cases that give you the opportunity to show what your strengths are. 
						These cases take place in a fictional context in which you work for an imaginary organisation: <strong>'+@Context+'</strong>. 
						It’s important that you have <strong>already thoroughly examined the '+@Context+'</strong> website so that you can form a good image of the organisation in which you’ll find yourself.</p>
						<p><strong>You’re not expected to learn this information by heart!</strong> 
						But please make sure you know the structure, context, values and standards of the <strong>'+@Context+'</strong> organisation. It will be of help to you during your '+LOWER(@ProjectType)+'.</p>
						<p>You’ll find all the important details here: <a href="https://simulation.quintessence.be/en/login"><strong>https://simulation.quintessence.be/en/login</strong></a></p>'
					END
				END

	INSERT INTO @Table
	SELECT 'HELP', CASE WHEN @ContactName = 'Fod Financiën' THEN 
			CASE WHEN @ProjectType NOT LIKE '%Custom%' THEN
			CASE @LanguageId
					WHEN 1 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 14.6667px;">Deze persoonlijkheidsvragenlijst laat een beoordeling toe van de vijf hoofddimensies van persoonlijkheid. Wij vragen u om verschillende stellingen te beoordelen die gelinkt zijn aan u als persoon en die dus niet noodzakelijk gelinkt zijn aan uw werkcontext. Het is belangrijk om deze persoonlijkheidsvragenlijst in te vullen <u><strong>vóór</strong></u> uw evaluatie. Indien er iets fout loopt bij het inloggen op bovenstaande website, kunt u altijd contact met ons opnemen. Wij helpen u graag verder tussen 8u30 en 17u.</span></p>'
					WHEN 2 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 14.6667px;">Ce questionnaire de personnalité permet l’évaluation des cinq domaines principaux de la personnalité. Nous vous demandons de juger de différentes affirmations liées à votre personne et donc non pas nécessairement liées à votre contexte de travail. Il est important de remplir ce questionnaire de personnalité <u><strong>avant</strong></u> votre participation à l''évaluation. Si vous rencontrez des difficult&eacute;s lors de la connexion au site mentionn&eacute; ci-dessus, n&rsquo;h&eacute;sitez pas &agrave; nous contacter. Nous vous aiderons avec plaisir, entre 8h30 et 17h.</span></p>'
					WHEN 3 THEN ''
					WHEN 4 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 14.6667px;">Dieser Persönlichkeitsfragebogen ermöglicht eine Beurteilung der fünf wichtigsten Persönlichkeitsfaktoren. Wir bitten Sie, verschiedene Thesen zu beurteilen, die mit Ihnen als Person im Zusammenhang stehen und somit nicht zwangsläufig mit Ihrem Arbeitskontext. Es ist wichtig, diesen Persönlichkeitsfragebogen <u><strong>vor</strong></u> Ihrer Evaluierung auszufüllen. Wenn beim Einloggen auf der vorgenannten Website Probleme auftreten, können Sie uns jederzeit kontaktieren. Wir stehen zwischen 08.30 und 17.00 Uhr zu Ihrer Verfügung.</span></p>'
				END
			ELSE '' END
			ELSE CASE @LanguageId
			WHEN 1 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Indien er iets fout loopt bij het inloggen op bovenstaande website, kunt u altijd contact met ons opnemen. Wij helpen u graag verder tussen 8u en 17u.</span></p>'
			WHEN 2 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 14.6667px;">Si vous rencontrez des difficult&eacute;s lors de la connexion au site mentionn&eacute; ci-dessus, n&rsquo;h&eacute;sitez pas &agrave; nous contacter. Nous vous aiderons avec plaisir, entre 8h et 17h.</span></p>'
			WHEN 3 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">If something goes wrong when you log into the website above, you can always contact us. We are happy to help you between 8am and 5pm.</span></p>'
			WHEN 4 THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">If something goes wrong when you log into the website above, you can always contact us. We are happy to help you between 8am and 5pm.</span></p>'
		END
	END

	INSERT INTO @Table
	SELECT 'IMPORTANT', CASE WHEN @ContactName = 'Fod Financiën' THEN 
			CASE WHEN @ProjectType NOT like '%Custom%' THEN
				CASE @LanguageId
						WHEN 1 THEN '<p><strong><u><span style="color: #002649; font-family: calibri; font-size: 11pt;">Belangrijke punten ter voorbereiding:</span></u></strong></p>'
						WHEN 2 THEN '<p><strong><u><span style="color: #002649; font-family: calibri; font-size: 11pt;">Points importants en guise de pr&eacute;paration :</span></u></strong></p>'
						WHEN 3 THEN ''
						WHEN 4 THEN '<p><strong><u><span style="color: #002649; font-family: calibri; font-size: 11pt;">Wichtige Aspekte zur Vorbereitung:</span></u></strong></p>'
					END
				ELSE '' END
			ELSE CASE @LanguageId
			WHEN 1 THEN '<p><strong><span style="color: #385623; font-family: calibri; font-size: 11pt;">Belangrijk als voorbereiding!</span></strong></p>'
			WHEN 2 THEN '<p><strong><span style="color: #385623; font-family: calibri; font-size: 11pt;">Points importants en guise de pr&eacute;paration !</span></strong></p>'
			WHEN 3 THEN '<p><strong><span style="color: #385623; font-family: calibri; font-size: 11pt;">Important preparations!</span></strong></p>'
			WHEN 4 THEN '<p><strong><span style="color: #385623; font-family: calibri; font-size: 11pt;">Important preparations!</span></strong></p>'
		END
	END
	
	INSERT INTO @Table
	SELECT 'CONTEXTLOGIN', CASE WHEN @ContactName = 'Fod Financiën' THEN ''
						ELSE CASE @LanguageId
						WHEN 1 THEN 'Vanaf heden tot aan uw '+LOWER(@ProjectType)+' kan u zoveel inloggen als u wil met volgende gegevens:<br/>'
						WHEN 2 THEN 'A partir de maintenant jusqu''à ce que votre '+LOWER(@ProjectType)+', vous pouvez vous connecter autant que vous voulez avec les informations suivantes :<br/><!--SIMCONLOGINS-->'
						WHEN 3 THEN 'From now on till your '+LOWER(@ProjectType)+' you can log in as many times as you like using the following details:<br/>'
						WHEN 4 THEN 'From now on till your '+LOWER(@ProjectType)+' you can log in as many times as you like using the following details:<br/>'
					END
				END

	INSERT INTO @Table
	SELECT 'HOERABOEK', CASE WHEN @ContactName = 'Fod Financiën' THEN 
							CASE @LanguageId
								WHEN 1 THEN ''
								WHEN 2 THEN ''
								WHEN 3 THEN ''
								WHEN 4 THEN ''
							END
						ELSE CASE @LanguageId
						WHEN 1 THEN '<p>Last, but not least: <a href="http://epub02.publitas.com/quintessence/9/"><strong>hier</strong></a> vindt u het boek: <strong><span style="color: #385623;">“Hoera, ik neem deel aan een assessment center”</span></strong>, terug. Dit is een handige gids die u eens rustig kan doornemen zodat u ontspannen en goed geïnformeerd aan uw '+LOWER(@ProjectType)+' kan beginnen.'+
						CASE 
							WHEN @ContactName = 'FOD Volksgezondheid Veiligheid' THEN ' Hierin staat de assessment center methode toegelicht, welke ook toegepast wordt tijdens uw development center.</p>' 
							WHEN @ContactName = 'Havenbedrijf Antwerpen' THEN ' Bovendien vindt u in dit document meer informatie over de meerwaarde van een deelname aan het development center: <a href="http://www.iedereencompetent.be/assessment@quintessence/Havenbedrijf_voordelen_van_een_development_center.pdf">LINK</a></p>'
							ELSE '</p>' END
						+'<p><strong><span style="color: #385623; font-family: calibri; font-size: 11pt;">En onthoud: wij zijn er om u te helpen!</span></strong></p>
						<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Mocht u verder nog vragen hebben, bel ons gerust! Als u tevreden bent, zijn wij het ook.</span></p>
						<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">We wensen u alvast veel succes. <strong><span style="color: #385623;">Het allerbelangrijkste tijdens een '+LOWER(@ProjectType)+' is dat u gewoon uzelf blijft.</span></strong> Dan haalt u er het meeste uit!</span></p>'+
						CASE WHEN @ContactName = 'FOD Volksgezondheid Veiligheid' THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Mocht u in de loop van het proces beslissen om niet langer aan de procedure deel te nemen of omwille van andere redenen niet aanwezig kunnen zijn op het assessment, zouden we u willen vragen om ons
						hiervan zo spoedig mogelijk op de hoogte te brengen. We vernemen graag uw beweegredenen. Bovendien kunnen we op deze manier nodeloze kosten vermijden.</p>' ELSE '' END
						WHEN 2 THEN '<p>Last, but not least: vous trouverez <a href="https://view.publitas.com/quintessence-consulting/chouette-je-participe-a-un-assessment-center/page/1"><strong>ici</strong></a> le livre : <strong><span style="color: #385623;">“Chouette, je participe à un assessment center”</span></strong>. Il s’agit d’un guide pratique que vous pouvez d’ores et déjà parcourir tranquillement afin d’aborder l’assessment sans appréhension et en connaissance de cause.'+
						CASE WHEN @ContactName = 'FOD Volksgezondheid Veiligheid' THEN ' Ce livre décrit la méthodologie d’un assessment center et qui est également appliquée durant votre development center.</p>' ELSE '</p>' END
						+'<p><strong><span style="color: #385623; font-family: calibri; font-size: 11pt;">N&rsquo;oubliez pas : nous sommes l&agrave; pour vous aider !</span></strong></p>
						<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Si vous avez d&rsquo;autres questions, n&rsquo;h&eacute;sitez pas &agrave; nous appeler ! Votre satisfaction est la n&ocirc;tre.</span></p>
						<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Nous vous souhaitons bonne chance. <strong><span style="color: #385623;">Le plus important lors d&rsquo;un '+LOWER(@ProjectType)+' est que vous restiez simplement vous-m&ecirc;me.</span></strong> C&rsquo;est ainsi que vous en retirerez le plus !</span></p>'+
						CASE WHEN @ContactName = 'FOD Volksgezondheid Veiligheid' THEN '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Si vous décidez de ne plus participer au processus  ou pour d’autres raisons vous pensez ne pas pouvoir être présent lors du development center, nous vous demandons de nous informer le plus vite possible. Ainsi nous pourrons éviter des coûts inutiles.</p>'  ELSE '' END
						WHEN 3 THEN '<p>Last, but not least: click <a href="https://view.publitas.com/quintessence-consulting/test/page/1"><strong>here</strong></a> for the book: <strong><span style="color: #385623;">“Making my assessment center a great experience”</span></strong>. This is a handy guide that you’ll be able to go through at your own pace. It will help to relax you and keep you informed about your '+LOWER(@ProjectType)+'.</p>
						<p><strong><span style="color: #385623; font-family: calibri; font-size: 11pt;">And remember &hellip; we&rsquo;re here to help you!</span></strong></p>
						<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">If you have any further questions, please feel free to call us! If you&rsquo;re happy, we are too.</span></p>
						<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">We wish you all the best. <strong><span style="color: #385623;">The most important thing during your '+LOWER(@ProjectType)+' is to be yourself.</span></strong> Then you&rsquo;ll get the most out of the experience!</span></p>'
						WHEN 4 THEN '<p>Last, but not least: click <a href="https://view.publitas.com/quintessence-consulting/test/page/1"><strong>here</strong></a> for the book: <strong><span style="color: #385623;">“Making my assessment center a great experience”</span></strong>. This is a handy guide that you’ll be able to go through at your own pace. It will help to relax you and keep you informed about your '+LOWER(@ProjectType)+'.</p>
						<p><strong><span style="color: #385623; font-family: calibri; font-size: 11pt;">And remember &hellip; we&rsquo;re here to help you!</span></strong></p>
						<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">If you have any further questions, please feel free to call us! If you&rsquo;re happy, we are too.</span></p>
						<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">We wish you all the best. <strong><span style="color: #385623;">The most important thing during your '+LOWER(@ProjectType)+' is to be yourself.</span></strong> Then you&rsquo;ll get the most out of the experience!</span></p>'
					END
				END
		
	DROP TABLE #TempTable

	SELECT *
	FROM @Table
END