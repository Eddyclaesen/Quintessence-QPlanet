CREATE TABLE [dbo].[ProjectCandidateReportRecipient] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateId] UNIQUEIDENTIFIER NOT NULL,
    [CrmEmailId]         INT              NOT NULL,
    [Audit_CreatedBy]    NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]    DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]   NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]   DATETIME         NULL,
    [Audit_DeletedBy]    NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]    DATETIME         NULL,
    [Audit_IsDeleted]    BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectCandidateReportRecipient] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCandidateReportRecipient_ProjectCandidate] FOREIGN KEY ([ProjectCandidateId]) REFERENCES [dbo].[ProjectCandidate] ([Id])
);

