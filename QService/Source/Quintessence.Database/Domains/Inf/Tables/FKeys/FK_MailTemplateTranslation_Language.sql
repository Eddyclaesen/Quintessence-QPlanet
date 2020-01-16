ALTER TABLE [dbo].[MailTemplateTranslation]
	ADD CONSTRAINT [FK_MailTemplateTranslation_Language]
	FOREIGN KEY (LanguageId)
	REFERENCES [Language] (Id)
