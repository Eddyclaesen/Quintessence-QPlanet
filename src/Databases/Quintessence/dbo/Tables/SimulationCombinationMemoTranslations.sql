CREATE TABLE [dbo].[SimulationCombinationMemoTranslations]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [SimulationCombinationMemoId] UNIQUEIDENTIFIER NOT NULL, 
    [LanguageId] INT NOT NULL, 
    [Title] NVARCHAR(255) NOT NULL,
    CONSTRAINT [PK_SimulationCombinationMemoTranslations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SimulationCombinationMemoTranslations_SimulationCombinationMemos] FOREIGN KEY ([SimulationCombinationMemoId]) REFERENCES [dbo].[SimulationCombinationMemos] ([Id]),
    CONSTRAINT [FK_SimulationCombinationMemoTranslations_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
