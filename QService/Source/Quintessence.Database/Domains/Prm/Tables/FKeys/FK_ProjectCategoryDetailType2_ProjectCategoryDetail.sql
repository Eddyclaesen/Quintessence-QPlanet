ALTER TABLE [dbo].[ProjectCategoryDetailType2]
	ADD CONSTRAINT [FK_ProjectCategoryDetailType2_ProjectCategoryDetail]
	FOREIGN KEY (Id)
	REFERENCES [ProjectCategoryDetail] (Id)
