--Dictionaries
--------------
PRINT 'Inserting Dictionaries - start'
INSERT INTO [Dictionary]([Id], [ContactId], [Name], [Description], [Current], [LegacyId])
	SELECT	NEWID(), ActDictionary.[Contact_ID], ActDictionary.[Omschrijving], NULL, 0, ActDictionary.[Woordenboek_id]
	FROM [$(actserver)].[$(Act)].[dbo].[Woordenboek] ActDictionary
	WHERE ActDictionary.[Woordenboek_id] IN (160,121,73,122,88,87,104,117,116,102,159,144,118,125,33,152,41,169,72,27,109,75,78,60,158,89,173,177)
	--As defined in document http://qshare/Qdocumenten/Lijst%20woordenboeken%20in%20ACT.xlsx

UPDATE	[Dictionary]
SET		[Dictionary].[ContactId] = NULL
WHERE	[Dictionary].[ContactId] = 3
PRINT 'Inserting Dictionaries - end'

--Clusters
----------
PRINT 'Inserting Clusters - start'
INSERT INTO [DictionaryCluster]([Id], [DictionaryId], [Name], [LegacyId], [Order], [ImageName])
	SELECT		NEWID(), [Dictionary].[Id], ActClusters.[Name], ActCombination.[Cluster_ID], ActCombination.[Cluster_ID], 'default.png'
	FROM		(SELECT DISTINCT woordenboek_id, cluster_id FROM [$(actserver)].[$(Act)].[dbo].[w_samengesteld]) ActCombination
	INNER JOIN	[Dictionary]
		ON		[Dictionary].[LegacyId] = ActCombination.[woordenboek_id]
	INNER JOIN	[$(actserver)].[$(Act)].[dbo].[clusters] ActClusters
		ON		ActClusters.[Cluster_ID] = ActCombination.[Cluster_ID]
	ORDER BY	ActCombination.[Cluster_ID]

UPDATE		[DictionaryCluster]		SET		[ImageName] = 'zelfmanagement'		WHERE [LegacyId] = 145
UPDATE		[DictionaryCluster]		SET		[ImageName] = 'interactie'			WHERE [LegacyId] = 146
UPDATE		[DictionaryCluster]		SET		[ImageName] = 'informatie'			WHERE [LegacyId] = 147
UPDATE		[DictionaryCluster]		SET		[ImageName] = 'probleemoplossen'	WHERE [LegacyId] = 148
UPDATE		[DictionaryCluster]		SET		[ImageName] = 'organisatie'			WHERE [LegacyId] = 149
UPDATE		[DictionaryCluster]		SET		[ImageName] = 'leidinggeven'		WHERE [LegacyId] = 150
PRINT 'Inserting Clusters - end'

--Cluster translations
----------------------
PRINT 'Inserting Cluster translations - start'
INSERT INTO [DictionaryClusterTranslation]([Id], [LanguageId], [DictionaryClusterId], [Text], [Description])
	SELECT		NEWID(), ActClusterTranslation.[taal_id], [DictionaryCluster].[Id], ActClusterTranslation.[Omschrijving], ActClusterTranslation.[Definitie]
	FROM		[$(actserver)].[$(Act)].[dbo].[cluster_strings] ActClusterTranslation
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[LegacyId] = ActClusterTranslation.[cluster_id]
PRINT 'Inserting Cluster translations - end'

--Competences
-------------
PRINT 'Inserting Competences - start'
INSERT INTO [DictionaryCompetence]([Id], [DictionaryClusterId], [Name], [LegacyId])
	SELECT		NEWID(), [DictionaryCluster].[Id], ActCompetences.[Name], ActCombination.[competentie_id]
	FROM		(SELECT DISTINCT woordenboek_id, cluster_id, competentie_id FROM [$(actserver)].[$(Act)].[dbo].[w_samengesteld]) ActCombination
	INNER JOIN	[Dictionary]
		ON		[Dictionary].[LegacyId] = ActCombination.[woordenboek_id]
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[LegacyId] = ActCombination.[cluster_id]
		AND		[DictionaryCluster].[DictionaryId] = [Dictionary].[Id]
	INNER JOIN	[$(actserver)].[$(Act)].[dbo].[competenties] ActCompetences
		ON		ActCompetences.[competentie_ID] = ActCombination.[competentie_ID]
PRINT 'Inserting Competences - end'

--Competence translations
-------------------------
PRINT 'Inserting Competence translations - start'
INSERT INTO [DictionaryCompetenceTranslation]([Id], [LanguageId], [DictionaryCompetenceId], [Text], [Description])
	SELECT		NEWID(), ActCompetenceTranslation.[taal_id], [DictionaryCompetence].[Id], ActCompetenceTranslation.[Omschrijving], ActCompetenceTranslation.[Definitie]
	FROM		[$(actserver)].[$(Act)].[dbo].[competentie_strings] ActCompetenceTranslation
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[LegacyId] = ActCompetenceTranslation.[competentie_id]
PRINT 'Inserting Competence translations - end'

