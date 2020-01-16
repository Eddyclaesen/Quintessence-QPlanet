ALTER TABLE [dbo].[ProjectCandidateCategoryDetailType2]
	ADD CONSTRAINT [FK_ProjectCandidateCategoryDetailType2_ProjectCandidate]
	FOREIGN KEY ([ProjectCandidateId])
	REFERENCES [ProjectCandidate] (Id)
