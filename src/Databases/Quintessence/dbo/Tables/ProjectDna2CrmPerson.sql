CREATE TABLE [dbo].[ProjectDna2CrmPerson] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ProjectDnaId]     UNIQUEIDENTIFIER NOT NULL,
    [CrmPersonId]      INT              NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectDna2CrmPerson] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectDna2CrmPerson_ProjectDna] FOREIGN KEY ([ProjectDnaId]) REFERENCES [dbo].[ProjectDna] ([Id])
);

