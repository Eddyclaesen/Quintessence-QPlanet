ALTER TABLE [dbo].[ProjectCandidateCompetenceSimulationScore]
	ADD CONSTRAINT [FK_ProjectCandidateCompetenceSimulationScore_SimulationCombination]
	FOREIGN KEY (SimulationCombinationId)
	REFERENCES [SimulationCombination] (Id)
