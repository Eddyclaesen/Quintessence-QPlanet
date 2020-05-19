CREATE TABLE [dbo].[ProjectRole] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (100)   NOT NULL,
    [ContactId]        INT              NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Legacy6Id]        INT              NULL,
    [Legacy14Id]       INT              NULL,
    CONSTRAINT [PK_ProjectRole] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

