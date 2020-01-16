ALTER TABLE [dbo].[CulturalFitCandidateLink]
	ADD CONSTRAINT [FK_CulturalFitCandidateLink_TheoremListCandidate]
	FOREIGN KEY (TheoremListCandidateId)
	REFERENCES [TheoremListCandidate] (Id)
