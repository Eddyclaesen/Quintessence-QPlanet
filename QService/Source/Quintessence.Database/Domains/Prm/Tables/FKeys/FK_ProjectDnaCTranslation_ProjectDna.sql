ALTER TABLE [dbo].[ProjectDnaCommercialTranslation]  
	ADD CONSTRAINT [FK_ProjectDnaCommercialTranslation_ProjectDna] 
	FOREIGN KEY([ProjectDnaId])
	REFERENCES [dbo].[ProjectDna] ([Id])