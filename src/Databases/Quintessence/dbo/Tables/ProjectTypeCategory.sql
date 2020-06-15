CREATE TABLE [dbo].[ProjectTypeCategory] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (100)   NOT NULL,
    [Code]             VARCHAR (10)     NOT NULL,
    [SubCategoryType]  INT              NULL,
    [Execution]        INT              NULL,
    [Color]            VARCHAR (6)      NULL,
    [CrmTaskId]        INT              NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectTypeCategory] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [UK_ProjectTypeCategory_Code] UNIQUE NONCLUSTERED ([Code] ASC)
);

