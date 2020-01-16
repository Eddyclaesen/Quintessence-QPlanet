CREATE TABLE [dbo].[ProjectCandidateAssessor](
	[ProjectCandidateId]	UNIQUEIDENTIFIER	NOT NULL,
    [UserId]				UNIQUEIDENTIFIER	NOT NULL,
	[AssessorType]			INT					NOT NULL --0: Head assessor, 1: Co-assessor
)