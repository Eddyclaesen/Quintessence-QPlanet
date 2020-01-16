ALTER TABLE [dbo].[ProjectCandidateIndicatorScore]
	ADD CONSTRAINT [FK_ProjectCandidateIndicatorScore_ProjectCandidateCompetenceScore]
	FOREIGN KEY (ProjectCandidateCompetenceScoreId)
	REFERENCES [ProjectCandidateCompetenceScore] (Id)
