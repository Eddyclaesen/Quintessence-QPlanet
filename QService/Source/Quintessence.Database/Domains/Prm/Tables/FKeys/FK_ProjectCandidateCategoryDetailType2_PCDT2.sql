ALTER TABLE [dbo].[ProjectCandidateCategoryDetailType2]
	ADD CONSTRAINT [FK_ProjectCandidateCategoryDetailType2_ProjectCategoryDetailType2]
	FOREIGN KEY ([ProjectCategoryDetailType2Id])
	REFERENCES [ProjectCategoryDetailType2] (Id)
