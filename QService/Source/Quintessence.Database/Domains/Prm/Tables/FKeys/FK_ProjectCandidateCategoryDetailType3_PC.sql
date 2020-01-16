ALTER TABLE [dbo].[ProjectCandidateCategoryDetailType3]
	ADD CONSTRAINT [FK_ProjectCandidateCategoryDetailType3_ProjectCandidate]
	FOREIGN KEY ([ProjectCandidateId])
	REFERENCES [ProjectCandidate] (Id)
