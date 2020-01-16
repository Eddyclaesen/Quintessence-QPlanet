ALTER TABLE [dbo].[ProjectCategoryAcDetail]  
	ADD CONSTRAINT [FK_ProjectCategoryAcDetail_ProjectCategoryDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])