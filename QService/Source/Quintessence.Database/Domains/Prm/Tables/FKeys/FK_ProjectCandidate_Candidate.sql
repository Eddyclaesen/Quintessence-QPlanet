ALTER TABLE [dbo].[ProjectCandidate]
	ADD CONSTRAINT [FK_ProjectCandidate_Candidate]
	FOREIGN KEY (CandidateId)
	REFERENCES [Candidate] (Id)
