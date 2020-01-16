GO

DECLARE @LanguageId AS INT = 1
DECLARE @ContactId AS INT
DECLARE @DictionaryTable AS TABLE(ClusterName NVARCHAR(MAX), ClusterDefinition NVARCHAR(MAX), CompetenceName NVARCHAR(MAX), CompetenceDefinition NVARCHAR(MAX), Level INT, LevelDefinition NVARCHAR(MAX), Indicator NVARCHAR(MAX), ClusterLegacyId INT, CompetenceLegacyId INT, LevelLegacyId INT, IndicatorLegacyId INT)

INSERT INTO @DictionaryTable
	select	cluster_strings.omschrijving		as	ClusterName, 
			cluster_strings.definitie			as	ClusterDefinition,
			competentie_strings.omschrijving	as	CompetenceName, 
			competentie_strings.definitie		as	CompetenceDefinition,
			niveaus.niveau						as	Level, 
			niveaus_strings.omschrijving		as	LevelDefinition,
			indicator_strings.omschrijving		as	Indicator,
			clusters.cluster_id					as	ClusterLegacyId, 
			competenties.competentie_id			as	CompetenceLegacyId, 
			niveaus.niveau_id					as	LevelLegacyId,
			indicatoren.indicator_id			as	IndicatorLegacyId

	from [$(ActServer)].[$(Act)].[dbo].w_samengesteld w_samengesteld

	left join [$(ActServer)].[$(Act)].[dbo].clusters clusters
		on clusters.cluster_id = w_samengesteld.cluster_id
	left join [$(ActServer)].[$(Act)].[dbo].cluster_strings cluster_strings
		on cluster_strings.cluster_id = clusters.cluster_id
		and cluster_strings.taal_id = @languageid

	left join [$(ActServer)].[$(Act)].[dbo].competenties competenties
		on competenties.competentie_id = w_samengesteld.competentie_id
	left join [$(ActServer)].[$(Act)].[dbo].competentie_strings competentie_strings
		on competentie_strings.competentie_id = competenties.competentie_id
		and competentie_strings.taal_id = @languageid

	left join [$(ActServer)].[$(Act)].[dbo].indicatoren indicatoren
		on indicatoren.indicator_id = w_samengesteld.indicator_id
	left join [$(ActServer)].[$(Act)].[dbo].indicator_strings indicator_strings
		on indicator_strings.indicator_id = indicatoren.indicator_id
		and indicator_strings.taal_id = @languageid

	left join [$(ActServer)].[$(Act)].[dbo].niveaus niveaus
		on niveaus.competentie_id = competenties.competentie_id
		and niveaus.woordenboek_id = 158
		and niveaus.niveau = indicatoren.niveau
	left join [$(ActServer)].[$(Act)].[dbo].niveaus_strings niveaus_strings
		on niveaus_strings.niveau_id = niveaus.niveau_id
		and niveaus_strings.taal_id = @languageid

	where w_samengesteld.woordenboek_id = 158

	order by w_samengesteld.cluster_id, w_samengesteld.competentie_id, w_samengesteld.indicator_id	

SELECT @ContactId = contact_id FROM [$(ActServer)].[$(Act)].[dbo].woordenboek where woordenboek_id = 158

DECLARE @DictionaryId AS UNIQUEIDENTIFIER = NEWID()
INSERT INTO [Dictionary]([Id], [ContactId], [Name], [Description], [Current], [LegacyId])
	SELECT	@DictionaryId, @ContactId, ActDictionary.[Omschrijving], NULL, 0, ActDictionary.[Woordenboek_id]
	FROM [$(ActServer)].[$(Act)].[dbo].[Woordenboek] ActDictionary
	WHERE ActDictionary.[Woordenboek_id] = 158

INSERT INTO [DictionaryCluster]([Id], [DictionaryId], [Name], [Description], [LegacyId], [Order], [ImageName])
	SELECT		NEWID(), @DictionaryId, [ClusterName], [ClusterDefinition], [ClusterLegacyId], [ClusterLegacyId], 'default'
	FROM		(SELECT DISTINCT [ClusterName], [ClusterDefinition], [ClusterLegacyId] FROM @DictionaryTable) AS DictionaryTable

