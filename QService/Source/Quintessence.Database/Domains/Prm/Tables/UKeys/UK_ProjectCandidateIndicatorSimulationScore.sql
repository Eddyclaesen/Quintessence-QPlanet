ALTER TABLE [dbo].[ProjectCandidateIndicatorSimulationScore]
	ADD CONSTRAINT [UK_ProjectCandidateIndicatorSimulationScore] 
	UNIQUE ([ProjectCandidateId], [SimulationCombinationId], [DictionaryIndicatorId])