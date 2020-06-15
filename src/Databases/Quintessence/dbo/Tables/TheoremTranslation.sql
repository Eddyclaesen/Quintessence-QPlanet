CREATE TABLE [dbo].[TheoremTranslation] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [TheoremId]        UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]       INT              NOT NULL,
    [Quote]            TEXT             NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_TheoremTranslation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TheoremTranslation_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_TheoremTranslation_Theorem] FOREIGN KEY ([TheoremId]) REFERENCES [dbo].[Theorem] ([Id])
);

