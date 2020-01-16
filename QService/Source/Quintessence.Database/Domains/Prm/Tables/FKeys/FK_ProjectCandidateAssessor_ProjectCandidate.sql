ALTER TABLE [dbo].[ProjectCandidateAssessor]
	ADD CONSTRAINT [FK_ProjectCandidateAssessor_ProjectCandidate]
	FOREIGN KEY (ProjectCandidateId)
	REFERENCES [ProjectCandidate] (Id)
