ALTER TABLE [dbo].[ProjectCandidateIndicatorScore]
	ADD CONSTRAINT [FK_ProjectCandidateIndicatorScore_ProjectCandidate]
	FOREIGN KEY (ProjectCandidateId)
	REFERENCES [ProjectCandidate] (Id)
