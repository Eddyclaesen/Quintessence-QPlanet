ALTER TABLE [dbo].[ProjectCategoryDetail]  
	ADD CONSTRAINT [FK_ProjectCategoryDetail_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])