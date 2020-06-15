CREATE TABLE [dbo].[ProgramComponentSpecial] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [Name]               NVARCHAR (MAX)   NOT NULL,
    [Execution]          INT              NOT NULL,
    [IsWithLeadAssessor] BIT              NOT NULL,
    [Audit_CreatedBy]    NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]    DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]   NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]   DATETIME         NULL,
    [Audit_DeletedBy]    NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]    DATETIME         NULL,
    [Audit_IsDeleted]    BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProgramComponentSpecial] PRIMARY KEY CLUSTERED ([Id] ASC)
);

