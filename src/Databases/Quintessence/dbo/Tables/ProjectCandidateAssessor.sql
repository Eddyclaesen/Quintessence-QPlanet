CREATE TABLE [dbo].[ProjectCandidateAssessor] (
    [ProjectCandidateId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]             UNIQUEIDENTIFIER NOT NULL,
    [AssessorType]       INT              NOT NULL,
    CONSTRAINT [PK_ProjectCandidateAssessor] PRIMARY KEY NONCLUSTERED ([ProjectCandidateId] ASC, [UserId] ASC),
    CONSTRAINT [FK_ProjectCandidateAssessor_ProjectCandidate] FOREIGN KEY ([ProjectCandidateId]) REFERENCES [dbo].[ProjectCandidate] ([Id]),
    CONSTRAINT [FK_ProjectCandidateAssessor_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

