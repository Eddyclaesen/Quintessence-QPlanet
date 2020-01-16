ALTER TABLE [dbo].[EvaluationForm]
	ADD CONSTRAINT [FK_EvaluationForm_MailStatusType]
	FOREIGN KEY (MailStatusTypeId)
	REFERENCES [MailStatusType] (Id)
