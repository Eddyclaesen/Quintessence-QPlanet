ALTER TABLE [dbo].[OfficeTranslation]  
	ADD CONSTRAINT [FK_OfficeTranslation_Language] 
	FOREIGN KEY([LanguageId])
	REFERENCES [dbo].[Language] ([Id])