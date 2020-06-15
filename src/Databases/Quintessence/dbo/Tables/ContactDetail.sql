CREATE TABLE [dbo].[ContactDetail] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ContactId]        INT              NOT NULL,
    [Remarks]          TEXT             NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ContactDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [UK_ContactDetail_ContactId] UNIQUE NONCLUSTERED ([ContactId] ASC)
);

