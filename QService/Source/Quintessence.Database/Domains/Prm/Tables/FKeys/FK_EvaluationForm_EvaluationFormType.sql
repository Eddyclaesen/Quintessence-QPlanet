ALTER TABLE [dbo].[EvaluationForm]
	ADD CONSTRAINT [FK_EvaluationForm_EvaluationFormType]
	FOREIGN KEY (EvaluationFormTypeId)
	REFERENCES [EvaluationFormType] (Id)
