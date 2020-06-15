CREATE TABLE [dbo].[MailTemplateTranslation] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Subject]          NVARCHAR (MAX)   NULL,
    [Body]             NVARCHAR (MAX)   NULL,
    [LanguageId]       INT              NOT NULL,
    [MailTemplateId]   UNIQUEIDENTIFIER NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_MailTemplateTranslation] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MailTemplateTranslation_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_MailTemplateTranslation_MailTemplate] FOREIGN KEY ([MailTemplateId]) REFERENCES [dbo].[MailTemplate] ([Id])
);

