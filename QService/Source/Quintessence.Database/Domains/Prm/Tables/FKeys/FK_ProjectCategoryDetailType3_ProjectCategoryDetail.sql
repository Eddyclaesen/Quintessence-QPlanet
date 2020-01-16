ALTER TABLE [dbo].[ProjectCategoryDetailType3]
	ADD CONSTRAINT [FK_ProjectCategoryDetailType3_ProjectCategoryDetail]
	FOREIGN KEY (Id)
	REFERENCES [ProjectCategoryDetail] (Id)
