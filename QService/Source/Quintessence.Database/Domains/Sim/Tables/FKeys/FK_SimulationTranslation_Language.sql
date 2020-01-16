ALTER TABLE [dbo].[SimulationTranslation]  
	ADD CONSTRAINT [FK_SimulationTranslation_Language] 
	FOREIGN KEY([LanguageId])
	REFERENCES [dbo].[Language] ([Id])