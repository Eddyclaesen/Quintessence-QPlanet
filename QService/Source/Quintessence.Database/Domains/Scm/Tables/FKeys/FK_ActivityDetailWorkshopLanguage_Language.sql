ALTER TABLE [dbo].[ActivityDetailWorkshopLanguage]  
	ADD CONSTRAINT [FK_ActivityDetailWorkshopLanguage_Language] 
	FOREIGN KEY([LanguageId])
	REFERENCES [dbo].[Language] ([Id])