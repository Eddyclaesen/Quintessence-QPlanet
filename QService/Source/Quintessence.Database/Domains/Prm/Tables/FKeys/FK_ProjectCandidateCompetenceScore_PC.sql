ALTER TABLE [dbo].[ProjectCandidateCompetenceScore]
	ADD CONSTRAINT [FK_ProjectCandidateCompetenceScore_ProjectCandidate]
	FOREIGN KEY (ProjectCandidateId)
	REFERENCES [ProjectCandidate] (Id)
