CREATE TABLE [dbo].[CrmReplicationContactHistory] (
    [CrmReplicationContactHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                             INT            NOT NULL,
    [Name]                           NVARCHAR (MAX) NULL,
    [Department]                     NVARCHAR (MAX) NULL,
    [AssociateId]                    INT            NULL,
    [AccountManagerId]               INT            NULL,
    [CustomerAssistantId]            INT            NULL,
    [TeamLeaderId]                   INT            NULL,
    [LastSyncedUtc]                  DATETIME       NULL,
    [SyncVersion]                    INT            NOT NULL,
    [SuperOfficeId]                  INT            NULL,
    CONSTRAINT [PK_CrmReplicationContactHistory] PRIMARY KEY CLUSTERED ([CrmReplicationContactHistoryId] ASC) WITH (FILLFACTOR = 90)
);

