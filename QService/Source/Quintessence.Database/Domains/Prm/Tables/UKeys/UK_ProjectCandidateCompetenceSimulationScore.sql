ALTER TABLE [dbo].[ProjectCandidateCompetenceSimulationScore]
	ADD CONSTRAINT [UK_ProjectCandidateCompetenceSimulationScore] 
	UNIQUE ([ProjectCandidateId], [SimulationCombinationId], [DictionaryCompetenceId])