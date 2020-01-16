ALTER TABLE [dbo].[ProjectCategoryDetail2SimulationCombination]  
	ADD CONSTRAINT [FK_ProjectCategoryDetail2SimulationCombination_ProjectCategoryDetail] 
	FOREIGN KEY([ProjectCategoryDetailId])
	REFERENCES [dbo].[ProjectCategoryDetail] ([Id])