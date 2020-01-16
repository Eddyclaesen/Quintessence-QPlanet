ALTER TABLE [dbo].[SimulationCombination]  
	ADD CONSTRAINT [FK_SimulationCombination_SimulationLevel] 
	FOREIGN KEY([SimulationLevelId])
	REFERENCES [dbo].[SimulationLevel] ([Id])