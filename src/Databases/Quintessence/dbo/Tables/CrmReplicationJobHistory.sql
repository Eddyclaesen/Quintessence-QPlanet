CREATE TABLE [dbo].[CrmReplicationJobHistory] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [CrmReplicationJobId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate]           DATETIME         NOT NULL,
    [EndDate]             DATETIME         NULL,
    [Succeeded]           BIT              NULL,
    [Info]                NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_CrmReplicationJobHistory] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);

