CREATE TABLE [dbo].[ProjectCandidateClusterScore] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateId]  UNIQUEIDENTIFIER NOT NULL,
    [DictionaryClusterId] UNIQUEIDENTIFIER NOT NULL,
    [Score]               DECIMAL (18, 2)  NULL,
    [Statement]           TEXT             NULL,
    [Audit_CreatedBy]     NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]     DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]    NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]    DATETIME         NULL,
    [Audit_DeletedBy]     NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]     DATETIME         NULL,
    [Audit_IsDeleted]     BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectCandidateClusterScore] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCandidateClusterScore_DictionaryCluster] FOREIGN KEY ([DictionaryClusterId]) REFERENCES [dbo].[DictionaryCluster] ([Id]),
    CONSTRAINT [FK_ProjectCandidateClusterScore_ProjectCandidate] FOREIGN KEY ([ProjectCandidateId]) REFERENCES [dbo].[ProjectCandidate] ([Id]),
    CONSTRAINT [UK_ProjectCandidateClusterScore] UNIQUE NONCLUSTERED ([ProjectCandidateId] ASC, [DictionaryClusterId] ASC)
);

