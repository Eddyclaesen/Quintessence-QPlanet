CREATE TABLE [dbo].[TheoremListTemplate] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (MAX)   NOT NULL,
    [Code]             NVARCHAR (20)    NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_TheoremListTemplate] PRIMARY KEY CLUSTERED ([Id] ASC)
);

