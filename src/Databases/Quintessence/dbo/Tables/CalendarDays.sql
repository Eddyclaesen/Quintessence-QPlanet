CREATE TABLE [dbo].[CalendarDays]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [MemoProgramComponentId] UNIQUEIDENTIFIER NOT NULL, 
    [Day] DATETIME NOT NULL, 
    [Note] NVARCHAR(MAX) NOT NULL,
    [Audit_CreatedBy]        NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]        DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]       NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]       DATETIME         NULL,
    [Audit_DeletedBy]        NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]        DATETIME         NULL,
    [Audit_IsDeleted]        BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_CalendarDay] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CalendarDay_MemoProgramComponent] FOREIGN KEY ([MemoProgramComponentId]) REFERENCES [dbo].[ProgramComponent] ([Id])
)
