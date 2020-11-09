CREATE TABLE [dbo].[Memos]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [MemoProgramComponentId] UNIQUEIDENTIFIER NULL, 
    [Position] INT NULL, 
    [OriginId] UNIQUEIDENTIFIER NULL,
    [Audit_CreatedBy]        NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]        DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]       NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]       DATETIME         NULL,
    [Audit_DeletedBy]        NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]        DATETIME         NULL,
    [Audit_IsDeleted]        BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_Memo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Memo_MemoProgramComponent] FOREIGN KEY ([MemoProgramComponentId]) REFERENCES [dbo].[ProgramComponent] ([Id]),
    CONSTRAINT [FK_Memo_SimulationCombinationMemo] FOREIGN KEY ([OriginId]) REFERENCES [dbo].[SimulationCombinationMemos] ([Id])
)
