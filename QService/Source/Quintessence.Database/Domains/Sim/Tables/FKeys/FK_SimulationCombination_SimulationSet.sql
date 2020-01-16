ALTER TABLE [dbo].[SimulationCombination] 
	ADD CONSTRAINT [FK_SimulationCombination_SimulationSet] 
	FOREIGN KEY([SimulationSetId])
	REFERENCES [dbo].[SimulationSet] ([Id])