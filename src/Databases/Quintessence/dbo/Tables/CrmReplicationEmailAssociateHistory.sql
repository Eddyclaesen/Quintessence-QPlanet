CREATE TABLE [dbo].[CrmReplicationEmailAssociateHistory] (
    [CrmReplicationEmailAssociateHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                                    INT            NOT NULL,
    [AssociateId]                           INT            NULL,
    [Email]                                 NVARCHAR (MAX) NULL,
    [Rank]                                  INT            NULL,
    [LastSyncedUtc]                         DATETIME       NULL,
    [SyncVersion]                           INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationEmailAssociateHistory] PRIMARY KEY CLUSTERED ([CrmReplicationEmailAssociateHistoryId] ASC) WITH (FILLFACTOR = 90)
);

