CREATE TABLE [dbo].[ActivityDetailConsulting] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ActivityDetailConsulting] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityDetail_ActivityDetailConsulting] FOREIGN KEY ([Id]) REFERENCES [dbo].[ActivityDetail] ([Id])
);

