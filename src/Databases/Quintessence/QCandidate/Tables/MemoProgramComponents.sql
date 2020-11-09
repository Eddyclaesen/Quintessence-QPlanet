CREATE TABLE [QCandidate].[MemoProgramComponents]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]        NVARCHAR (MAX)    NOT NULL,
    [CreatedOn]        DATETIME          NOT NULL,
    [ModifiedBy]       NVARCHAR (MAX)   NULL,
    [ModifiedOn]       DATETIME         NULL,
    [ConcurrencyLock]        TIMESTAMP  NOT NULL,
    CONSTRAINT [PK_MemoProgramComponents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MemoProgramComponents_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id])
)
