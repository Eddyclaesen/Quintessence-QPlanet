CREATE TABLE [dbo].[DictionaryCompetenceTranslation] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]             INT              NOT NULL,
    [DictionaryCompetenceId] UNIQUEIDENTIFIER NOT NULL,
    [Text]                   TEXT             NOT NULL,
    [Description]            TEXT             NULL,
    [Audit_CreatedBy]        NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]        DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]       NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]       DATETIME         NULL,
    [Audit_DeletedBy]        NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]        DATETIME         NULL,
    [Audit_IsDeleted]        BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_DictionaryCompetenceTranslation] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DictionaryCompetenceTranslation_DictionaryCompetence] FOREIGN KEY ([DictionaryCompetenceId]) REFERENCES [dbo].[DictionaryCompetence] ([Id])
);

