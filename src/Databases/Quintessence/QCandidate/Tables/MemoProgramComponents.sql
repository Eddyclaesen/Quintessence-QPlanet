CREATE TABLE [QCandidate].[MemoProgramComponents]
(
	[Id]                        UNIQUEIDENTIFIER    NOT NULL, 
    [SimulationCombinationId]   UNIQUEIDENTIFIER    NOT NULL,
    [UserId]                    UNIQUEIDENTIFIER    NOT NULL, 
    [CreatedBy]                 NVARCHAR (250)      NOT NULL,
    [CreatedOn]                 DATETIME2           NOT NULL,
    [ModifiedBy]                NVARCHAR (250)      NULL,
    [ModifiedOn]                DATETIME2           NULL,
    [ConcurrencyLock]           TIMESTAMP           NOT NULL,
    CONSTRAINT [PK_MemoProgramComponents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MemoProgramComponents_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id])
)
