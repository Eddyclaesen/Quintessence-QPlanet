ALTER TABLE [dbo].[ContactPerson]
	ADD CONSTRAINT [FK_ContactPerson_Language]
	FOREIGN KEY (LanguageId)
	REFERENCES [Language] (Id)
	 