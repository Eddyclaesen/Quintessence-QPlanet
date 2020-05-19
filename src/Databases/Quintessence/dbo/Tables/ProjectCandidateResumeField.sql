CREATE TABLE [dbo].[ProjectCandidateResumeField] (
    [Id]                               UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateResumeId]         UNIQUEIDENTIFIER NOT NULL,
    [CandidateReportDefinitionFieldId] UNIQUEIDENTIFIER NOT NULL,
    [Statement]                        TEXT             NULL,
    [Audit_CreatedBy]                  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]                  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]                 NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]                 DATETIME         NULL,
    [Audit_DeletedBy]                  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]                  DATETIME         NULL,
    [Audit_IsDeleted]                  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]                  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectCandidateResumeField] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCandidateResumeField_CandidateReportDefinitionField] FOREIGN KEY ([CandidateReportDefinitionFieldId]) REFERENCES [dbo].[CandidateReportDefinitionField] ([Id]),
    CONSTRAINT [FK_ProjectCandidateResumeField_ProjectCandidateResume] FOREIGN KEY ([ProjectCandidateResumeId]) REFERENCES [dbo].[ProjectCandidateResume] ([Id])
);

