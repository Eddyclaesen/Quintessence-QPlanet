ALTER TABLE [dbo].[ProjectCategoryEaDetail]  
	ADD CONSTRAINT [FK_ProjectCategoryEaDetail_ProjectCategoryDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])