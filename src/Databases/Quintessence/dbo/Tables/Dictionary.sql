CREATE TABLE [dbo].[Dictionary] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ContactId]        INT              NULL,
    [Name]             NVARCHAR (MAX)   NOT NULL,
    [Current]          BIT              NOT NULL,
    [Description]      TEXT             NULL,
    [IsLive]           BIT              NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [LegacyId]         INT              NOT NULL,
    CONSTRAINT [PK_Dictionary] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

