ALTER TABLE [dbo].[ProjectCategoryPsDetail]  
	ADD CONSTRAINT [FK_ProjectCategoryPsDetail_ProjectCategoryDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])