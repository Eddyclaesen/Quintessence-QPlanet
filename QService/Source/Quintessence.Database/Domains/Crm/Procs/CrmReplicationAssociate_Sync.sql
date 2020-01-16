CREATE PROCEDURE [dbo].[CrmReplicationAssociate_Sync]
AS
BEGIN
	DECLARE @JobHistoryId AS UNIQUEIDENTIFIER
	DECLARE @JobName AS NVARCHAR(MAX)

	SELECT	@JobName = OBJECT_NAME(@@PROCID)

	EXEC [dbo].[CrmReplicationJob_StartProcess] @JobName, @JobHistoryId OUTPUT

	--Associates
	DECLARE	@Associate	AS	TABLE(								
								[Id]						INT			NOT NULL,
								[UserName]					NVARCHAR(MAX),
								[FirstName]					NVARCHAR(MAX),
								[LastName]					NVARCHAR(MAX),
								[UserGroupId]				INT)

	INSERT INTO @Associate(	[Id],
							[UserName],
							[FirstName],
							[LastName],
							[UserGroupId])
		SELECT		a.associate_id AS Id, 
					a.name AS UserName, 
					p.firstname, 
					p.lastname, 
					a.group_idx AS UserGroupId
		FROM        [$(superoffice7server)].[$(SuperOffice7)].dbo.ASSOCIATE AS a 
		
		INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].dbo.PERSON AS p 
			ON		p.person_id = a.person_id

		WHERE		(a.deleted = 0)
		
	MERGE [CrmReplicationAssociate] AS [TargetDatabase]
	USING @Associate AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]	
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[UserName] =			[SourceDatabase].[UserName],
					[TargetDatabase].[FirstName] =			[SourceDatabase].[FirstName],
					[TargetDatabase].[LastName] =			[SourceDatabase].[LastName],
					[TargetDatabase].[UserGroupId] =		[SourceDatabase].[UserGroupId]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[UserName],
				[FirstName],
				[LastName],
				[UserGroupId]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[UserName],
				[SourceDatabase].[FirstName],
				[SourceDatabase].[LastName],
				[SourceDatabase].[UserGroupId]);


	--Usergroups
	DECLARE	@UserGroup	AS	TABLE(								
								[Id]						INT			NOT NULL,
								[Name]						NVARCHAR(MAX),
								[Rank]						INT)

	INSERT INTO @UserGroup(	[Id],
							[Name],
							[Rank])
		SELECT		UserGroup_id AS Id, name AS Name, [rank] AS [Rank]
		FROM		[$(superoffice7server)].[$(SuperOffice7)].dbo.USERGROUP
		WHERE		(deleted = 0)
		
	MERGE [CrmReplicationUserGroup] AS [TargetDatabase]
	USING @UserGroup AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]	
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[Name] =			[SourceDatabase].[Name],
					[TargetDatabase].[Rank] =			[SourceDatabase].[Rank]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[Name],
				[Rank]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[Name],
				[SourceDatabase].[Rank]);

	EXEC [dbo].[CrmReplicationJob_StopProcess] @JobHistoryId
END
GO