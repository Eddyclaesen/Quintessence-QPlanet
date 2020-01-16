ALTER TABLE [dbo].[ProjectCandidateCategoryDetailType1]
	ADD CONSTRAINT [FK_ProjectCandidateCategoryDetailType1_ProjectCategoryDetailType1]
	FOREIGN KEY ([ProjectCategoryDetailType1Id])
	REFERENCES [ProjectCategoryDetailType1] (Id)
