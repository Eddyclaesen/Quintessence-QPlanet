CREATE TABLE [dbo].[CrmReplicationSettings] (
    [Id]    INT            NOT NULL,
    [Key]   NVARCHAR (MAX) NOT NULL,
    [Value] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CrmReplicationSettings] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);

