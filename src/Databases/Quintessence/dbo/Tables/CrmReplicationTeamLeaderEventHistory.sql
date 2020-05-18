CREATE TABLE [dbo].[CrmReplicationTeamLeaderEventHistory] (
    [CrmReplicationTeamLeaderEventHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                                     INT            NOT NULL,
    [EventType]                              NVARCHAR (MAX) NULL,
    [ObjectType]                             NVARCHAR (MAX) NULL,
    [ObjectId]                               NVARCHAR (MAX) NULL,
    [Source]                                 NVARCHAR (10)  NOT NULL,
    [ReceivedUtc]                            DATETIME       NOT NULL,
    [ProcessCount]                           INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationTeamLeaderEventHistory] PRIMARY KEY CLUSTERED ([CrmReplicationTeamLeaderEventHistoryId] ASC) WITH (FILLFACTOR = 90)
);

