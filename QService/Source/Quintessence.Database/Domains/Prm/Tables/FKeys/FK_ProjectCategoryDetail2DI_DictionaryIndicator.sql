ALTER TABLE [dbo].[ProjectCategoryDetail2DictionaryIndicator]  
	ADD CONSTRAINT [FK_ProjectCategoryDetail2DictionaryIndicator_DictionaryIndicatory] 
	FOREIGN KEY([DictionaryIndicatorId])
	REFERENCES [dbo].[DictionaryIndicator] ([Id])