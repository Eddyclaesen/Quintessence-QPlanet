CREATE TABLE [dbo].[CrmReplicationJob] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (200)   NOT NULL,
    [Description] TEXT             NULL,
    CONSTRAINT [PK_CrmReplicationJob] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);

