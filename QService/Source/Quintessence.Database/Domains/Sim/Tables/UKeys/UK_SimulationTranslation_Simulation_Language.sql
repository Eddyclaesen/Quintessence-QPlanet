ALTER TABLE [dbo].[SimulationTranslation]
	ADD CONSTRAINT [UK_SimulationTranslation_Simulation_Language] 
	UNIQUE ([SimulationId], [LanguageId])