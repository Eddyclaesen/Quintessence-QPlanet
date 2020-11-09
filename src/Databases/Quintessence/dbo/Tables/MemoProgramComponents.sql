CREATE TABLE [dbo].[MemoProgramComponents]
(
	[Id] INT NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_MemoProgramComponent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MemoProgramComponent_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_MemoProgramComponent_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id])
)
