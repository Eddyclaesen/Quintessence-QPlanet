ALTER TABLE [dbo].[ProjectCategorySoDetail]  
	ADD CONSTRAINT [FK_ProjectCategorySoDetail_ProjectCategoryDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])