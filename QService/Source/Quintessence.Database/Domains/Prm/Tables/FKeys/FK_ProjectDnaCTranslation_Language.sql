ALTER TABLE [dbo].[ProjectDnaCommercialTranslation]  
	ADD CONSTRAINT [FK_ProjectDnaCommercialTranslation_Language] 
	FOREIGN KEY([LanguageId])
	REFERENCES [dbo].[Language] ([Id])