ALTER TABLE [dbo].[DictionaryClusterTranslation]  
	ADD CONSTRAINT [FK_DictionaryClusterTranslation_DictionaryCluster] 
	FOREIGN KEY([DictionaryClusterId])
	REFERENCES [dbo].[DictionaryCluster] ([Id])