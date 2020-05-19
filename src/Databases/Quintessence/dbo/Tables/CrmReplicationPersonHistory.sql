CREATE TABLE [dbo].[CrmReplicationPersonHistory] (
    [CrmReplicationPersonHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                            INT            NOT NULL,
    [ContactId]                     INT            NULL,
    [FirstName]                     NVARCHAR (MAX) NULL,
    [LastName]                      NVARCHAR (MAX) NULL,
    [Title]                         NVARCHAR (MAX) NULL,
    [IsRetired]                     BIT            NULL,
    [TeamLeaderId]                  INT            NULL,
    [LastSyncedUtc]                 DATETIME       NULL,
    [SyncVersion]                   INT            NOT NULL,
    [SuperOfficeId]                 INT            NULL,
    CONSTRAINT [PK_CrmReplicationPersonHistory] PRIMARY KEY CLUSTERED ([CrmReplicationPersonHistoryId] ASC) WITH (FILLFACTOR = 90)
);

