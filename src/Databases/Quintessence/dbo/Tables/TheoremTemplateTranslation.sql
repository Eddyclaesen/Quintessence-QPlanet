CREATE TABLE [dbo].[TheoremTemplateTranslation] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [TheoremTemplateId] UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]        INT              NOT NULL,
    [Text]              NVARCHAR (MAX)   NOT NULL,
    [IsDefault]         BIT              NOT NULL,
    [Audit_CreatedBy]   NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]  NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]  DATETIME         NULL,
    [Audit_DeletedBy]   NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]   DATETIME         NULL,
    [Audit_IsDeleted]   BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_TheoremTemplateTranslation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TheoremTemplateTranslation_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_TheoremTemplateTranslation_TheoremTemplate] FOREIGN KEY ([TheoremTemplateId]) REFERENCES [dbo].[TheoremTemplate] ([Id])
);

