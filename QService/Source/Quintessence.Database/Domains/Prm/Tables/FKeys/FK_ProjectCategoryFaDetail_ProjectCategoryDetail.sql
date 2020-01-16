ALTER TABLE [dbo].[ProjectCategoryFaDetail]  
	ADD CONSTRAINT [FK_ProjectCategoryFaDetail_ProjectCategoryDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])