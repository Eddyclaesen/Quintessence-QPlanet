ALTER TABLE [dbo].[DictionaryIndicator]  
	ADD CONSTRAINT [FK_DictionaryIndicator_DictionaryLevel] 
	FOREIGN KEY([DictionaryLevelId])
	REFERENCES [dbo].[DictionaryLevel] ([Id])