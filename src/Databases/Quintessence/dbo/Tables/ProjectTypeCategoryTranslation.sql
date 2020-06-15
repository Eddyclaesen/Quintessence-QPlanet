CREATE TABLE [dbo].[ProjectTypeCategoryTranslation] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [ProjectTypeCategoryId] UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]            INT              NOT NULL,
    [Name]                  NVARCHAR (MAX)   NULL,
    [Audit_CreatedBy]       NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]       DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]      NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]      DATETIME         NULL,
    [Audit_DeletedBy]       NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]       DATETIME         NULL,
    [Audit_IsDeleted]       BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectTypeCategoryTranslation] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectTypeCategoryTranslation_ProjectTypeCategory] FOREIGN KEY ([ProjectTypeCategoryId]) REFERENCES [dbo].[ProjectTypeCategory] ([Id]),
    CONSTRAINT [FK_ProjectTypeCategoryTranslation_Translation] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
);

