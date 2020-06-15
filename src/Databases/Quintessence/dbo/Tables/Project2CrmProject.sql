CREATE TABLE [dbo].[Project2CrmProject] (
    [ProjectId]    UNIQUEIDENTIFIER NOT NULL,
    [CrmProjectId] INT              NOT NULL,
    CONSTRAINT [PK_Project2CrmProject] PRIMARY KEY NONCLUSTERED ([ProjectId] ASC, [CrmProjectId] ASC),
    CONSTRAINT [FK_Project2CrmProject_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

