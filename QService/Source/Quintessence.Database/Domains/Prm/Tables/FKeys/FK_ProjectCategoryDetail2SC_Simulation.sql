ALTER TABLE [dbo].[ProjectCategoryDetail2SimulationCombination]  
	ADD CONSTRAINT [FK_ProjectCategoryDetail2SimulationCombination_SimulationCombination] 
	FOREIGN KEY([SimulationCombinationId])
	REFERENCES [dbo].[SimulationCombination] ([Id])