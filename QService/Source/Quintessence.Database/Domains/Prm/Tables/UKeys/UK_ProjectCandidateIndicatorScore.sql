ALTER TABLE [dbo].[ProjectCandidateIndicatorScore]
	ADD CONSTRAINT [UK_ProjectCandidateIndicatorScore] 
	UNIQUE ([ProjectCandidateId], [DictionaryIndicatorId])