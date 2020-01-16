ALTER TABLE [dbo].[ProjectCandidateClusterScore]
	ADD CONSTRAINT [FK_ProjectCandidateClusterScore_DictionaryCluster]
	FOREIGN KEY (DictionaryClusterId)
	REFERENCES [DictionaryCluster] (Id)
