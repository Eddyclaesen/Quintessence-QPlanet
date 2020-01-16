ALTER TABLE [dbo].[SimulationContextUser]  
	ADD CONSTRAINT [FK_SimulationContextUser_SimulationContext] 
	FOREIGN KEY([SimulationContextId])
	REFERENCES [dbo].[SimulationContext] ([Id])