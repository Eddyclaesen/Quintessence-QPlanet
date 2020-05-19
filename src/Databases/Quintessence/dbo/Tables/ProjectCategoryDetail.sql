CREATE TABLE [dbo].[ProjectCategoryDetail] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]             UNIQUEIDENTIFIER NOT NULL,
    [ProjectTypeCategoryId] UNIQUEIDENTIFIER NOT NULL,
    [UnitPrice]             DECIMAL (18, 2)  NOT NULL,
    [Audit_CreatedBy]       NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]       DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]      NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]      DATETIME         NULL,
    [Audit_DeletedBy]       NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]       DATETIME         NULL,
    [Audit_IsDeleted]       BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectCategoryDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryDetail_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

