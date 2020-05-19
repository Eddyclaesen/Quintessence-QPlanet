CREATE TABLE [dbo].[Product] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]        UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (100)   NOT NULL,
    [ProductTypeId]    UNIQUEIDENTIFIER NOT NULL,
    [UnitPrice]        DECIMAL (18, 2)  NOT NULL,
    [Description]      TEXT             NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Product_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductType] ([Id]),
    CONSTRAINT [FK_Product_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

