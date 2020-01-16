CREATE PROCEDURE [dbo].[CrmReplicationContact_Sync]
AS
BEGIN
	DECLARE @JobHistoryId AS UNIQUEIDENTIFIER
	DECLARE @JobName AS NVARCHAR(MAX)

	SELECT	@JobName = OBJECT_NAME(@@PROCID)

	EXEC [dbo].[CrmReplicationJob_StartProcess] @JobName, @JobHistoryId OUTPUT

	DECLARE	@Contact	AS	TABLE(								
								[Id]							INT			NOT NULL,
								[Name]							NVARCHAR(MAX),
								[Department]					NVARCHAR(MAX),
								[AssociateId]					INT,
								[AccountManagerId]				INT,
								[CustomerAssistantId]			INT)

	INSERT INTO @Contact(	[Id],
							[Name],
							[Department],
							[AssociateId],
							[AccountManagerId],
							[CustomerAssistantId])
		SELECT		CONTACT_1.contact_id AS Id, 
					CONTACT_1.name, 
					CONTACT_1.department,
					CONTACT_1.associate_id AS AssociateId, 
					cs.long08 AS AccountManagerId, 
					ka.Klantassistente AS CustomerAssistantId
		FROM        [$(superoffice7server)].[$(SuperOffice7)].dbo.CONTACT AS CONTACT_1 
		LEFT JOIN	[$(superoffice7server)].[$(SuperOffice7)].dbo.UDCONTACTSMALL AS cs 
			ON		cs.udcontactSmall_id = CONTACT_1.contact_id 
		LEFT JOIN	[$(superoffice7server)].[$(SuperOffice7)].dbo.vw_Klanten_ptool AS ka 
			ON		ka.[ID Klant] = CONTACT_1.contact_id
		WHERE		(CONTACT_1.deleted = 0)
		
	MERGE [CrmReplicationContact] AS [TargetDatabase]
	USING @Contact AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]	
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[Name] =					[SourceDatabase].[Name],
					[TargetDatabase].[Department] =				[SourceDatabase].[Department],
					[TargetDatabase].[AssociateId] =			[SourceDatabase].[AssociateId],
					[TargetDatabase].[AccountManagerId] =		[SourceDatabase].[AccountManagerId],
					[TargetDatabase].[CustomerAssistantId] =	[SourceDatabase].[CustomerAssistantId]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[Name],
				[Department],
				[AssociateId],
				[AccountManagerId],
				[CustomerAssistantId]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[Name],
				[SourceDatabase].[Department],
				[SourceDatabase].[AssociateId],
				[SourceDatabase].[AccountManagerId],
				[SourceDatabase].[CustomerAssistantId]);

	EXEC [dbo].[CrmReplicationJob_StopProcess] @JobHistoryId
END
GO