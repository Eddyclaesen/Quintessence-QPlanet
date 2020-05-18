CREATE TABLE [dbo].[DictionaryCluster] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [DictionaryId]     UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (MAX)   NOT NULL,
    [Description]      TEXT             NULL,
    [Color]            NVARCHAR (10)    NULL,
    [Order]            INT              NOT NULL,
    [ImageName]        NVARCHAR (MAX)   NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [LegacyId]         INT              NOT NULL,
    CONSTRAINT [PK_DictionaryCluster] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DictionaryCluster_Dictionary] FOREIGN KEY ([DictionaryId]) REFERENCES [dbo].[Dictionary] ([Id])
);

