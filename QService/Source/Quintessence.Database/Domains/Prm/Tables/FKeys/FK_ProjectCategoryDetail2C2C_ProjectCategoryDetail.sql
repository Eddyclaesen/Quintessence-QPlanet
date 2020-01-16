ALTER TABLE [dbo].[ProjectCategoryDetail2Competence2Combination]  
	ADD CONSTRAINT [FK_ProjectCategoryDetail2C2C_ProjectCategoryDetail] 
	FOREIGN KEY([ProjectCategoryDetailId])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])