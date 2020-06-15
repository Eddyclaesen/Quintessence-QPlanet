CREATE TABLE [dbo].[CrmReplicationContact] (
    [Id]                  INT            NOT NULL,
    [Name]                NVARCHAR (MAX) NULL,
    [Department]          NVARCHAR (MAX) NULL,
    [AssociateId]         INT            NULL,
    [AccountManagerId]    INT            NULL,
    [CustomerAssistantId] INT            NULL,
    [TeamLeaderId]        INT            NULL,
    [LastSyncedUtc]       DATETIME       NULL,
    [SyncVersion]         INT            NOT NULL,
    [SuperOfficeId]       INT            NULL,
    CONSTRAINT [PK_CrmReplicationContact] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


GO

CREATE TRIGGER [dbo].[History_CrmReplicationContact] ON [dbo].[CrmReplicationContact] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO [dbo].[CrmReplicationContactHistory]
           ([Id]
           ,[Name]
           ,[Department]
           ,[AssociateId]
           ,[AccountManagerId]
           ,[CustomerAssistantId]
           ,[TeamLeaderId]
	   ,[SuperOfficeId]
           ,[LastSyncedUtc]
           ,[SyncVersion]
		   )
SELECT d.Id,  d.Name, d.Department, d.AssociateId, d.AccountManagerId, d.CustomerAssistantId, d.TeamLeaderId, d.SuperOfficeId, d.LastSyncedUtc, d.SyncVersion
  FROM Deleted d 

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END

