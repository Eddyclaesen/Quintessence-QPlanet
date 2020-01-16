ALTER TABLE [dbo].[Translation]  
	ADD CONSTRAINT [FK_Translation_Language] 
	FOREIGN KEY([LanguageId])
	REFERENCES [dbo].[Language] ([Id])