ALTER TABLE [dbo].[TheoremTemplateTranslation]
	ADD CONSTRAINT [FK_TheoremTemplateTranslation_TheoremTemplate]
	FOREIGN KEY (TheoremTemplateId)
	REFERENCES [TheoremTemplate] (Id)
