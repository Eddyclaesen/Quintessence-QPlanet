CREATE TABLE [dbo].[CrmReplicationSuperOfficeEventErrorLog] (
    [Id]                               INT            IDENTITY (1, 1) NOT NULL,
    [CrmReplicationSuperOfficeEventId] INT            NOT NULL,
    [LogDateUtc]                       DATETIME       NOT NULL,
    [Info]                             NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CrmReplicationSuperOfficeEventErrorLog] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);

