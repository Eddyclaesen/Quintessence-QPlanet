CREATE TABLE [dbo].[SimulationCombination2Language] (
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]              INT              NOT NULL,
    CONSTRAINT [PK_SimulationCombination2Language] PRIMARY KEY NONCLUSTERED ([SimulationCombinationId] ASC, [LanguageId] ASC),
    CONSTRAINT [FK_SimulationCombination2Language_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_SimulationCombination2Language_Simulation] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id])
);

