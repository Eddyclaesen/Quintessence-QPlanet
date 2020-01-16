ALTER TABLE [dbo].[ProjectCategoryCuDetail]  
	ADD CONSTRAINT [FK_ProjectCategoryCuDetail_ProjectCategoryDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])