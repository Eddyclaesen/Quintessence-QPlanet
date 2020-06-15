CREATE TABLE [dbo].[ProjectDna2ProjectDnaType] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ProjectDnaId]     UNIQUEIDENTIFIER NOT NULL,
    [ProjectDnaTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectDna2ProjectDnaType] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectDna2ProjectDnaType_ProjectDna] FOREIGN KEY ([ProjectDnaId]) REFERENCES [dbo].[ProjectDna] ([Id]),
    CONSTRAINT [FK_ProjectDna2ProjectDnaType_ProjectDnaType] FOREIGN KEY ([ProjectDnaTypeId]) REFERENCES [dbo].[ProjectDnaType] ([Id])
);

