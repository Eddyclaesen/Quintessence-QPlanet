ALTER TABLE [dbo].[ActivityDetailTrainingLanguage]  
	ADD CONSTRAINT [FK_ActivityDetailTrainingLanguage_Language] 
	FOREIGN KEY([LanguageId])
	REFERENCES [dbo].[Language] ([Id])