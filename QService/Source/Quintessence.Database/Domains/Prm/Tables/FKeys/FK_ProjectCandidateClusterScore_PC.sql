ALTER TABLE [dbo].[ProjectCandidateClusterScore]
	ADD CONSTRAINT [FK_ProjectCandidateClusterScore_ProjectCandidate]
	FOREIGN KEY (ProjectCandidateId)
	REFERENCES [ProjectCandidate] (Id)
