CREATE TABLE [dbo].[ProgramComponent] (
    [Id]                                   UNIQUEIDENTIFIER NOT NULL,
    [AssessmentRoomId]                     UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateId]                   UNIQUEIDENTIFIER NOT NULL,
    [SimulationCombinationId]              UNIQUEIDENTIFIER NULL,
    [SimulationCombinationTypeCode]        INT              NOT NULL,
    [ProjectCandidateCategoryDetailTypeId] UNIQUEIDENTIFIER NULL,
    [LeadAssessorUserId]                   UNIQUEIDENTIFIER NULL,
    [CoAssessorUserId]                     UNIQUEIDENTIFIER NULL,
    [Description]                          TEXT             NULL,
    [Start]                                DATETIME         NOT NULL,
    [End]                                  DATETIME         NOT NULL,
    [Audit_CreatedBy]                      NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]                      DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]                     NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]                     DATETIME         NULL,
    [Audit_DeletedBy]                      NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]                      DATETIME         NULL,
    [Audit_IsDeleted]                      BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]                      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProgramComponent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProgramComponent_ProjectCandidate] FOREIGN KEY ([ProjectCandidateId]) REFERENCES [dbo].[ProjectCandidate] ([Id])
);

