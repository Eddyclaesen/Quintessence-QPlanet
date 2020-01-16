ALTER TABLE [dbo].[SimulationCombination2Language]  
	ADD CONSTRAINT [FK_SimulationCombination2Language_Language] 
	FOREIGN KEY([LanguageId])
	REFERENCES [dbo].[Language] ([Id])