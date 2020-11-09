CREATE TABLE [QCandidate].[MemoProgramComponents]
(
	[Id] INT NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]        NVARCHAR (MAX)    NOT NULL,
    [CreatedOn]        DATETIME          NOT NULL,
    [ModifiedBy]       NVARCHAR (MAX)   NULL,
    [ModifiedOn]       DATETIME         NULL,
    [ConcurrencyLock]        TIMESTAMP  NOT NULL,
    CONSTRAINT [PK_MemoProgramComponent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MemoProgramComponent_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id])
)
