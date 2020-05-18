CREATE TABLE [dbo].[TheoremTemplate] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [TheoremListTemplateId] UNIQUEIDENTIFIER NOT NULL,
    [Audit_CreatedBy]       NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]       DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]      NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]      DATETIME         NULL,
    [Audit_DeletedBy]       NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]       DATETIME         NULL,
    [Audit_IsDeleted]       BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_TheoremTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TheoremTemplate_TheoremListTemplate] FOREIGN KEY ([TheoremListTemplateId]) REFERENCES [dbo].[TheoremListTemplate] ([Id])
);

