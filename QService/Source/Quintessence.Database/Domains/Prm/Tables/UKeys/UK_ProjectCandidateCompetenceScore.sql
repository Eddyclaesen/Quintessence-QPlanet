ALTER TABLE [dbo].[ProjectCandidateCompetenceScore]
	ADD CONSTRAINT [UK_ProjectCandidateCompetenceScore] 
	UNIQUE ([ProjectCandidateId], [DictionaryCompetenceId])