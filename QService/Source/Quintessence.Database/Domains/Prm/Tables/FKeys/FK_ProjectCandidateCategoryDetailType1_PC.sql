ALTER TABLE [dbo].[ProjectCandidateCategoryDetailType1]
	ADD CONSTRAINT [FK_ProjectCandidateCategoryDetailType1_ProjectCandidate]
	FOREIGN KEY ([ProjectCandidateId])
	REFERENCES [ProjectCandidate] (Id)
