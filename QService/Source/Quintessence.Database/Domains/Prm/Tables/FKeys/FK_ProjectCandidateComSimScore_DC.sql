ALTER TABLE [dbo].[ProjectCandidateCompetenceSimulationScore]
	ADD CONSTRAINT [FK_ProjectCandidateCompetenceSimulationScore_DictionaryIndicator]
	FOREIGN KEY (DictionaryCompetenceId)
	REFERENCES [DictionaryCompetence] (Id)
