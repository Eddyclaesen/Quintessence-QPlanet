ALTER TABLE [dbo].[ProjectRoleTranslation]
	ADD CONSTRAINT [FK_ProjectRoleTranslation_Translation]
	FOREIGN KEY (LanguageId)
	REFERENCES [Language] (Id)
