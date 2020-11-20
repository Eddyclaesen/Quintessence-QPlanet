CREATE TABLE [dbo].[SimulationCombinationMemos]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL, 
    [Position] INT NOT NULL,
    CONSTRAINT [PK_SimulationCombinationMemos] PRIMARY KEY NONCLUSTERED ([Id]),
    CONSTRAINT [FK_SimulationCombinationMemos_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id])
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_SimulationCombinationMemos_SimulationCombinationId_Position ON [dbo].[SimulationCombinationMemos]
    (
    [SimulationCombinationId],
    [Position]
    )
GO


