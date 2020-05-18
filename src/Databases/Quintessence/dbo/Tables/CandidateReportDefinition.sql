CREATE TABLE [dbo].[CandidateReportDefinition] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ContactId]        INT              NULL,
    [Name]             NVARCHAR (MAX)   NOT NULL,
    [Location]         NVARCHAR (MAX)   NOT NULL,
    [IsActive]         BIT              DEFAULT ((1)) NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_CandidateReportDefinition] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

