ALTER TABLE [dbo].[ProjectCategoryDetail2DictionaryIndicator]  
	ADD CONSTRAINT [FK_ProjectCategoryDetail2DictionaryIndicator_ProjectCategoryDetail] 
	FOREIGN KEY([ProjectCategoryDetailId])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])