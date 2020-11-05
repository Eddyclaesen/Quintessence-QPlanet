CREATE TABLE [dbo].[SimulationCombinationMemo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL, 
    [Position] INT NOT NULL,
    CONSTRAINT [FK_SimulationCombinationMemos_Simulation] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id]),
    INDEX [IX_SimulationCombination_Position] CLUSTERED ([SimulationCombinationId],[Position])
)
