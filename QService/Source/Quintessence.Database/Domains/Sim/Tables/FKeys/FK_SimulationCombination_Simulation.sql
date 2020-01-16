ALTER TABLE [dbo].[SimulationCombination]  
	ADD CONSTRAINT [FK_SimulationCombination_Simulation] 
	FOREIGN KEY([SimulationId])
	REFERENCES [dbo].[Simulation] ([Id])