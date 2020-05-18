CREATE TABLE [dbo].[ActivityDetailCoaching] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [TargetGroup]     TEXT             NULL,
    [SessionQuantity] INT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ActivityDetailCoaching] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityDetail_ActivityDetailCoaching] FOREIGN KEY ([Id]) REFERENCES [dbo].[ActivityDetail] ([Id])
);

