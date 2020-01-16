ALTER TABLE [dbo].[TheoremTranslation]
	ADD CONSTRAINT [FK_TheoremTranslation_Language]
	FOREIGN KEY (LanguageId)
	REFERENCES [Language] (Id)
