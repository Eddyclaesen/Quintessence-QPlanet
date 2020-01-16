ALTER TABLE [dbo].[MailTemplateTranslation]
	ADD CONSTRAINT [FK_MailTemplateTranslation_MailTemplate]
	FOREIGN KEY (MailTemplateId)
	REFERENCES [MailTemplate] (Id)
