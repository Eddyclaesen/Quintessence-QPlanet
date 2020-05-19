CREATE TABLE [dbo].[ActivityDetailSupport] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ActivityDetailSupport] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityDetail_ActivityDetailSupport] FOREIGN KEY ([Id]) REFERENCES [dbo].[ActivityDetail] ([Id])
);

