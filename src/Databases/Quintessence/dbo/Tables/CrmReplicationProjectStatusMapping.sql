CREATE TABLE [dbo].[CrmReplicationProjectStatusMapping] (
    [Id]          INT            NOT NULL,
    [SourceValue] NVARCHAR (MAX) NULL,
    [TargetId]    INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationProjectStatusMapping] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);

