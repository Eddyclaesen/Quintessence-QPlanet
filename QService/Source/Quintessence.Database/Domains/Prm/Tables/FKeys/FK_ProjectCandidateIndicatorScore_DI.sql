ALTER TABLE [dbo].[ProjectCandidateIndicatorScore]
	ADD CONSTRAINT [FK_ProjectCandidateIndicatorScore_DictionaryIndicator]
	FOREIGN KEY (DictionaryIndicatorId)
	REFERENCES [DictionaryIndicator] (Id)
