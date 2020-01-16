ALTER TABLE [dbo].[DictionaryLevel]  
	ADD CONSTRAINT [FK_DictionaryLevel_DictionaryCompetence] 
	FOREIGN KEY([DictionaryCompetenceId])
	REFERENCES [dbo].[DictionaryCompetence] ([Id])