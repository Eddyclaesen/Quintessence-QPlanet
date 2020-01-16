ALTER TABLE [dbo].[ProjectTypeCategoryTranslation]
	ADD CONSTRAINT [FK_ProjectTypeCategoryTranslation_Translation]
	FOREIGN KEY (LanguageId)
	REFERENCES [Language] (Id)
