CREATE TABLE [dbo].[CrmReplicationAssociateHistory] (
    [CrmReplicationAssociateHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                               INT            NOT NULL,
    [UserName]                         NVARCHAR (MAX) NULL,
    [FirstName]                        NVARCHAR (MAX) NULL,
    [LastName]                         NVARCHAR (MAX) NULL,
    [UserGroupId]                      INT            NULL,
    [TeamLeaderName]                   NVARCHAR (MAX) NULL,
    [TeamLeaderId]                     INT            NULL,
    [LastSyncedUtc]                    DATETIME       NULL,
    [SyncVersion]                      INT            NOT NULL,
    [SuperOfficeId]                    INT            NULL,
    CONSTRAINT [PK_CrmReplicationAssociateHistory] PRIMARY KEY CLUSTERED ([CrmReplicationAssociateHistoryId] ASC) WITH (FILLFACTOR = 90)
);

