CREATE TABLE [dbo].[ActivityDetail] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Description] TEXT             NULL,
    CONSTRAINT [PK_ActivityDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Activity_ActivityDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[Activity] ([Id])
);

