ALTER TABLE [dbo].[ProjectCategoryCaDetail]  
	ADD CONSTRAINT [FK_ProjectCategoryCaDetail_ProjectCategoryDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])