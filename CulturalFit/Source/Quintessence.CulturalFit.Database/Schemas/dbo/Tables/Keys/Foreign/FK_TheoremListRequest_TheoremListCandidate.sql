ALTER TABLE [dbo].[TheoremListRequest]
	ADD CONSTRAINT [FK_TheoremListRequest_TheoremListCandidate]
	FOREIGN KEY (TheoremListCandidateId)
	REFERENCES [TheoremListCandidate] (Id)
