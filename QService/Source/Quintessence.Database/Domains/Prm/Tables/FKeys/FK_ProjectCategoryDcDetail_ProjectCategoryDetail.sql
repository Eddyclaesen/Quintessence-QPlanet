ALTER TABLE [dbo].[ProjectCategoryDcDetail]  
	ADD CONSTRAINT [FK_ProjectCategoryDcDetail_ProjectCategoryDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])