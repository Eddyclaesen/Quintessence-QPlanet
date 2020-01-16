ALTER TABLE [dbo].[DictionaryLevelTranslation]  
	ADD CONSTRAINT [FK_DictionaryLevelTranslation_DictionaryLevel] 
	FOREIGN KEY([DictionaryLevelId])
	REFERENCES [dbo].[DictionaryLevel] ([Id])