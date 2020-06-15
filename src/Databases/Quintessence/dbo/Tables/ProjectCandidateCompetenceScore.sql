CREATE TABLE [dbo].[ProjectCandidateCompetenceScore] (
    [Id]                             UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateId]             UNIQUEIDENTIFIER NOT NULL,
    [DictionaryCompetenceId]         UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateClusterScoreId] UNIQUEIDENTIFIER NULL,
    [Score]                          DECIMAL (18, 2)  NULL,
    [Statement]                      TEXT             NULL,
    [Audit_CreatedBy]                NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]                DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]               NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]               DATETIME         NULL,
    [Audit_DeletedBy]                NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]                DATETIME         NULL,
    [Audit_IsDeleted]                BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]                UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectCandidateCompetenceScore] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCandidateCompetenceScore_DictionaryCompetence] FOREIGN KEY ([DictionaryCompetenceId]) REFERENCES [dbo].[DictionaryCompetence] ([Id]),
    CONSTRAINT [FK_ProjectCandidateCompetenceScore_ProjectCandidate] FOREIGN KEY ([ProjectCandidateId]) REFERENCES [dbo].[ProjectCandidate] ([Id]),
    CONSTRAINT [UK_ProjectCandidateCompetenceScore] UNIQUE NONCLUSTERED ([ProjectCandidateId] ASC, [DictionaryCompetenceId] ASC)
);

