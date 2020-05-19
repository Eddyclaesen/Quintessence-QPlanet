CREATE TABLE [dbo].[CrmReplicationUserGroup] (
    [Id]   INT            NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    [Rank] INT            NULL,
    CONSTRAINT [PK_CrmReplicationUserGroup] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);

