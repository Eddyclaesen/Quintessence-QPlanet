ALTER TABLE [dbo].[EmailTemplate]
	ADD CONSTRAINT [FK_EmailTemplate_Language]
	FOREIGN KEY (LanguageId)
	REFERENCES [Language] (Id)
