CREATE TABLE [dbo].[ActivityDetailWorkshopLanguage] (
    [Id]                       UNIQUEIDENTIFIER NOT NULL,
    [ActivityDetailWorkshopId] UNIQUEIDENTIFIER NOT NULL,
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
    CONSTRAINT [PK_ActivityDetailWorkshopLanguage] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityDetailWorkshopLanguage_ActivityDetailWorkshop] FOREIGN KEY ([ActivityDetailWorkshopId]) REFERENCES [dbo].[ActivityDetailWorkshop] ([Id]),
    CONSTRAINT [FK_ActivityDetailWorkshopLanguage_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
);

