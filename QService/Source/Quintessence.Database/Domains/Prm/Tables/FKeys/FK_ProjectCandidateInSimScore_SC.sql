ALTER TABLE [dbo].[ProjectCandidateIndicatorSimulationScore]
	ADD CONSTRAINT [FK_ProjectCandidateIndicatorSimulationScore_SimulationCombination]
	FOREIGN KEY (SimulationCombinationId)
	REFERENCES [SimulationCombination] (Id)
