ALTER TABLE [dbo].[ProjectCandidateCategoryDetailType3]
	ADD CONSTRAINT [FK_ProjectCandidateCategoryDetailType3_ProjectCategoryDetailType3]
	FOREIGN KEY ([ProjectCategoryDetailType3Id])
	REFERENCES [ProjectCategoryDetailType3] (Id)
