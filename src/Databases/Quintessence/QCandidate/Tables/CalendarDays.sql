CREATE TABLE [QCandidate].[CalendarDays]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [MemoProgramComponentId] UNIQUEIDENTIFIER NOT NULL, 
    [Day] DATETIME NOT NULL, 
    [Note] NVARCHAR(MAX) NOT NULL,
    [CreatedBy]        NVARCHAR (MAX)    NOT NULL,
    [CreatedOn]        DATETIME          NOT NULL,
    [ModifiedBy]       NVARCHAR (MAX)   NULL,
    [ModifiedOn]       DATETIME         NULL,
    [ConcurrencyLock]        TIMESTAMP  NOT NULL,
    CONSTRAINT [PK_CalendarDay] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CalendarDay_MemoProgramComponent] FOREIGN KEY ([MemoProgramComponentId]) REFERENCES [QCandidate].[MemoProgramComponents] ([Id])
)
GO

CREATE CLUSTERED INDEX IX_CalendarDays_MemoProgramComponentId ON [QCandidate].[CalendarDays] ([MemoProgramComponentId]);

GO