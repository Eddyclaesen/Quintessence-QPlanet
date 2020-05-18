CREATE TABLE [dbo].[ProjectPlanPhaseActivity] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ActivityId] UNIQUEIDENTIFIER NOT NULL,
    [ProfileId]  UNIQUEIDENTIFIER NOT NULL,
    [Duration]   DECIMAL (18, 2)  NOT NULL,
    [Notes]      NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ProjectPlanPhaseActivity] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectPlanPhaseActivity_ProjectPlanPhaseEntry] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectPlanPhaseEntry] ([Id])
);

