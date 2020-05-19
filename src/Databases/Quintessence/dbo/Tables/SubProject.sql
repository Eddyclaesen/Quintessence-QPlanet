CREATE TABLE [dbo].[SubProject] (
    [ProjectId]          UNIQUEIDENTIFIER NOT NULL,
    [SubProjectId]       UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SubProject] PRIMARY KEY NONCLUSTERED ([ProjectId] ASC, [SubProjectId] ASC),
    CONSTRAINT [FK_SubProject_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
    CONSTRAINT [FK_SubProject_SubProject] FOREIGN KEY ([SubProjectId]) REFERENCES [dbo].[Project] ([Id])
);

