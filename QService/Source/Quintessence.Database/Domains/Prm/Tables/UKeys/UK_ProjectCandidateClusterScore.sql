ALTER TABLE [dbo].[ProjectCandidateClusterScore]
	ADD CONSTRAINT [UK_ProjectCandidateClusterScore] 
	UNIQUE ([ProjectCandidateId], [DictionaryClusterId])