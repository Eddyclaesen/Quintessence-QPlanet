CREATE TABLE [dbo].[DictionaryIndicatorTranslation] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]            INT              NOT NULL,
    [DictionaryIndicatorId] UNIQUEIDENTIFIER NOT NULL,
    [Text]                  TEXT             NOT NULL,
    [Audit_CreatedBy]       NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]       DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]      NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]      DATETIME         NULL,
    [Audit_DeletedBy]       NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]       DATETIME         NULL,
    [Audit_IsDeleted]       BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_DictionaryIndicatorTranslation] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DictionaryIndicatorTranslation_DictionaryIndicator] FOREIGN KEY ([DictionaryIndicatorId]) REFERENCES [dbo].[DictionaryIndicator] ([Id])
);

