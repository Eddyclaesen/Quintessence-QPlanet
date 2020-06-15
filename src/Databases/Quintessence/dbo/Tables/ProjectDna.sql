CREATE TABLE [dbo].[ProjectDna] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [CrmProjectId]          INT              NOT NULL,
    [ApprovedForReferences] BIT              NULL,
    [Details]               NVARCHAR (MAX)   NULL,
    [Audit_CreatedBy]       NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]       DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]      NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]      DATETIME         NULL,
    [Audit_DeletedBy]       NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]       DATETIME         NULL,
    [Audit_IsDeleted]       BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectDna] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [UK_ProjectDna_CrmProjectId] UNIQUE NONCLUSTERED ([CrmProjectId] ASC)
);

