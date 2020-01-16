ALTER TABLE [dbo].[ProjectCategoryFdDetail]  
	ADD CONSTRAINT [FK_ProjectCategoryFdDetail_ProjectCategoryDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])