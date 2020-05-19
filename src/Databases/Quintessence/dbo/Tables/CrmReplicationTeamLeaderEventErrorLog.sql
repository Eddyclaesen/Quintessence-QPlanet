CREATE TABLE [dbo].[CrmReplicationTeamLeaderEventErrorLog] (
    [Id]                              INT            IDENTITY (1, 1) NOT NULL,
    [CrmReplicationTeamLeaderEventId] INT            NOT NULL,
    [LogDateUtc]                      DATETIME       NOT NULL,
    [Info]                            NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CrmReplicationTeamLeaderEventErrorLog] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);

