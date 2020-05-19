CREATE TABLE [dbo].[ProjectCategoryDetail2Competence2Combination] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [ProjectCategoryDetailId] UNIQUEIDENTIFIER NOT NULL,
    [DictionaryCompetenceId]  UNIQUEIDENTIFIER NOT NULL,
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ProjectCategoryDetail2Competence2Combination] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryDetail2C2C_DictionaryCompetence] FOREIGN KEY ([DictionaryCompetenceId]) REFERENCES [dbo].[DictionaryCompetence] ([Id]),
    CONSTRAINT [FK_ProjectCategoryDetail2C2C_ProjectCategoryDetail] FOREIGN KEY ([ProjectCategoryDetailId]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id]),
    CONSTRAINT [FK_ProjectCategoryDetail2C2C_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id])
);

