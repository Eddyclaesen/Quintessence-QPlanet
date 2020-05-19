CREATE TABLE [dbo].[ProjectTypeCategoryUnitPrice] (
    [Id]                         UNIQUEIDENTIFIER NOT NULL,
    [ProjectTypeCategoryId]      UNIQUEIDENTIFIER NOT NULL,
    [ProjectTypeCategoryLevelId] UNIQUEIDENTIFIER NOT NULL,
    [UnitPrice]                  DECIMAL (18, 2)  NOT NULL,
    [Audit_CreatedBy]            NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]            DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]           NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]           DATETIME         NULL,
    [Audit_DeletedBy]            NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]            DATETIME         NULL,
    [Audit_IsDeleted]            BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]            UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectTypeCategoryUnitPrice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectTypeCategoryUnitPrice_ProjectTypeCategory] FOREIGN KEY ([ProjectTypeCategoryId]) REFERENCES [dbo].[ProjectTypeCategory] ([Id]),
    CONSTRAINT [FK_ProjectTypeCategoryUnitPrice_ProjectTypeCategoryLevel] FOREIGN KEY ([ProjectTypeCategoryLevelId]) REFERENCES [dbo].[ProjectTypeCategoryLevel] ([Id])
);

