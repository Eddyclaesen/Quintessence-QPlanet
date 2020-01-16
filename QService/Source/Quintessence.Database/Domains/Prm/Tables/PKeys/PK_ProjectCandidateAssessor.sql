ALTER TABLE [dbo].[ProjectCandidateAssessor]
	ADD CONSTRAINT [PK_ProjectCandidateAssessor] 
	PRIMARY KEY NONCLUSTERED ([ProjectCandidateId], [UserId])