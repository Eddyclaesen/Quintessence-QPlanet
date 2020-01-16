ALTER TABLE [dbo].[TheoremTemplateTranslation]
	ADD CONSTRAINT [FK_TheoremTemplateTranslation_Language]
	FOREIGN KEY (LanguageId)
	REFERENCES [Language] (Id)