--Levels
--------
PRINT 'Inserting levels - start'
INSERT INTO [DictionaryLevel]([Id], [DictionaryCompetenceId], [Name], [Level], [LegacyId])
	SELECT		NEWID(), [DictionaryCompetence].[Id], ActLevels.[Niveau], ActLevels.[Niveau], ActLevels.[Niveau_id]
	FROM		[$(actserver)].[$(Act)].[dbo].[niveaus] ActLevels
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[LegacyId] = ActLevels.[competentie_id]

INSERT INTO [DictionaryLevel]([Id], [DictionaryCompetenceId], [Name], [Level], [LegacyId])
	SELECT		NEWID(), [DictionaryCompetence].[Id], 0, 0, 0
	FROM		[DictionaryCompetence]
PRINT 'Inserting levels - end'

--level translations
--------------------
PRINT 'Inserting Level translations - start'
INSERT INTO [DictionaryLevelTranslation]([Id], [LanguageId], [DictionaryLevelId], [Text])
	SELECT		NEWID(), ActLevelTranslation.[Taal_id], [DictionaryLevel].[Id], ActLevelTranslation.[Omschrijving]
	FROM		[$(actserver)].[$(Act)].[dbo].[niveaus_strings] ActLevelTranslation
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[LegacyId] = ActLevelTranslation.[niveau_id]
PRINT 'Inserting Level translations - end'

--Indicators
------------
PRINT 'Inserting indicators - start'
INSERT INTO [DictionaryIndicator]([Id], [DictionaryLevelId], [Name], [LegacyId])
	SELECT		NEWID(), [DictionaryLevel].[Id], ActIndicator.[Name], ActIndicator.[indicator_id]
	FROM		[$(actserver)].[$(Act)].[dbo].[Indicatoren] ActIndicator
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[Level] = ActIndicator.[niveau]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[Id] = [DictionaryLevel].[DictionaryCompetenceId]
		AND		[DictionaryCompetence].[LegacyId] = ActIndicator.[competentie_id]
PRINT 'Inserting indicators - end'

--Indicator translations
------------------------
PRINT 'Inserting Indicator translations - start'
INSERT INTO [DictionaryIndicatorTranslation]([Id], [LanguageId], [DictionaryIndicatorId], [Text])
	SELECT		NEWID(), ActIndicatorTranslation.[taal_id], [DictionaryIndicator].[Id], ActIndicatorTranslation.[omschrijving]
	FROM		[$(actserver)].[$(Act)].[dbo].[indicator_strings] ActIndicatorTranslation
	INNER JOIN	[DictionaryIndicator]
		ON		[DictionaryIndicator].[LegacyId] = ActIndicatorTranslation.[indicator_id]
PRINT 'Inserting Indicator translations - end'

--Colors
--------
UPDATE	[DictionaryCluster]
SET		[Order] = 10, [Color] = '#F58220'
WHERE	[DictionaryCluster].[DictionaryId] IN (SELECT	[DictionaryView].[Id]	FROM	[DictionaryView]	WHERE	[DictionaryView].[ContactId]	IS NULL)
	AND	[DictionaryCluster].[Name] = 'Self-Management'
	
UPDATE	[DictionaryCluster]
SET		[Order] = 20, [Color] = '#EC1D25'
WHERE	[DictionaryCluster].[DictionaryId] IN (SELECT	[DictionaryView].[Id]	FROM	[DictionaryView]	WHERE	[DictionaryView].[ContactId]	IS NULL)
	AND	[DictionaryCluster].[Name] = 'Interaction'
	
UPDATE	[DictionaryCluster]
SET		[Order] = 30, [Color] = '#0095DA'
WHERE	[DictionaryCluster].[DictionaryId] IN (SELECT	[DictionaryView].[Id]	FROM	[DictionaryView]	WHERE	[DictionaryView].[ContactId]	IS NULL)
	AND	[DictionaryCluster].[Name] = 'Information'
	
UPDATE	[DictionaryCluster]
SET		[Order] = 40, [Color] = '#007C99'
WHERE	[DictionaryCluster].[DictionaryId] IN (SELECT	[DictionaryView].[Id]	FROM	[DictionaryView]	WHERE	[DictionaryView].[ContactId]	IS NULL)
	AND	[DictionaryCluster].[Name] = 'Problem-Solving'
	
UPDATE	[DictionaryCluster]
SET		[Order] = 50, [Color] = '#8DC73F'
WHERE	[DictionaryCluster].[DictionaryId] IN (SELECT	[DictionaryView].[Id]	FROM	[DictionaryView]	WHERE	[DictionaryView].[ContactId]	IS NULL)
	AND	[DictionaryCluster].[Name] = 'Organisation'
	
UPDATE	[DictionaryCluster]
SET		[Order] = 60, [Color] = '#904097'
WHERE	[DictionaryCluster].[DictionaryId] IN (SELECT	[DictionaryView].[Id]	FROM	[DictionaryView]	WHERE	[DictionaryView].[ContactId]	IS NULL)
	AND	[DictionaryCluster].[Name] = 'People Management'