CREATE TABLE [dbo].[CrmReplicationEmailHistory] (
    [CrmReplicationEmailHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                           INT            NOT NULL,
    [PersonId]                     INT            NULL,
    [ContactId]                    INT            NULL,
    [ContactName]                  NVARCHAR (MAX) NULL,
    [FirstName]                    NVARCHAR (MAX) NULL,
    [LastName]                     NVARCHAR (MAX) NULL,
    [Email]                        NVARCHAR (MAX) NULL,
    [DirectPhone]                  NVARCHAR (MAX) NULL,
    [MobilePhone]                  NVARCHAR (MAX) NULL,
    [LastSyncedUtc]                DATETIME       NULL,
    [SyncVersion]                  INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationEmailHistory] PRIMARY KEY CLUSTERED ([CrmReplicationEmailHistoryId] ASC) WITH (FILLFACTOR = 90)
);

