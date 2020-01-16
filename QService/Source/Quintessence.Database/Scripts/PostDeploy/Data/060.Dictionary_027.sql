GO

DECLARE @LanguageId AS INT = 3
DECLARE @ContactId AS INT
DECLARE @DictionaryLegacyId AS INT = 27
DECLARE @DictionaryTable AS TABLE(ClusterName NVARCHAR(MAX), ClusterDefinition NVARCHAR(MAX), CompetenceName NVARCHAR(MAX), CompetenceDefinition NVARCHAR(MAX), Level INT, LevelDefinition NVARCHAR(MAX), Indicator NVARCHAR(MAX), ClusterLegacyId INT, CompetenceLegacyId INT, LevelLegacyId INT, IndicatorLegacyId INT)

INSERT INTO @DictionaryTable
	select	cluster_strings.omschrijving		as	ClusterName, 
			cluster_strings.definitie			as	ClusterDefinition,
			competentie_strings.omschrijving	as	CompetenceName, 
			competentie_strings.definitie		as	CompetenceDefinition,
			0									as	Level, 
			''									as	LevelDefinition,
			indicator_strings.omschrijving		as	Indicator,
			clusters.cluster_id					as	ClusterLegacyId, 
			competenties.competentie_id			as	CompetenceLegacyId, 
			0									as	LevelLegacyId,
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

	where w_samengesteld.woordenboek_id = @DictionaryLegacyId

	order by w_samengesteld.cluster_id, w_samengesteld.competentie_id, w_samengesteld.indicator_id

SELECT @ContactId = contact_id FROM [$(ActServer)].[$(Act)].[dbo].woordenboek where woordenboek_id = @DictionaryLegacyId

DECLARE @DictionaryId AS UNIQUEIDENTIFIER = NEWID()
INSERT INTO [Dictionary]([Id], [ContactId], [Name], [Description], [Current], [LegacyId])
	SELECT	@DictionaryId, @ContactId, ActDictionary.[Omschrijving], NULL, 0, ActDictionary.[Woordenboek_id]
	FROM [$(ActServer)].[$(Act)].[dbo].[Woordenboek] ActDictionary
	WHERE ActDictionary.[Woordenboek_id] = @DictionaryLegacyId

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
	SELECT		NEWID(), [DictionaryCompetence].[Id], [Level], [LevelDefinition], [LevelLegacyId]
	FROM		(SELECT DISTINCT 0 AS Level, '' as LevelDefinition, [CompetenceLegacyId], 0 AS LevelLegacyId FROM @DictionaryTable) AS DictionaryTable
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[LegacyId] =  DictionaryTable.[CompetenceLegacyId]
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[Id] =  [DictionaryCompetence].[DictionaryClusterId]
		AND		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryIndicator]([Id], [DictionaryLevelId], [Name], [LegacyId])
	SELECT		NEWID(), [DictionaryLevel].[Id], [Indicator], [IndicatorLegacyId]
	FROM		(SELECT DISTINCT [Indicator], [IndicatorLegacyId], [CompetenceLegacyId], [ClusterLegacyId] FROM @DictionaryTable) AS DictionaryTable
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[DictionaryId] = @DictionaryId
		AND		[DictionaryCluster].[LegacyId] = DictionaryTable.[ClusterLegacyId]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[DictionaryClusterId] = [DictionaryCluster].[Id]
		AND		[DictionaryCompetence].[LegacyId] =  DictionaryTable.[CompetenceLegacyId]
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[DictionaryCompetenceId] = [DictionaryCompetence].[Id]

INSERT INTO [DictionaryClusterTranslation]([Id], [LanguageId], [DictionaryClusterId], [Text], [Description])
	SELECT		NEWID(), 3, [Id], [Name], [Description]
	FROM		[DictionaryCluster]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId	

INSERT INTO [DictionaryCompetenceTranslation]([Id], [LanguageId], [DictionaryCompetenceId], [Text], [Description])
	SELECT		NEWID(), 3, [DictionaryCompetence].[Id], [DictionaryCompetence].[Name], [DictionaryCompetence].[Description]
	FROM		[DictionaryCompetence]
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryLevelTranslation]([Id], [LanguageId], [DictionaryLevelId], [Text])
	SELECT		NEWID(), 3, [DictionaryLevel].[Id], [DictionaryLevel].[Name]
	FROM		[DictionaryLevel]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[Id] = [DictionaryLevel].[DictionaryCompetenceId]
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId

INSERT INTO [DictionaryIndicatorTranslation]([Id], [LanguageId], [DictionaryIndicatorId], [Text])
	SELECT		NEWID(), 3, [DictionaryIndicator].[Id], [DictionaryIndicator].[Name]
	FROM		[DictionaryIndicator]
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[Id] = [DictionaryIndicator].[DictionaryLevelId]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[Id] = [DictionaryLevel].[DictionaryCompetenceId]
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
	WHERE		[DictionaryCluster].[DictionaryId] = @DictionaryId