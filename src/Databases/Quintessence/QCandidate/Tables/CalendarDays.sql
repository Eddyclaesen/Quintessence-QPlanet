CREATE TABLE [QCandidate].[CalendarDays]
(
	[Id]                        UNIQUEIDENTIFIER    NOT NULL, 
    [MemoProgramComponentId]    UNIQUEIDENTIFIER    NOT NULL, 
    [Day]                       DATETIME2           NOT NULL, 
    [Note]                      NVARCHAR(MAX)       NULL,
    [CreatedBy]                 NVARCHAR (250)      NOT NULL,
    [CreatedOn]                 DATETIME2           NOT NULL,
    [ModifiedBy]                NVARCHAR (250)      NULL,
    [ModifiedOn]                DATETIME2           NULL,
    [ConcurrencyLock]           TIMESTAMP           NOT NULL,
    CONSTRAINT [PK_CalendarDays] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CalendarDays_MemoProgramComponents] FOREIGN KEY ([MemoProgramComponentId]) REFERENCES [QCandidate].[MemoProgramComponents] ([Id])
)
GO

CREATE CLUSTERED INDEX IX_CalendarDays_MemoProgramComponentId ON [QCandidate].[CalendarDays] ([MemoProgramComponentId]);

GO