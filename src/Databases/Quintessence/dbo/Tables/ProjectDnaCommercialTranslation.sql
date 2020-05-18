CREATE TABLE [dbo].[ProjectDnaCommercialTranslation] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ProjectDnaId]     UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]       INT              NOT NULL,
    [CommercialName]   NVARCHAR (MAX)   NULL,
    [CommercialRecap]  NVARCHAR (MAX)   NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectDnaCommercialTranslation] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectDnaCommercialTranslation_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_ProjectDnaCommercialTranslation_ProjectDna] FOREIGN KEY ([ProjectDnaId]) REFERENCES [dbo].[ProjectDna] ([Id])
);

