CREATE TABLE [dbo].[ActivityDetailTrainingLanguage] (
    [Id]                       UNIQUEIDENTIFIER NOT NULL,
    [ActivityDetailTrainingId] UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]               INT              NOT NULL,
    [SessionQuantity]          INT              NOT NULL,
    [Audit_CreatedBy]          NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]          DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]         NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]         DATETIME         NULL,
    [Audit_DeletedBy]          NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]          DATETIME         NULL,
    [Audit_IsDeleted]          BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ActivityDetailTrainingLanguage] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityDetailTrainingLanguage_ActivityDetailTraining] FOREIGN KEY ([ActivityDetailTrainingId]) REFERENCES [dbo].[ActivityDetailTraining] ([Id]),
    CONSTRAINT [FK_ActivityDetailTrainingLanguage_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
);

