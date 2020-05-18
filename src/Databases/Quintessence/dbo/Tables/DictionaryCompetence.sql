CREATE TABLE [dbo].[DictionaryCompetence] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [DictionaryClusterId] UNIQUEIDENTIFIER NOT NULL,
    [Name]                NVARCHAR (MAX)   NOT NULL,
    [Description]         TEXT             NULL,
    [Order]               INT              NOT NULL,
    [Audit_CreatedBy]     NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]     DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]    NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]    DATETIME         NULL,
    [Audit_DeletedBy]     NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]     DATETIME         NULL,
    [Audit_IsDeleted]     BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [LegacyId]            INT              NOT NULL,
    CONSTRAINT [PK_DictionaryCompetence] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DictionaryCompetence_DictionaryCluster] FOREIGN KEY ([DictionaryClusterId]) REFERENCES [dbo].[DictionaryCluster] ([Id])
);

