CREATE TABLE [QCandidate].[Memos]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [MemoProgramComponentId] UNIQUEIDENTIFIER NOT NULL, 
    [Position] INT NOT NULL, 
    [OriginId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]        NVARCHAR (MAX)    NOT NULL,
    [CreatedOn]        DATETIME          NOT NULL,
    [ModifiedBy]       NVARCHAR (MAX)   NULL,
    [ModifiedOn]       DATETIME         NULL,
    [ConcurrencyLock]        TIMESTAMP  NOT NULL,
    CONSTRAINT [PK_Memo] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Memo_MemoProgramComponent] FOREIGN KEY ([MemoProgramComponentId]) REFERENCES [QCandidate].[MemoProgramComponents] ([Id]),
    CONSTRAINT [FK_Memo_SimulationCombinationMemo] FOREIGN KEY ([OriginId]) REFERENCES [dbo].[SimulationCombinationMemos] ([Id])
)

GO

CREATE CLUSTERED INDEX IX_Memos_MemoProgramComponentId ON [QCandidate].[Memos] ([MemoProgramComponentId]);

GO