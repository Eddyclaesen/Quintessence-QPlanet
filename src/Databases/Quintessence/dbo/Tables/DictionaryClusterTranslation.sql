CREATE TABLE [dbo].[DictionaryClusterTranslation] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]          INT              NOT NULL,
    [DictionaryClusterId] UNIQUEIDENTIFIER NOT NULL,
    [Text]                TEXT             NOT NULL,
    [Description]         TEXT             NULL,
    [Audit_CreatedBy]     NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]     DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]    NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]    DATETIME         NULL,
    [Audit_DeletedBy]     NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]     DATETIME         NULL,
    [Audit_IsDeleted]     BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_DictionaryClusterTranslation] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

