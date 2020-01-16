ALTER TABLE [dbo].[SimulationTranslation]  
	ADD CONSTRAINT [FK_SimulationTranslation_Simulation] 
	FOREIGN KEY([SimulationId])
	REFERENCES [dbo].[Simulation] ([Id])