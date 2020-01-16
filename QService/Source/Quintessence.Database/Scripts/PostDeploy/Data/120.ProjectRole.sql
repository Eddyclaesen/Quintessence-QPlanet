/*QUINTESSENCE ROLES*/
DECLARE	@OperationalLg AS UNIQUEIDENTIFIER = 'B5C000D4-3F84-40DE-92A7-B71C7D3E0903'
DECLARE	@KaderLg AS UNIQUEIDENTIFIER = 'AF739B12-52BE-46FD-A7D0-DD4078827F40'
DECLARE	@Projectmanagement AS UNIQUEIDENTIFIER = '32D81830-E2FD-49B6-8E4E-A22B2B9F38F0'
DECLARE	@Operationeel AS UNIQUEIDENTIFIER = '4E08F30C-684C-4CC9-B8F9-916011E7FC2A'
DECLARE	@KaderSpecialisten AS UNIQUEIDENTIFIER = 'F2C0DC85-70BD-4AAB-A533-F7AECC6F3603'

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@OperationalLg, 'Operationeel lg', null)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @OperationalLg, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 1

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@KaderLg, 'Kader/Lg', null)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @KaderLg, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 2

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@Projectmanagement, 'Projectmanagement', null)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @Projectmanagement, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 3

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@Operationeel, 'Operationeel', null)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @Operationeel, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 4

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@KaderSpecialisten, 'Kader/Specialisten', null)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @KaderSpecialisten, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 5

/*CUSTOMER SPECIFIC ROLES*/
DECLARE	@MakroOperationalLg UNIQUEIDENTIFIER = NEWID()
DECLARE	@MakroFirstLine UNIQUEIDENTIFIER = NEWID()
DECLARE	@MakroKaderSpecialisten UNIQUEIDENTIFIER = NEWID()
DECLARE	@MakroKaderLg UNIQUEIDENTIFIER = NEWID()
DECLARE	@MakroProjectmanagement UNIQUEIDENTIFIER = NEWID()

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@MakroOperationalLg, 'Operationeel uitvoerend', 3836)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @MakroOperationalLg, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 6

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@MakroFirstLine, 'Eerstelijns leidinggevende', 3836)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @MakroFirstLine, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 7

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@MakroKaderSpecialisten, 'Kader – specialist', 3836)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @MakroKaderSpecialisten, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 8

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@MakroKaderLg, 'Leidinggevend kader', 3836)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @MakroKaderLg, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 9

INSERT INTO ProjectRole (Id, Name, ContactId)
VALUES (@MakroProjectmanagement, 'Projectmanagement', 3836)

INSERT INTO [ProjectRoleTranslation]([Id], [ProjectRoleId], [LanguageId], [Text])
	SELECT	NEWID(), @MakroProjectmanagement, [Translation].[Taal_ID], [Translation].[Reikwijdte]
	FROM	[$(ActServer)].[$(Act)].[dbo].[FA_Reikwijdte] [Translation]
	WHERE	[Translation].[FA_ProjectType] = 10


--Link Dictionary Levels with the roles
---------------------------------------

DECLARE @Qwb2012 AS UNIQUEIDENTIFIER
SELECT	@Qwb2012 = [Id] FROM [Dictionary] WHERE [Name] = 'Q WB 2012'

--587	Convincing power
--597	Judgement
--601	Initiative
--603	Flexibility
--611	Planning and organising
--615	Providing direction

DECLARE	@DistinctIndicatorTable AS TABLE(LegacyId INT)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(10034)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(10033)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(10032)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(10040)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(10038)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(10043)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(10049)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(10051)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9840)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9837)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9845)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9846)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9849)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9850)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9812)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9816)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9819)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9955)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9960)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9961)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9956)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9970)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9964)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9966)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9972)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9973)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9976)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9547)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9548)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9557)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9552)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9560)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9561)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9756)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9749)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9753)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9757)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9762)
INSERT INTO @DistinctIndicatorTable(LegacyId) VALUES(9764)


--Operationeel lg
INSERT INTO	[ProjectRole2DictionaryIndicator]([ProjectRoleId], [DictionaryIndicatorId], [Norm])
	SELECT		@OperationalLg, [DictionaryIndicator].[Id], CASE WHEN [DictionaryIndicator].[LegacyId] IN (SELECT LegacyId FROM @DistinctIndicatorTable) THEN 20 ELSE 10 END
	FROM		[DictionaryCluster]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[DictionaryClusterId] = [DictionaryCluster].[Id]
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[DictionaryCompetenceId] = [DictionaryCompetence].[Id]
	INNER JOIN	[DictionaryIndicator]
		ON		[DictionaryIndicator].[DictionaryLevelId] = [DictionaryLevel].[Id]
	WHERE		[DictionaryCluster].[DictionaryId] = @Qwb2012
		AND		(
					([DictionaryCompetence].[LegacyId] IN (603, 601, 615) AND [DictionaryLevel].[Level] = 1)
				OR	([DictionaryCompetence].[LegacyId] IN (611, 587) AND [DictionaryLevel].[Level] = 2)
				)

