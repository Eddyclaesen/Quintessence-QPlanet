CREATE TABLE [dbo].[SimulationCombinationMemoTranslations]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [SimulationCombinationMemoId] UNIQUEIDENTIFIER NULL, 
    [LanguageId] INT NULL, 
    [Title] NVARCHAR(255) NULL,
    CONSTRAINT [PK_SimulationCombinationMemoTranslations] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SimulationCombinationMemoTranslations_SimulationCombinationMemos] FOREIGN KEY ([SimulationCombinationMemoId]) REFERENCES [dbo].[SimulationCombinationMemos] ([Id]),
    CONSTRAINT [FK_SimulationCombinationMemoTranslations_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
