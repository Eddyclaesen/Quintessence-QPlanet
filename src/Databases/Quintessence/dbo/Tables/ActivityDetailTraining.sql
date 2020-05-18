CREATE TABLE [dbo].[ActivityDetailTraining] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [TargetGroup]   TEXT             NULL,
    [Duration]      TEXT             NULL,
    [ExtraInfo]     TEXT             NULL,
    [ChecklistLink] TEXT             NULL,
    [Code]          NVARCHAR (12)    NULL,
    CONSTRAINT [PK_ActivityDetailTraining] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityDetail_ActivityDetailTraining] FOREIGN KEY ([Id]) REFERENCES [dbo].[ActivityDetail] ([Id])
);

