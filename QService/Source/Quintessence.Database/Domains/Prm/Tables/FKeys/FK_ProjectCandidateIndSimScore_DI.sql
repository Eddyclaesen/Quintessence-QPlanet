ALTER TABLE [dbo].[ProjectCandidateIndicatorSimulationScore]
	ADD CONSTRAINT [FK_ProjectCandidateIndicatorSimulationScore_DictionaryIndicator]
	FOREIGN KEY (DictionaryIndicatorId)
	REFERENCES [DictionaryIndicator] (Id)
