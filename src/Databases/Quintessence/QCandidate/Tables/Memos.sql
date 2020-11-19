CREATE TABLE [QCandidate].[Memos]
(
	[Id]                        UNIQUEIDENTIFIER    NOT NULL, 
    [MemoProgramComponentId]    UNIQUEIDENTIFIER    NOT NULL, 
    [Position]                  INT                 NOT NULL, 
    [OriginId]                  UNIQUEIDENTIFIER    NOT NULL,
    [CreatedBy]                 NVARCHAR (250)      NOT NULL,
    [CreatedOn]                 DATETIME2           NOT NULL,
    [ModifiedBy]                NVARCHAR (250)      NULL,
    [ModifiedOn]                DATETIME2           NULL,
    [ConcurrencyLock]           TIMESTAMP           NOT NULL,
    CONSTRAINT [PK_Memos] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Memos_MemoProgramComponents] FOREIGN KEY ([MemoProgramComponentId]) REFERENCES [QCandidate].[MemoProgramComponents] ([Id]),
    CONSTRAINT [FK_Memos_SimulationCombinationMemos] FOREIGN KEY ([OriginId]) REFERENCES [dbo].[SimulationCombinationMemos] ([Id])
)

GO

CREATE CLUSTERED INDEX IX_Memos_MemoProgramComponentId ON [QCandidate].[Memos] ([MemoProgramComponentId]);

GO