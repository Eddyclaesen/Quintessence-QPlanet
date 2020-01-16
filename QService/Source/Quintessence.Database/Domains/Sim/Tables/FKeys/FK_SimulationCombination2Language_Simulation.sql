ALTER TABLE [dbo].[SimulationCombination2Language]  
	ADD CONSTRAINT [FK_SimulationCombination2Language_Simulation] 
	FOREIGN KEY([SimulationCombinationId])
	REFERENCES [dbo].[SimulationCombination] ([Id])