--Kader/Lg
INSERT INTO	[ProjectRole2DictionaryIndicator]([ProjectRoleId], [DictionaryIndicatorId], [Norm])
	SELECT		@KaderLg, [DictionaryIndicator].[Id], CASE WHEN [DictionaryIndicator].[LegacyId] IN (SELECT LegacyId FROM @DistinctIndicatorTable) THEN 20 ELSE 10 END
	FROM		[DictionaryCluster]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[DictionaryClusterId] = [DictionaryCluster].[Id]
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[DictionaryCompetenceId] = [DictionaryCompetence].[Id]
	INNER JOIN	[DictionaryIndicator]
		ON		[DictionaryIndicator].[DictionaryLevelId] = [DictionaryLevel].[Id]
	WHERE		[DictionaryCluster].[DictionaryId] = @Qwb2012
		AND		[DictionaryCompetence].[LegacyId] IN (587, 601, 603, 611, 615) 
		AND		[DictionaryLevel].[Level] = 2

--Projectmanagement
INSERT INTO	[ProjectRole2DictionaryIndicator]([ProjectRoleId], [DictionaryIndicatorId], [Norm])
	SELECT		@Projectmanagement, [DictionaryIndicator].[Id], CASE WHEN [DictionaryIndicator].[LegacyId] IN (SELECT LegacyId FROM @DistinctIndicatorTable) THEN 20 ELSE 10 END
	FROM		[DictionaryCluster]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[DictionaryClusterId] = [DictionaryCluster].[Id]
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[DictionaryCompetenceId] = [DictionaryCompetence].[Id]
	INNER JOIN	[DictionaryIndicator]
		ON		[DictionaryIndicator].[DictionaryLevelId] = [DictionaryLevel].[Id]
	WHERE		[DictionaryCluster].[DictionaryId] = @Qwb2012
		AND		(
					([DictionaryCompetence].[LegacyId] IN (615) AND [DictionaryLevel].[Level] = 1)
				OR	([DictionaryCompetence].[LegacyId] IN (603, 601, 587) AND [DictionaryLevel].[Level] = 2)
				OR	([DictionaryCompetence].[LegacyId] IN (611) AND [DictionaryLevel].[Level] = 3)
				)

--Operationeel
INSERT INTO	[ProjectRole2DictionaryIndicator]([ProjectRoleId], [DictionaryIndicatorId], [Norm])
	SELECT		@Operationeel, [DictionaryIndicator].[Id], CASE WHEN [DictionaryIndicator].[LegacyId] IN (SELECT LegacyId FROM @DistinctIndicatorTable) THEN 20 ELSE 10 END
	FROM		[DictionaryCluster]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[DictionaryClusterId] = [DictionaryCluster].[Id]
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[DictionaryCompetenceId] = [DictionaryCompetence].[Id]
	INNER JOIN	[DictionaryIndicator]
		ON		[DictionaryIndicator].[DictionaryLevelId] = [DictionaryLevel].[Id]
	WHERE		[DictionaryCluster].[DictionaryId] = @Qwb2012
		AND		[DictionaryCompetence].[LegacyId] IN (587, 601, 603, 611, 597) 
		AND		[DictionaryLevel].[Level] = 1

--Kader/Specialisten
INSERT INTO	[ProjectRole2DictionaryIndicator]([ProjectRoleId], [DictionaryIndicatorId], [Norm])
	SELECT		@KaderSpecialisten, [DictionaryIndicator].[Id], CASE WHEN [DictionaryIndicator].[LegacyId] IN (SELECT LegacyId FROM @DistinctIndicatorTable) THEN 20 ELSE 10 END
	FROM		[DictionaryCluster]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[DictionaryClusterId] = [DictionaryCluster].[Id]
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[DictionaryCompetenceId] = [DictionaryCompetence].[Id]
	INNER JOIN	[DictionaryIndicator]
		ON		[DictionaryIndicator].[DictionaryLevelId] = [DictionaryLevel].[Id]
	WHERE		[DictionaryCluster].[DictionaryId] = @Qwb2012
		AND		(
					([DictionaryCompetence].[LegacyId] IN (587, 601, 603, 611) AND [DictionaryLevel].[Level] = 2)
				OR	([DictionaryCompetence].[LegacyId] IN (597) AND [DictionaryLevel].[Level] = 3)
				)


INSERT INTO [ProjectRole2DictionaryLevel]([ProjectRoleId], [DictionaryLevelId])
	SELECT		DISTINCT [ProjectRole2DictionaryIndicator].[ProjectRoleId], [DictionaryIndicator].[DictionaryLevelId]
	FROM		[ProjectRole2DictionaryIndicator]
	INNER JOIN	[DictionaryIndicator]
		ON		[DictionaryIndicator].[Id] = [ProjectRole2DictionaryIndicator].[DictionaryIndicatorId]	