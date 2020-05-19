CREATE TABLE [dbo].[ActivityDetailWorkshop] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [TargetGroup] TEXT             NULL,
    CONSTRAINT [PK_ActivityDetailWorkshop] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityDetail_ActivityDetailWorkshop] FOREIGN KEY ([Id]) REFERENCES [dbo].[ActivityDetail] ([Id])
);

