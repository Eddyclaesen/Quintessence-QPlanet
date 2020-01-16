ALTER TABLE [dbo].[DictionaryCompetence]  
	ADD CONSTRAINT [FK_DictionaryCompetence_DictionaryCluster] 
	FOREIGN KEY([DictionaryClusterId])
	REFERENCES [dbo].[DictionaryCluster] ([Id])