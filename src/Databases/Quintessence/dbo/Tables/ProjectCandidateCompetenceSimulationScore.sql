CREATE TABLE [dbo].[ProjectCandidateCompetenceSimulationScore] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateId]      UNIQUEIDENTIFIER NOT NULL,
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL,
    [DictionaryCompetenceId]  UNIQUEIDENTIFIER NOT NULL,
    [Score]                   DECIMAL (18, 2)  NULL,
    [Remarks]                 TEXT             NULL,
    [Audit_CreatedBy]         NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]         DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]        NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]        DATETIME         NULL,
    [Audit_DeletedBy]         NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]         DATETIME         NULL,
    [Audit_IsDeleted]         BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]         UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectCandidateCompetenceSimulationScore] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCandidateCompetenceSimulationScore_DictionaryIndicator] FOREIGN KEY ([DictionaryCompetenceId]) REFERENCES [dbo].[DictionaryCompetence] ([Id]),
    CONSTRAINT [FK_ProjectCandidateCompetenceSimulationScore_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id]),
    CONSTRAINT [UK_ProjectCandidateCompetenceSimulationScore] UNIQUE NONCLUSTERED ([ProjectCandidateId] ASC, [SimulationCombinationId] ASC, [DictionaryCompetenceId] ASC)
);

