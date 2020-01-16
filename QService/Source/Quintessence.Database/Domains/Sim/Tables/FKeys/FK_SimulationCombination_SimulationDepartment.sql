ALTER TABLE [dbo].[SimulationCombination]  
	ADD CONSTRAINT [FK_SimulationCombination_SimulationDepartment] 
	FOREIGN KEY([SimulationDepartmentId])
	REFERENCES [dbo].[SimulationDepartment] ([Id])