INSERT INTO [DictionaryCompetence]([Id], [DictionaryClusterId], [Name], [Description], [LegacyId])
	SELECT		NEWID(), [DictionaryCluster].[Id], [CompetenceName], [CompetenceDefinition], [CompetenceLegacyId]
	FROM		(SELECT DISTINCT [CompetenceName], [CompetenceDefinition], [ClusterLegacyId], [CompetenceLegacyId] FROM @DictionaryTable) AS DictionaryTable
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[LegacyId] =  DictionaryTable.[ClusterLegacyId]
		AND		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryLevel]([Id], [DictionaryCompetenceId], [Level], [Name], [LegacyId])
	SELECT		NEWID(), [DictionaryCompetence].[Id], 0, '', 0
	FROM		[DictionaryCompetence]
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[Id] =  [DictionaryCompetence].[DictionaryClusterId]
		AND		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryIndicator]([Id], [DictionaryLevelId], [Name], [LegacyId])
	SELECT		NEWID(), [DictionaryLevel].[Id], [Indicator], [IndicatorLegacyId]
	FROM		(SELECT DISTINCT [Indicator], [CompetenceLegacyId], [IndicatorLegacyId] FROM @DictionaryTable) AS DictionaryTable
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[LegacyId] =  DictionaryTable.[CompetenceLegacyId]
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[Id] =  [DictionaryCompetence].[DictionaryClusterId]
		AND		[DictionaryCluster].[DictionaryId] = @DictionaryId
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[DictionaryCompetenceId] =  [DictionaryCompetence].[Id]

SET @LanguageId = 1
DELETE FROM @DictionaryTable
INSERT INTO @DictionaryTable
	select	cluster_strings.omschrijving		as	ClusterName, 
			cluster_strings.definitie			as	ClusterDefinition,
			competentie_strings.omschrijving	as	CompetenceName, 
			competentie_strings.definitie		as	CompetenceDefinition,
			niveaus.niveau						as	Level, 
			niveaus_strings.omschrijving		as	LevelDefinition,
			indicator_strings.omschrijving		as	Indicator,
			clusters.cluster_id					as	ClusterLegacyId, 
			competenties.competentie_id			as	CompetenceLegacyId, 
			niveaus.niveau_id					as	LevelLegacyId,
			indicatoren.indicator_id			as	IndicatorLegacyId

	from [$(ActServer)].[$(Act)].[dbo].w_samengesteld w_samengesteld

	left join [$(ActServer)].[$(Act)].[dbo].clusters clusters
		on clusters.cluster_id = w_samengesteld.cluster_id
	left join [$(ActServer)].[$(Act)].[dbo].cluster_strings cluster_strings
		on cluster_strings.cluster_id = clusters.cluster_id
		and cluster_strings.taal_id = @languageid

	left join [$(ActServer)].[$(Act)].[dbo].competenties competenties
		on competenties.competentie_id = w_samengesteld.competentie_id
	left join [$(ActServer)].[$(Act)].[dbo].competentie_strings competentie_strings
		on competentie_strings.competentie_id = competenties.competentie_id
		and competentie_strings.taal_id = @languageid

	left join [$(ActServer)].[$(Act)].[dbo].indicatoren indicatoren
		on indicatoren.indicator_id = w_samengesteld.indicator_id
	left join [$(ActServer)].[$(Act)].[dbo].indicator_strings indicator_strings
		on indicator_strings.indicator_id = indicatoren.indicator_id
		and indicator_strings.taal_id = @languageid

	left join [$(ActServer)].[$(Act)].[dbo].niveaus niveaus
		on niveaus.competentie_id = competenties.competentie_id
		and niveaus.woordenboek_id = 158
		and niveaus.niveau = indicatoren.niveau
	left join [$(ActServer)].[$(Act)].[dbo].niveaus_strings niveaus_strings
		on niveaus_strings.niveau_id = niveaus.niveau_id
		and niveaus_strings.taal_id = @languageid

	where w_samengesteld.woordenboek_id = 158

	order by w_samengesteld.cluster_id, w_samengesteld.competentie_id, w_samengesteld.indicator_id

