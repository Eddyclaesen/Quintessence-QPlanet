ALTER TABLE [dbo].[DictionaryCluster]  
	ADD CONSTRAINT [FK_DictionaryCluster_Dictionary] 
	FOREIGN KEY([DictionaryId])
	REFERENCES [dbo].[Dictionary] ([Id])