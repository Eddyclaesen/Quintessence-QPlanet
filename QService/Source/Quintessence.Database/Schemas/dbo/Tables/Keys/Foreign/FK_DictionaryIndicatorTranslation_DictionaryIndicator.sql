ALTER TABLE [dbo].[DictionaryIndicatorTranslation]  
	ADD CONSTRAINT [FK_DictionaryIndicatorTranslation_DictionaryIndicator] 
	FOREIGN KEY([DictionaryIndicatorId])
	REFERENCES [dbo].[DictionaryIndicator] ([Id])