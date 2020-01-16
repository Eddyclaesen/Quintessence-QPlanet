ALTER TABLE [dbo].[ProjectCategoryDetail2Competence2Combination]  
	ADD CONSTRAINT [FK_ProjectCategoryDetail2C2C_SimulationCombination] 
	FOREIGN KEY([SimulationCombinationId])
	REFERENCES [dbo].[SimulationCombination] ([Id])