CREATE VIEW [dbo].[EvaluationFormView] AS
	SELECT		[EvaluationForm].*,
				[MailStatusTypeView].[Name]			AS [MailStatusName],
				[EvaluationFormTypeView].[Name]		AS [EvaluationFormName],
				[LanguageView].[Code]				AS [LanguageCode]

	FROM		[EvaluationForm]	WITH (NOLOCK)

	INNER JOIN  [MailStatusTypeView]
		ON		[MailStatusTypeView].[Id] = [EvaluationForm].[MailStatusTypeId]

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [EvaluationForm].[LanguageId]

	INNER JOIN  [EvaluationFormTypeView]
		ON		[EvaluationFormTypeView].[Id] = [EvaluationForm].[EvaluationFormTypeId]
