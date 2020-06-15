CREATE TABLE [dbo].[CrmReplicationProjectHistory] (
    [CrmReplicationProjectHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                             INT            NOT NULL,
    [Name]                           NVARCHAR (MAX) NULL,
    [AssociateId]                    INT            NULL,
    [ContactId]                      INT            NULL,
    [ProjectStatusId]                INT            NULL,
    [StartDate]                      DATETIME       NULL,
    [BookyearFrom]                   DATETIME       NULL,
    [BookyearTo]                     DATETIME       NULL,
    [TeamLeaderId]                   INT            NULL,
    [LastSyncedUtc]                  DATETIME       NULL,
    [SyncVersion]                    INT            NOT NULL,
    [SuperOfficeId]                  INT            NULL,
    CONSTRAINT [PK_CrmReplicationProjectHistory] PRIMARY KEY CLUSTERED ([CrmReplicationProjectHistoryId] ASC) WITH (FILLFACTOR = 90)
);

