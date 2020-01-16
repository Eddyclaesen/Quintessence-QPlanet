ALTER TABLE [dbo].[ProjectCandidateCompetenceScore]
	ADD CONSTRAINT [FK_ProjectCandidateCompetenceScore_DictionaryCompetence]
	FOREIGN KEY (DictionaryCompetenceId)
	REFERENCES [DictionaryCompetence] (Id)