INSERT INTO [DictionaryClusterTranslation]([Id], [LanguageId], [DictionaryClusterId], [Text], [Description])
	SELECT		NEWID(), @LanguageId, [DictionaryCluster].[Id], [ClusterName], [ClusterDefinition]
	FROM		[DictionaryCluster]
	INNER JOIN	(SELECT DISTINCT [ClusterName], [ClusterDefinition], ClusterLegacyId FROM @DictionaryTable) DictionaryTable ON DictionaryTable.ClusterLegacyId = [DictionaryCluster].[LegacyId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryCompetenceTranslation]([Id], [LanguageId], [DictionaryCompetenceId], [Text], [Description])
	SELECT		NEWID(), @LanguageId, [DictionaryCompetence].[Id], [CompetenceName], [CompetenceDefinition]
	FROM		[DictionaryCompetence]
	INNER JOIN	(SELECT DISTINCT [CompetenceName], [CompetenceDefinition], CompetenceLegacyId FROM @DictionaryTable) DictionaryTable ON DictionaryTable.CompetenceLegacyId = [DictionaryCompetence].[LegacyId]
	INNER JOIN	[DictionaryCluster] ON [DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryLevelTranslation]([Id], [LanguageId], [DictionaryLevelId], [Text])
	SELECT		NEWID(), @LanguageId, [DictionaryLevel].[Id], ''
	FROM		[DictionaryLevel]
	INNER JOIN	[DictionaryCompetence] ON [DictionaryCompetence].[Id] = [DictionaryLevel].[DictionaryCompetenceId]
	INNER JOIN	[DictionaryCluster] ON [DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryIndicatorTranslation]([Id], [LanguageId], [DictionaryIndicatorId], [Text])
	SELECT		NEWID(), @LanguageId, [DictionaryIndicator].[Id], ISNULL([Indicator], '')
	FROM		[DictionaryIndicator]
	INNER JOIN	(SELECT DISTINCT [Indicator], IndicatorLegacyId FROM @DictionaryTable) DictionaryTable ON DictionaryTable.IndicatorLegacyId = [DictionaryIndicator].[LegacyId]
	INNER JOIN	[DictionaryLevel] ON [DictionaryLevel].[Id] = [DictionaryIndicator].[DictionaryLevelId]
	INNER JOIN	[DictionaryCompetence] ON [DictionaryCompetence].[Id] = [DictionaryLevel].[DictionaryCompetenceId]
	INNER JOIN	[DictionaryCluster] ON [DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId
	

SET @LanguageId = 2
DELETE FROM @DictionaryTable
INSERT INTO @DictionaryTable
	select	cluster_strings.omschrijving		as	ClusterName, 
			cluster_strings.definitie			as	ClusterDefinition,
			competentie_strings.omschrijving	as	CompetenceName, 
			competentie_strings.definitie		as	CompetenceDefinition,
			niveaus.niveau						as	Level, 
			niveaus_strings.omschrijving		as	LevelDefinition,
			indicator_strings.omschrijving		as	Indicator,
			clusters.cluster_id					as	ClusterLegacyId, 
			competenties.competentie_id			as	CompetenceLegacyId, 
			niveaus.niveau_id					as	LevelLegacyId,
			indicatoren.indicator_id			as	IndicatorLegacyId

	from [$(ActServer)].[$(Act)].[dbo].w_samengesteld w_samengesteld

	left join [$(ActServer)].[$(Act)].[dbo].clusters clusters
		on clusters.cluster_id = w_samengesteld.cluster_id
	left join [$(ActServer)].[$(Act)].[dbo].cluster_strings cluster_strings
		on cluster_strings.cluster_id = clusters.cluster_id
		and cluster_strings.taal_id = @languageid

	left join [$(ActServer)].[$(Act)].[dbo].competenties competenties
		on competenties.competentie_id = w_samengesteld.competentie_id
	left join [$(ActServer)].[$(Act)].[dbo].competentie_strings competentie_strings
		on competentie_strings.competentie_id = competenties.competentie_id
		and competentie_strings.taal_id = @languageid

	left join [$(ActServer)].[$(Act)].[dbo].indicatoren indicatoren
		on indicatoren.indicator_id = w_samengesteld.indicator_id
	left join [$(ActServer)].[$(Act)].[dbo].indicator_strings indicator_strings
		on indicator_strings.indicator_id = indicatoren.indicator_id
		and indicator_strings.taal_id = @languageid

	left join [$(ActServer)].[$(Act)].[dbo].niveaus niveaus
		on niveaus.competentie_id = competenties.competentie_id
		and niveaus.woordenboek_id = 158
		and niveaus.niveau = indicatoren.niveau
	left join [$(ActServer)].[$(Act)].[dbo].niveaus_strings niveaus_strings
		on niveaus_strings.niveau_id = niveaus.niveau_id
		and niveaus_strings.taal_id = @languageid

	where w_samengesteld.woordenboek_id = 158

	order by w_samengesteld.cluster_id, w_samengesteld.competentie_id, w_samengesteld.indicator_id

INSERT INTO [DictionaryClusterTranslation]([Id], [LanguageId], [DictionaryClusterId], [Text], [Description])
	SELECT		NEWID(), @LanguageId, [DictionaryCluster].[Id], ISNULL([ClusterName], ''), [ClusterDefinition]
	FROM		[DictionaryCluster]
	INNER JOIN	(SELECT DISTINCT [ClusterName], [ClusterDefinition], ClusterLegacyId FROM @DictionaryTable) DictionaryTable ON DictionaryTable.ClusterLegacyId = [DictionaryCluster].[LegacyId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryCompetenceTranslation]([Id], [LanguageId], [DictionaryCompetenceId], [Text], [Description])
	SELECT		NEWID(), @LanguageId, [DictionaryCompetence].[Id], [CompetenceName], [CompetenceDefinition]
	FROM		[DictionaryCompetence]
	INNER JOIN	(SELECT DISTINCT [CompetenceName], [CompetenceDefinition], CompetenceLegacyId FROM @DictionaryTable) DictionaryTable ON DictionaryTable.CompetenceLegacyId = [DictionaryCompetence].[LegacyId]
	INNER JOIN	[DictionaryCluster] ON [DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryLevelTranslation]([Id], [LanguageId], [DictionaryLevelId], [Text])
	SELECT		NEWID(), @LanguageId, [DictionaryLevel].[Id], ''
	FROM		[DictionaryLevel]
	INNER JOIN	[DictionaryCompetence] ON [DictionaryCompetence].[Id] = [DictionaryLevel].[DictionaryCompetenceId]
	INNER JOIN	[DictionaryCluster] ON [DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryIndicatorTranslation]([Id], [LanguageId], [DictionaryIndicatorId], [Text])
	SELECT		NEWID(), @LanguageId, [DictionaryIndicator].[Id], ISNULL([Indicator], '')
	FROM		[DictionaryIndicator]
	INNER JOIN	(SELECT DISTINCT [Indicator], IndicatorLegacyId FROM @DictionaryTable) DictionaryTable ON DictionaryTable.IndicatorLegacyId = [DictionaryIndicator].[LegacyId]
	INNER JOIN	[DictionaryLevel] ON [DictionaryLevel].[Id] = [DictionaryIndicator].[DictionaryLevelId]
	INNER JOIN	[DictionaryCompetence] ON [DictionaryCompetence].[Id] = [DictionaryLevel].[DictionaryCompetenceId]
	INNER JOIN	[DictionaryCluster] ON [DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId
	
GO