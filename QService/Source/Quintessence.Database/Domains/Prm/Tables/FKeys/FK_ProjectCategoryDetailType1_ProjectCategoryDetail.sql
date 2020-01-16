ALTER TABLE [dbo].[ProjectCategoryDetailType1]
	ADD CONSTRAINT [FK_ProjectCategoryDetailType1_ProjectCategoryDetail]
	FOREIGN KEY (Id)
	REFERENCES [ProjectCategoryDetail] (Id)
