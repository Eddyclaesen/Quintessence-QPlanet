CREATE TABLE [dbo].[ProjectCategoryDetail2SimulationCombination] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [ProjectCategoryDetailId] UNIQUEIDENTIFIER NOT NULL,
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ProjectCategoryDetail2SimulationCombination] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryDetail2SimulationCombination_ProjectCategoryDetail] FOREIGN KEY ([ProjectCategoryDetailId]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id]),
    CONSTRAINT [FK_ProjectCategoryDetail2SimulationCombination_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id])
);

