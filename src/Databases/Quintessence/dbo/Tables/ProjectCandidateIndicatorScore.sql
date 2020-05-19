CREATE TABLE [dbo].[ProjectCandidateIndicatorScore] (
    [Id]                                UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateId]                UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateCompetenceScoreId] UNIQUEIDENTIFIER NOT NULL,
    [DictionaryIndicatorId]             UNIQUEIDENTIFIER NOT NULL,
    [Score]                             DECIMAL (18, 2)  NULL,
    [Audit_CreatedBy]                   NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]                   DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]                  NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]                  DATETIME         NULL,
    [Audit_DeletedBy]                   NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]                   DATETIME         NULL,
    [Audit_IsDeleted]                   BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]                   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectCandidateIndicatorScore] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCandidateIndicatorScore_DictionaryIndicator] FOREIGN KEY ([DictionaryIndicatorId]) REFERENCES [dbo].[DictionaryIndicator] ([Id]),
    CONSTRAINT [FK_ProjectCandidateIndicatorScore_ProjectCandidate] FOREIGN KEY ([ProjectCandidateId]) REFERENCES [dbo].[ProjectCandidate] ([Id]),
    CONSTRAINT [FK_ProjectCandidateIndicatorScore_ProjectCandidateCompetenceScore] FOREIGN KEY ([ProjectCandidateCompetenceScoreId]) REFERENCES [dbo].[ProjectCandidateCompetenceScore] ([Id]),
    CONSTRAINT [UK_ProjectCandidateIndicatorScore] UNIQUE NONCLUSTERED ([ProjectCandidateId] ASC, [DictionaryIndicatorId] ASC)
);

