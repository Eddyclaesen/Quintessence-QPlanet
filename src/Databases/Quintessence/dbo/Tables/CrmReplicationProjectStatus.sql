CREATE TABLE [dbo].[CrmReplicationProjectStatus] (
    [Id]   INT            NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CrmReplicationProjectStatus] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);

