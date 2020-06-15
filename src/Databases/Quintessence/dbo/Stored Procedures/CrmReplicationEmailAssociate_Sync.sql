CREATE PROCEDURE [dbo].[CrmReplicationEmailAssociate_Sync]
AS
BEGIN
	DECLARE @JobHistoryId AS UNIQUEIDENTIFIER
	DECLARE @JobName AS NVARCHAR(MAX)

	SELECT	@JobName = OBJECT_NAME(@@PROCID)

	EXEC [dbo].[CrmReplicationJob_StartProcess] @JobName, @JobHistoryId OUTPUT

	DECLARE	@EmailAssociate	AS	TABLE(								
								[Id]					INT			NOT NULL,
								[AssociateId]			INT,
								[Email]					NVARCHAR(MAX),
								[Rank]					INT)

	INSERT INTO @EmailAssociate(	[Id],
									[AssociateId],
									[Email],
									[Rank])
	SELECT		Email.[email_id]			AS	[Id],
				Associate.[associate_id]	AS	[AssociateId],
				Email.[email_address]		AS	[Address],
				Email.[rank]				AS	[Rank]

	FROM		[Superoffice7].[CRM7].[EMAIL] Email	WITH (NOLOCK)

	INNER JOIN	[Superoffice7].[CRM7].[ASSOCIATE] Associate	WITH (NOLOCK)
		ON		Associate.[person_id] = Email.[person_id]

	WHERE		Associate.group_idx <> 0
		
	MERGE [CrmReplicationEmailAssociate] AS [TargetDatabase]
	USING @EmailAssociate AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id] AND [TargetDatabase].[AssociateId] = [SourceDatabase].[AssociateId]
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[Email] =				[SourceDatabase].[Email],
					[TargetDatabase].[AssociateId] =		[SourceDatabase].[AssociateId],
					[TargetDatabase].[Rank] =				[SourceDatabase].[Rank]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[AssociateId],
				[Email],
				[Rank]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[AssociateId],
				[SourceDatabase].[Email],
				[SourceDatabase].[Rank]);

	EXEC [dbo].[CrmReplicationJob_StopProcess] @JobHistoryId
END