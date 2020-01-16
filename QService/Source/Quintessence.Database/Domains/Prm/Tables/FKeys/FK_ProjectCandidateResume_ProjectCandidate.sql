ALTER TABLE [dbo].[ProjectCandidateResume]
	ADD CONSTRAINT [FK_ProjectCandidateResume_ProjectCandidate]
	FOREIGN KEY (ProjectCandidateId)
	REFERENCES [ProjectCandidate] (Id)
