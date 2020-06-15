CREATE TABLE [dbo].[ConsultancyProject] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ProjectInformation] TEXT             NULL,
    [ProjectPlanId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ConsultancyProject] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsultancyProject_Project] FOREIGN KEY ([Id]) REFERENCES [dbo].[Project] ([Id]),
    CONSTRAINT [FK_ConsultancyProject_ProjectPlan] FOREIGN KEY ([ProjectPlanId]) REFERENCES [dbo].[ProjectPlan] ([Id])
);

