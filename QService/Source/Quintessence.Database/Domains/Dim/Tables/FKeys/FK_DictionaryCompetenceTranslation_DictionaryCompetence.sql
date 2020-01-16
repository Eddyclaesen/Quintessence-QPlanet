ALTER TABLE [dbo].[DictionaryCompetenceTranslation]  
	ADD CONSTRAINT [FK_DictionaryCompetenceTranslation_DictionaryCompetence] 
	FOREIGN KEY([DictionaryCompetenceId])
	REFERENCES [dbo].[DictionaryCompetence] ([Id])